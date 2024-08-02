using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using NutriPlan.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace NutriPlan.Controllers
{
    public class MealPlansController : Controller
    {
        private readonly NutriPlanContext _context;

        public MealPlansController(NutriPlanContext context)
        {
            _context = context;
        }

        // GET: MealPlans
        public async Task<IActionResult> Index()
        {
            var mealPlans = await _context.MealPlans
                .Include(mp => mp.UserProfile)
                .Select(mp => new MealPlanViewModel
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    StartDate = mp.StartDate,
                    EndDate = mp.EndDate,
                    UserProfileId = mp.UserProfileId,
                    UserProfileName = mp.UserProfile.FullName,
                    Meals = mp.Meals.Select(m => new MealViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        DateTime = m.DateTime,
                        Description = m.Description,
                        Calories = m.Calories,
                        MealPlanId = m.MealPlanId
                    }).ToList()
                })
                .ToListAsync();
            return View(mealPlans);
        }

        // GET: MealPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans
                .Include(mp => mp.UserProfile)
                .Include(mp => mp.Meals)
                .Where(mp => mp.Id == id)
                .Select(mp => new MealPlanViewModel
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    StartDate = mp.StartDate,
                    EndDate = mp.EndDate,
                    UserProfileId = mp.UserProfileId,
                    UserProfileName = mp.UserProfile.FullName,
                    Meals = mp.Meals.Select(m => new MealViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        DateTime = m.DateTime,
                        Description = m.Description,
                        Calories = m.Calories,
                        MealPlanId = m.MealPlanId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (mealPlan == null)
            {
                return NotFound();
            }

            return View(mealPlan);
        }

        public IActionResult Create()
        {
            // Fetch meal plans from the database
            var mealPlans = _context.MealPlans.ToList();

            // Populate ViewBag with meal plans for dropdown
            ViewBag.MealPlans = new SelectList(mealPlans, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealViewModel mealViewModel)
        {
            if (ModelState.IsValid)
            {
                var meal = new Meal
                {
                    Name = mealViewModel.Name,
                    DateTime = mealViewModel.DateTime,
                    Description = mealViewModel.Description,
                    Calories = mealViewModel.Calories,
                    MealPlanId = mealViewModel.MealPlanId
                };

                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag in case of validation failure
            ViewBag.MealPlans = new SelectList(_context.MealPlans, "Id", "Name", mealViewModel.MealPlanId);
            return View(mealViewModel);
        }
        // POST: MealPlans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,UserProfileId")] MealPlanViewModel mealPlanViewModel)
        {
            if (id != mealPlanViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mealPlan = new MealPlan
                    {
                        Id = mealPlanViewModel.Id,
                        Name = mealPlanViewModel.Name,
                        Description = mealPlanViewModel.Description,
                        StartDate = mealPlanViewModel.StartDate,
                        EndDate = mealPlanViewModel.EndDate,
                        UserProfileId = mealPlanViewModel.UserProfileId
                    };

                    _context.Update(mealPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealPlanExists(mealPlanViewModel.Id))
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

            ViewData["UserProfileId"] = new SelectList(_context.Users, "Id", "Name", mealPlanViewModel.UserProfileId);
            return View(mealPlanViewModel);
        }

        // GET: MealPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans
                .Include(mp => mp.UserProfile)
                .Include(mp => mp.Meals)
                .Where(mp => mp.Id == id)
                .Select(mp => new MealPlanViewModel
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    StartDate = mp.StartDate,
                    EndDate = mp.EndDate,
                    UserProfileId = mp.UserProfileId,
                    UserProfileName = mp.UserProfile.FullName,
                    Meals = mp.Meals.Select(m => new MealViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        DateTime = m.DateTime,
                        Description = m.Description,
                        Calories = m.Calories,
                        MealPlanId = m.MealPlanId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (mealPlan == null)
            {
                return NotFound();
            }

            return View(mealPlan);
        }

        // POST: MealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);
            _context.MealPlans.Remove(mealPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealPlanExists(int id)
        {
            return _context.MealPlans.Any(e => e.Id == id);
        }
    }
}
