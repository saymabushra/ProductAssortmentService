using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAssortmentServiceApi.Models;

namespace ProductAssortmentServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssortmentProductsController : ControllerBase
    {
        private readonly COOP_PAMContext _context;

        public AssortmentProductsController(COOP_PAMContext context)
        {
            _context = context;
        }

        // GET: api/AssortmentProducts
        /// <summary>
        /// Get  Assortement  Product list
        /// </summary
        [HttpGet]
        [Route("GetAssortmentProducts")]
        public async Task<ActionResult<IEnumerable<AssortmentProduct>>> GetAssortmentProducts()
        {
            try 
            { 
               return await _context.AssortmentProducts.ToListAsync();
            }
            catch (NullReferenceException exception)
            {
                var nullpointerException = new ResponseMessage<string>();
                nullpointerException.Message = ApiConstants.NULL_POINTER_EXCEPTION_OCCURED;
                nullpointerException.Code = 500;
                nullpointerException.Result = exception.Message;
                return BadRequest(nullpointerException);
            }
            catch (Exception exception)
            {
                var generalException = new ResponseMessage<string>();
                generalException.Message = ApiConstants.UNHANDELED_EXCEPTION_MESSAGE;
                generalException.Code = 500;
                generalException.Result = exception.Message;
                return BadRequest(generalException);

            }
        }

        // GET: api/AssortmentProducts/5
        /// <summary>
        /// Get Assortment Product List by Id
        /// </summary
        /// <param name="id"><Require></param>
        [HttpGet]
        [Route("GetAssortmentProduct/{id}")]
        public async Task<ActionResult<AssortmentProduct>> GetAssortmentProduct(int id)
        {
            try 
            {
                if (id == null)
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.API_MODEL_ERROR;
                    return BadRequest(responesMessage);
                }
                var assortmentProduct = await _context.AssortmentProducts.FindAsync(id);

                if (assortmentProduct == null)
                {
                    return NotFound();
                }

                return assortmentProduct;
            }
            catch (NullReferenceException exception)
            {
                var nullpointerException = new ResponseMessage<string>();
                nullpointerException.Message = ApiConstants.NULL_POINTER_EXCEPTION_OCCURED;
                nullpointerException.Code = 500;
                nullpointerException.Result = exception.Message;
                return BadRequest(nullpointerException);
            }
            catch (Exception exception)
            {
                var generalException = new ResponseMessage<string>();
                generalException.Message = ApiConstants.UNHANDELED_EXCEPTION_MESSAGE;
                generalException.Code = 500;
                generalException.Result = exception.Message;
                return BadRequest(generalException);

            }
        }

        // PUT: api/AssortmentProducts/5
        /// <summary>
        /// Update Assortment Product 
        /// </summary
        /// <param name="id"><Require></param>
        /// <param name="ProductId"><Require></param>
        /// <param name="AssrtmntId"><Rquire></param> 
        [HttpPut]
        [Route("PutAssortmentProduct/{id}")]
        public async Task<IActionResult> PutAssortmentProduct(int id, AssortmentProduct assortmentProduct)
        {
            try
            {
                if (id != assortmentProduct.ProductId)
                {
                    return BadRequest();
                }

                _context.Entry(assortmentProduct).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssortmentProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (NullReferenceException exception)
            {
                var nullpointerException = new ResponseMessage<string>();
                nullpointerException.Message = ApiConstants.NULL_POINTER_EXCEPTION_OCCURED;
                nullpointerException.Code = 500;
                nullpointerException.Result = exception.Message;
                return BadRequest(nullpointerException);
            }
            catch (Exception exception)
            {
                var generalException = new ResponseMessage<string>();
                generalException.Message = ApiConstants.UNHANDELED_EXCEPTION_MESSAGE;
                generalException.Code = 500;
                generalException.Result = exception.Message;
                return BadRequest(generalException);

            }
        }

        // POST: api/AssortmentProducts
        /// <summary>
        /// Add Product to Assortment 
        /// </summary
        /// <param name="ProductId"><Require></param>
        /// <param name="AssrtmntId"><Rquire></param>
        [HttpPost]
        [Route("AddAssortmentProduct")]
        public async Task<ActionResult<AssortmentProduct>> AddAssortmentProduct(AssortmentProduct assortmentProduct)
        {
            try {
                if (assortmentProduct.ProductId <=0 || assortmentProduct.AssrtmntId<=0)
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.API_MODEL_ERROR;
                    return BadRequest(responesMessage);
                }
                _context.AssortmentProducts.Add(assortmentProduct);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (AssortmentProductExists(assortmentProduct.ProductId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetAssortmentProduct", new { id = assortmentProduct.ProductId }, assortmentProduct);
            }
            catch (NullReferenceException exception)
            {
                var nullpointerException = new ResponseMessage<string>();
                nullpointerException.Message = ApiConstants.NULL_POINTER_EXCEPTION_OCCURED;
                nullpointerException.Code = 500;
                nullpointerException.Result = exception.Message;
                return BadRequest(nullpointerException);
            }
            catch (Exception exception)
            {
                var generalException = new ResponseMessage<string>();
                generalException.Message = ApiConstants.UNHANDELED_EXCEPTION_MESSAGE;
                generalException.Code = 500;
                generalException.Result = exception.Message;
                return BadRequest(generalException);

            }
        }

        // DELETE: api/AssortmentProducts/5
        /// <summary>
        /// Delete Assortment Product 
        /// </summary
        /// <param name="id"><Require></param>
        [HttpDelete]
        [Route("DeleteAssortmentProduct/{id}")]
        public async Task<IActionResult> DeleteAssortmentProduct(int id)
        {
            try
            { 
                var assortmentProduct = await _context.AssortmentProducts.FindAsync(id);
                if (assortmentProduct == null)
                {
                    return NotFound();
                }

                _context.AssortmentProducts.Remove(assortmentProduct);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (NullReferenceException exception)
            {
                var nullpointerException = new ResponseMessage<string>();
                nullpointerException.Message = ApiConstants.NULL_POINTER_EXCEPTION_OCCURED;
                nullpointerException.Code = 500;
                nullpointerException.Result = exception.Message;
                return BadRequest(nullpointerException);
            }
            catch (Exception exception)
            {
                var generalException = new ResponseMessage<string>();
                generalException.Message = ApiConstants.UNHANDELED_EXCEPTION_MESSAGE;
                generalException.Code = 500;
                generalException.Result = exception.Message;
                return BadRequest(generalException);

            }
        }

        private bool AssortmentProductExists(int id)
        {
            return _context.AssortmentProducts.Any(e => e.ProductId == id);
        }
    }
}
