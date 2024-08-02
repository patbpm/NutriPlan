using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.Models
{
    public class UserProfile : IdentityUser
    {
        [StringLength(100)]
        public string? FullName { get; set; }

        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
        public int? Age { get; set; }

        public string? Gender { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public double? Weight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Height must be a positive number.")]
        public double? Height { get; set; }

        [StringLength(200)]
        public string? DietaryRestrictions { get; set; }

        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
        public ICollection<Intake> Intakes { get; set; } = new List<Intake>();
        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}
