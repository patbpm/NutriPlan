using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Models;
using NutriPlan.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlan.Services
{
    public class IntakeService : IIntakeService
    {
        private readonly NutriPlanContext _context;

        public IntakeService(NutriPlanContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intake>> GetAllAsync()
        {
            return await _context.Intakes.Include(i => i.UserProfile).ToListAsync();
        }

        public async Task<Intake> GetByIdAsync(int id)
        {
            return await _context.Intakes
                .Include(i => i.UserProfile)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task CreateAsync(Intake intake)
        {
            _context.Intakes.Add(intake);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Intake intake)
        {
            _context.Intakes.Update(intake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var intake = await _context.Intakes.FindAsync(id);
            if (intake != null)
            {
                _context.Intakes.Remove(intake);
                await _context.SaveChangesAsync();
            }
        }
    }
}
