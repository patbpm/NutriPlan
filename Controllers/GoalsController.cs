using Microsoft.AspNetCore.Mvc;
using NutriPlan.Models;
using NutriPlan.Services;
using NutriPlan.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace NutriPlan.Controllers
{
    public class GoalsController : Controller
    {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        public async Task<IActionResult> Index()
        {
            var goals = await _goalService.GetAllGoalsAsync();
            var goalViewModels = goals.Select(g => new GoalViewModel
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                StartDate = g.StartDate,
                EndDate = g.EndDate,
                IsAchieved = g.IsAchieved,
                UserProfileId = g.UserProfileId,
                UserProfileName = g.UserProfile?.FullName
            }).ToList();
            return View(goalViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var goal = await _goalService.GetGoalByIdAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            var goalViewModel = new GoalViewModel
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
                StartDate = goal.StartDate,
                EndDate = goal.EndDate,
                IsAchieved = goal.IsAchieved,
                UserProfileId = goal.UserProfileId,
                UserProfileName = goal.UserProfile?.FullName
            };
            return View(goalViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GoalViewModel goalViewModel)
        {
            if (ModelState.IsValid)
            {
                var goal = new Goal
                {
                    Name = goalViewModel.Name,
                    Description = goalViewModel.Description,
                    StartDate = goalViewModel.StartDate,
                    EndDate = goalViewModel.EndDate,
                    IsAchieved = goalViewModel.IsAchieved,
                    UserProfileId = goalViewModel.UserProfileId
                };
                await _goalService.CreateGoalAsync(goal);
                return RedirectToAction(nameof(Index));
            }
            return View(goalViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var goal = await _goalService.GetGoalByIdAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            var goalViewModel = new GoalViewModel
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
                StartDate = goal.StartDate,
                EndDate = goal.EndDate,
                IsAchieved = goal.IsAchieved,
                UserProfileId = goal.UserProfileId,
                UserProfileName = goal.UserProfile?.FullName
            };
            return View(goalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GoalViewModel goalViewModel)
        {
            if (id != goalViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var goal = new Goal
                {
                    Id = goalViewModel.Id,
                    Name = goalViewModel.Name,
                    Description = goalViewModel.Description,
                    StartDate = goalViewModel.StartDate,
                    EndDate = goalViewModel.EndDate,
                    IsAchieved = goalViewModel.IsAchieved,
                    UserProfileId = goalViewModel.UserProfileId
                };
                try
                {
                    await _goalService.UpdateGoalAsync(goal);
                }
                catch
                {
                    if (await _goalService.GetGoalByIdAsync(goal.Id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(goalViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var goal = await _goalService.GetGoalByIdAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            var goalViewModel = new GoalViewModel
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
                StartDate = goal.StartDate,
                EndDate = goal.EndDate,
                IsAchieved = goal.IsAchieved,
                UserProfileId = goal.UserProfileId,
                UserProfileName = goal.UserProfile?.FullName
            };
            return View(goalViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _goalService.DeleteGoalAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}