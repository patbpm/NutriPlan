using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriPlan.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntakesApiController : ControllerBase
    {
        private readonly NutriPlanContext _context;

        public IntakesApiController(NutriPlanContext context)
        {
            _context = context;
        }

        // GET: api/Intakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intake>>> GetIntakes()
        {
            return await _context.Intakes.Include(i => i.UserProfile).ToListAsync();
        }

        // GET: api/Intakes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intake>> GetIntake(int id)
        {
            var intake = await _context.Intakes
                .Include(i => i.UserProfile)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (intake == null)
            {
                return NotFound();
            }

            return intake;
        }

        // POST: api/Intakes
        [HttpPost]
        public async Task<ActionResult<Intake>> PostIntake(Intake intake)
        {
            _context.Intakes.Add(intake);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIntake), new { id = intake.Id }, intake);
        }

        // PUT: api/Intakes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntake(int id, Intake intake)
        {
            if (id != intake.Id)
            {
                return BadRequest();
            }

            _context.Entry(intake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntakeExists(id))
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

        // DELETE: api/Intakes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntake(int id)
        {
            var intake = await _context.Intakes.FindAsync(id);
            if (intake == null)
            {
                return NotFound();
            }

            _context.Intakes.Remove(intake);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IntakeExists(int id)
        {
            return _context.Intakes.Any(e => e.Id == id);
        }
    }
}
