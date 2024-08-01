using System;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.ViewModels
{
    public class IntakeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Food Item")]
        public string FoodItem { get; set; }

        [Required]
        [Display(Name = "Calories")]
        public int Calories { get; set; }

        [Display(Name = "Protein")]
        public int Protein { get; set; }

        [Display(Name = "Carbohydrates")]
        public int Carbohydrates { get; set; }

        [Display(Name = "Fat")]
        public int Fat { get; set; }

        public int UserProfileId { get; set; }
        public string UserProfileName { get; set; }
    }
}
