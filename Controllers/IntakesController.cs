using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NutriPlan.Controllers
{
    public class IntakesController : Controller
    {
        private readonly NutriPlanContext _context;

        public IntakesController(NutriPlanContext context)
        {
            _context = context;
        }

        // GET: Intakes
        public async Task<IActionResult> Index()
        {
            var intakes = await _context.Intakes
                .Include(i => i.UserProfile)
                .ToListAsync();
            return View(intakes);
        }

        // GET: Intakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes
                .Include(i => i.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // GET: Intakes/Create
        public IActionResult Create()
        {
            // Populate the UserProfile dropdown
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Name");
            return View();
        }

        // POST: Intakes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,FoodItem,Calories,Protein,Carbohydrates,Fat,UserProfileId")] Intake intake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the UserProfile dropdown if validation fails
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Name", intake.UserProfileId);
            return View(intake);
        }

        // GET: Intakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes.FindAsync(id);
            if (intake == null)
            {
                return NotFound();
            }

            // Populate the UserProfile dropdown
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Name", intake.UserProfileId);
            return View(intake);
        }

        // POST: Intakes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,FoodItem,Calories,Protein,Carbohydrates,Fat,UserProfileId")] Intake intake)
        {
            if (id != intake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakeExists(intake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the UserProfile dropdown if validation fails
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Name", intake.UserProfileId);
            return View(intake);
        }

        // GET: Intakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes
                .Include(i => i.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // POST: Intakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intake = await _context.Intakes.FindAsync(id);
            _context.Intakes.Remove(intake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntakeExists(int id)
        {
            return _context.Intakes.Any(e => e.Id == id);
        }
    }
}
