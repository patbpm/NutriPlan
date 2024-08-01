using NutriPlan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services
{
    public interface IMealPlanService
    {
        Task<IEnumerable<MealPlan>> GetAllMealPlansAsync();
        Task<MealPlan> GetMealPlanByIdAsync(int id);
        Task CreateMealPlanAsync(MealPlan mealPlan);
        Task UpdateMealPlanAsync(MealPlan mealPlan);
        Task DeleteMealPlanAsync(int id);
    }
}
