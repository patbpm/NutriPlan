using Microsoft.EntityFrameworkCore;
using NutriPlan.Models;

namespace NutriPlan.Data
{
    public class NutriPlanContext : DbContext
    {
        public NutriPlanContext(DbContextOptions<NutriPlanContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Intake> Intakes { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
