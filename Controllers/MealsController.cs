using Microsoft.AspNetCore.Mvc;
using NutriPlan.Models;
using NutriPlan.Services;
using NutriPlan.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriPlan.Controllers
{
    public class MealsController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMealPlanService _mealPlanService;

        public MealsController(IMealService mealService, IMealPlanService mealPlanService)
        {
            _mealService = mealService;
            _mealPlanService = mealPlanService;
        }

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            var meals = await _mealService.GetAllMealsAsync();
            var mealViewModels = meals.Select(meal => new MealViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                DateTime = meal.DateTime,
                Description = meal.Description,
                Calories = meal.Calories,
                MealPlanId = meal.MealPlanId
            }).ToList();

            return View(mealViewModels);
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            var mealViewModel = new MealViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                DateTime = meal.DateTime,
                Description = meal.Description,
                Calories = meal.Calories,
                MealPlanId = meal.MealPlanId
            };

            return View(mealViewModel);
        }

        // GET: Meals/Create
        public async Task<IActionResult> Create()
        {
            var mealPlans = await _mealPlanService.GetAllMealPlansAsync();
            ViewBag.MealPlans = mealPlans.Select(mp => new { mp.Id, mp.Name }).ToList();
            return View();
        }

        // POST: Meals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTime,Description,Calories,MealPlanId")] MealViewModel mealViewModel)
        {
            if (ModelState.IsValid)
            {
                var meal = new Meal
                {
                    Id = mealViewModel.Id,
                    Name = mealViewModel.Name,
                    DateTime = mealViewModel.DateTime,
                    Description = mealViewModel.Description,
                    Calories = mealViewModel.Calories,
                    MealPlanId = mealViewModel.MealPlanId
                };

                await _mealService.CreateMealAsync(meal);
                return RedirectToAction(nameof(Index));
            }
            var mealPlans = await _mealPlanService.GetAllMealPlansAsync();
            ViewBag.MealPlans = mealPlans.Select(mp => new { mp.Id, mp.Name }).ToList();
            return View(mealViewModel);
        }
        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            var mealViewModel = new MealViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                DateTime = meal.DateTime,
                Description = meal.Description,
                Calories = meal.Calories,
                MealPlanId = meal.MealPlanId
            };

            var mealPlans = await _mealPlanService.GetAllMealPlansAsync();
            ViewBag.MealPlans = mealPlans.Select(mp => new { mp.Id, mp.Name }).ToList();
            return View(mealViewModel);
        }

        // POST: Meals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateTime,Description,Calories,MealPlanId")] MealViewModel mealViewModel)
        {
            if (id != mealViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var meal = new Meal
                {
                    Id = mealViewModel.Id,
                    Name = mealViewModel.Name,
                    DateTime = mealViewModel.DateTime,
                    Description = mealViewModel.Description,
                    Calories = mealViewModel.Calories,
                    MealPlanId = mealViewModel.MealPlanId
                };

                try
                {
                    await _mealService.UpdateMealAsync(meal);
                }
                catch
                {
                    if (await _mealService.GetMealByIdAsync(mealViewModel.Id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var mealPlans = await _mealPlanService.GetAllMealPlansAsync();
            ViewBag.MealPlans = mealPlans.Select(mp => new { mp.Id, mp.Name }).ToList();
            return View(mealViewModel);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            var mealViewModel = new MealViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                DateTime = meal.DateTime,
                Description = meal.Description,
                Calories = meal.Calories,
                MealPlanId = meal.MealPlanId
            };

            return View(mealViewModel);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mealService.DeleteMealAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
