using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesApiController : ControllerBase
    {
        private readonly NutriPlanContext _context;

        public UserProfilesApiController(NutriPlanContext context)
        {
            _context = context;
        }

        // GET: api/UserProfilesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserProfilesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            var userProfile = await _context.Users.FindAsync(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return userProfile;
        }

        // POST: api/UserProfilesApi
        [HttpPost]
        public async Task<ActionResult<UserProfile>> PostUserProfile(UserProfile userProfile)
        {
            _context.Users.Add(userProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserProfile), new { id = userProfile.Id }, userProfile);
        }

        // PUT: api/UserProfilesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(string? id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
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

        // DELETE: api/UserProfilesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var userProfile = await _context.Users.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfileExists(string? id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
