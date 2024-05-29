using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UserRegistrationwebapi.Models;
using UserRegwebapi.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Models.NewEntry;


namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        public readonly UserRegDbContext userRegistrationDbContext;
        

        public RegistrationController(UserRegDbContext userRegistrationDbContext)
        {
            this.userRegistrationDbContext = userRegistrationDbContext;
        }
        // GET: api/Registration
        [HttpGet]

        public async Task<IActionResult> GetAllRegistrations()
        {
            var registrations = await userRegistrationDbContext.UserRegistrations.ToListAsync();
            return Ok(registrations);
        }
        [HttpGet]
        // GET: api/Registration/5
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegistrationbyId(Guid id)
        {
            var registration = await userRegistrationDbContext.UserRegistrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return Ok(registration);
        }
        // POST: api/Registration
        [HttpPost]
        public async Task<IActionResult> AddRegistration(UserRegistrations userRegistration)
        {
            userRegistration.Password = BCrypt.Net.BCrypt.HashPassword(userRegistration.Password);

            try
            {
                // Check if a registration with the same email already exists
                var existingRegistration = await userRegistrationDbContext.UserRegistrations
                    .FirstOrDefaultAsync(r => r.Email == userRegistration.Email);

                if (existingRegistration != null)
                {
                    // If a registration is found, return a conflict status code (409)
                    return Conflict("A registration with this email already exists.");

                }
                userRegistration.Id = Guid.NewGuid();
                await userRegistrationDbContext.UserRegistrations.AddAsync(userRegistration);
                await userRegistrationDbContext.SaveChangesAsync();
                return CreatedAtAction("GetRegistrationbyId", new { id = userRegistration.Id }, userRegistration);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegistration(Guid id, [FromBody] UserRegistrations updateUserRegistration)
        {
            var exixtingRegistration = await userRegistrationDbContext.UserRegistrations.FindAsync(id);
            if(exixtingRegistration == null)
            {
                return NotFound();
            }
            exixtingRegistration.FirstName = updateUserRegistration.FirstName;
            exixtingRegistration.LastName = updateUserRegistration.LastName;
            exixtingRegistration.Email = updateUserRegistration.Email;
            exixtingRegistration.Phone = updateUserRegistration.Phone;
            exixtingRegistration.Address = updateUserRegistration.Address;

            await userRegistrationDbContext.SaveChangesAsync();
            return Ok(exixtingRegistration);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRegistration([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return BadRequest("Invalid Guid format");
            }

            var userRegistration = await userRegistrationDbContext.UserRegistrations.FindAsync(guid);
            if (userRegistration == null)
            {
                return NotFound();
            }
            userRegistrationDbContext.UserRegistrations.Remove(userRegistration);
            await userRegistrationDbContext.SaveChangesAsync();
            return NoContent();
        }
        }
}

