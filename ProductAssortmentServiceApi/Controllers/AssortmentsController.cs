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
    public class AssortmentsController : ControllerBase
    {
        private readonly COOP_PAMContext _context;

        public AssortmentsController(COOP_PAMContext context)
        {
            _context = context;
        }

        // GET: api/Assortments
        /// <summary>
        /// Get Assortment List
        [HttpGet]
        [Route("GetAssortments")]
        public async Task<ActionResult<IEnumerable<Assortment>>> GetAssortments()
        {
            try
            { 
            return await _context.Assortments.ToListAsync();
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

        // GET: api/Assortments/5
        /// <summary>
        /// Get Assortment by Id
        /// </summary
        /// <param name="id"><Require></param>
        
        [HttpGet]
        [Route("GetAssortment/{id}")]
        public async Task<ActionResult<Assortment>> GetAssortment(int id)
        {
            try
            {
                if (id == null)
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.API_MODEL_ERROR;
                    return BadRequest(responesMessage);
                }
                var assortment = await _context.Assortments.FindAsync(id);

                if (assortment == null)
                {
                    return NotFound();
                }

                return assortment;
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

        // PUT: api/Assortments/5
        /// <summary>
        /// Update Assortment
        /// </summary
        /// <param name="id"><Require></param>
        /// <param name="AssrtmntName"><Not Require></param>
        /// <param name="AssrtmntActiveFrom"><Not require></param>  
        /// <param name="AssrtmntActiveTo"><Not require></param> 
        [HttpPut]
        [Route("PutAssortment/{id}")]
        public async Task<IActionResult> PutAssortment(int id, Assortment assortment)
        {
            try
            { 
                if (id != assortment.AssrtmntId)
                {
                    return BadRequest();
                }

                _context.Entry(assortment).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssortmentExists(id))
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

        // POST: api/Assortments
        /// <summary>
        /// Add Assortments 
        /// </summary
        /// <param name="AssrtmntName"><Require></param>
        /// <param name="AssrtmntActiveFrom"><require></param>  
        /// <param name="AssrtmntActiveTo"><Nullable></param>  
        [HttpPost]
        [Route("AddAssortment")]
        public async Task<ActionResult<Assortment>> AddAssortment(Assortment assortment)
        {
            try
            {
                if ((string.IsNullOrEmpty(assortment.AssrtmntName)))
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.API_MODEL_ERROR;
                    return BadRequest(responesMessage);
                }
                if (assortment.AssrtmntName.Length > 100)
                {
                    var inputException = new ResponseMessage<string>();
                    inputException.Message = ApiConstants.EXCEED_MAX_LIMIT;
                    return BadRequest(inputException);
                }
                

                _context.Assortments.Add(assortment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAssortment", new { id = assortment.AssrtmntId }, assortment);
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

        // DELETE: api/Assortments/5
        /// <summary>
        /// Delete Assortment
        /// </summary
        /// <param name="id"><Require></param>
        [HttpDelete]
        [Route("DeleteAssortment/{id}")]
        public async Task<IActionResult> DeleteAssortment(int id)
        {
            try
            {
                if (id == null)
                {
                    var responesMessage = new ResponseMessage<string>();
                    responesMessage.Message = ApiConstants.API_MODEL_ERROR;
                    return BadRequest(responesMessage);
                }
                var assortment = await _context.Assortments.FindAsync(id);
                if (assortment == null)
                {
                    return NotFound();
                }

                _context.Assortments.Remove(assortment);
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

        private bool AssortmentExists(int id)
        {
            return _context.Assortments.Any(e => e.AssrtmntId == id);
        }
    }
}
