using NutriPlan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services.Interfaces
{
    public interface IIntakeService
    {
        Task<IEnumerable<Intake>> GetAllAsync();
        Task<Intake> GetByIdAsync(int id);
        Task CreateAsync(Intake intake);
        Task UpdateAsync(Intake intake);
        Task DeleteAsync(int id);
    }
}
