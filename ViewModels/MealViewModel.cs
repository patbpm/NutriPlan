using System;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.ViewModels
{
    public class MealViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public int Calories { get; set; }

        public int MealPlanId { get; set; }
    }
}
