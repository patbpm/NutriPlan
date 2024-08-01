using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.Models
{
    public class MealPlan
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }

    public class Meal
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public int Calories { get; set; }

        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
    }
}
