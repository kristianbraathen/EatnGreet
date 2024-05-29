using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Models.NewEntry;
using webapi.Properties.Data;
using System;
using UserRegwebapi.Data;

namespace webapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NewentryController : Controller
    {
        private readonly NewEntryDbContext _context;
        private readonly UserRegDbContext _userContext;

        public NewentryController(NewEntryDbContext context, UserRegDbContext _userContext)
        {
            _context = context;
            this._userContext = _userContext;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllnewentrys()
        {
            var entry = await _context.NewEntries.ToListAsync();
            return Ok(entry);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetnewentryById(Guid id)
        {
            var entry = await _context.NewEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }
        [HttpPost]
        public async Task<IActionResult> AddnewEntry(NewEntries newentry)
        {
            try
            {
                var userId = HttpContext.Request.Headers["userId"].ToString();

                // Fetch the user from the database
                var user = await _userContext.UserRegistrations.FirstOrDefaultAsync
                    (
                    u => u.Id.ToString() == userId);

                if (user == null)
                {
                    return Unauthorized("Invalid user");
                }

                newentry.Id = Guid.NewGuid();
                newentry.CreatedAt = DateTime.Now;

                
                newentry.UserRegId = user.Id.ToString();

                await _context.NewEntries.AddAsync(newentry);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetnewentryById", new { id = newentry.Id }, newentry);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEntry(Guid id, [FromBody] NewEntries updatenewentry)
        {
            if (id != updatenewentry.Id)
            {
                return BadRequest();
            }
            var existingnewentry = await _context.NewEntries.FindAsync(id);
            if (existingnewentry == null)
            {
                return NotFound();
            }
            
            existingnewentry.UpdatedAt = DateTime.Now;
            existingnewentry.Description = updatenewentry.Description;
            existingnewentry.Allergens = updatenewentry.Allergens;
            existingnewentry.Date = updatenewentry.Date;
            existingnewentry.Time = updatenewentry.Time;
            existingnewentry.Vegetarian = updatenewentry.Vegetarian;

            try
            {
                _context.NewEntries.Update(existingnewentry);
                await _context.SaveChangesAsync();
                return Ok(existingnewentry);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Deleteentry(Guid id)
        {
            var existingnewentry = await _context.NewEntries.FindAsync(id);
            if (existingnewentry == null)
            {
                return NotFound();
            }
            try
            {
                _context.NewEntries.Remove(existingnewentry);
                await _context.SaveChangesAsync();
                return Ok(existingnewentry);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
    }
}
