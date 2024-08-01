using NutriPlan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services
{
    public interface IGoalService
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync();
        Task<Goal> GetGoalByIdAsync(int id);
        Task CreateGoalAsync(Goal goal);
        Task UpdateGoalAsync(Goal goal);
        Task DeleteGoalAsync(int id);
    }
}
