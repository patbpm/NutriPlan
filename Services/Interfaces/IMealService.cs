using NutriPlan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal> GetMealByIdAsync(int id);
        Task CreateMealAsync(Meal meal);
        Task UpdateMealAsync(Meal meal);
        Task DeleteMealAsync(int id);
    }
}
