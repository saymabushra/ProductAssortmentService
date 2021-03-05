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
    public class ProductsController : ControllerBase
    {
        private readonly COOP_PAMContext _context;

        public ProductsController(COOP_PAMContext context)
        {
            _context = context;
        }

        // GET: api/Products
        /// <summary>
        /// Get Products list
        /// </summary
        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try 
            {
                
             return await _context.Products.ToListAsync();
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

        // GET: api/Products/5
        /// <summary>
        /// Get Product List by Id
        /// </summary
        /// <param name="id"><Require></param>
        
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                if (id <=0)
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.BAD_INPUT_MESSAGE;
                    return BadRequest(responesMessage);
                }
               
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
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

        // PUT: api/Products/5
        /// <summary>
        /// Update Product 
        /// </summary
        /// <param name="id"><Require></param>
        /// <param name="ProductName"><Require></param>
        /// <param name="ProductEancode"><Rquire></param>  
        [HttpPut]
        [Route("PutProduct/{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            { 
                if (id != product.ProductId)
                {
                    return BadRequest();
                }

                _context.Entry(product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
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

        // POST: api/Products
        /// <summary>
        /// Add Product 
        /// </summary
        /// <param name="ProductName"><Require></param>
        /// <param name="ProductEancode"><Rquire></param>
        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {

                if ((string.IsNullOrEmpty(product.ProductEancode))||(string.IsNullOrEmpty(product.ProductName)))
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.API_MODEL_ERROR;
                    return BadRequest(responesMessage);
                }
                if (product.ProductName.Length>100)
                {
                    var inputException = new ResponseMessage<string>();
                    inputException.Message = ApiConstants.EXCEED_MAX_LIMIT;
                    return BadRequest(inputException);
                }
                if (product.ProductEancode.Length > 20)
                {
                    var inputException = new ResponseMessage<string>();
                    inputException.Message = ApiConstants.EXCEED_MAX_LIMIT;
                    return BadRequest(inputException);
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
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

        // DELETE: api/Products/5
        /// <summary>
        /// Delete Product 
        /// </summary
        /// <param name="id"><Require></param>
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try 
            { 
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
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

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
