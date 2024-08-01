using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services
{
    public class MealPlanService : IMealPlanService
    {
        private readonly NutriPlanContext _context;

        public MealPlanService(NutriPlanContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MealPlan>> GetAllMealPlansAsync()
        {
            return await _context.MealPlans
                .Include(mp => mp.Meals)
                .ToListAsync();
        }

        public async Task<MealPlan> GetMealPlanByIdAsync(int id)
        {
            return await _context.MealPlans
                .Include(mp => mp.Meals)
                .FirstOrDefaultAsync(mp => mp.Id == id);
        }

        public async Task CreateMealPlanAsync(MealPlan mealPlan)
        {
            _context.MealPlans.Add(mealPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMealPlanAsync(MealPlan mealPlan)
        {
            _context.MealPlans.Update(mealPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMealPlanAsync(int id)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan != null)
            {
                _context.MealPlans.Remove(mealPlan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
