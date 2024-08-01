using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public string DietaryRestrictions { get; set; }

        public ICollection<MealPlan> MealPlans { get; set; }
        public ICollection<Intake> Intakes { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }
}
