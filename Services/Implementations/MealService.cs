using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services
{
    public class MealService : IMealService
    {
        private readonly NutriPlanContext _context;

        public MealService(NutriPlanContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await _context.Meals.ToListAsync();
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task CreateMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
