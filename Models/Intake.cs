using System;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.Models
{
    public class Intake
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string FoodItem { get; set; }

        [Required]
        public int Calories { get; set; }

        public int Protein { get; set; }

        public int Carbohydrates { get; set; }

        public int Fat { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
