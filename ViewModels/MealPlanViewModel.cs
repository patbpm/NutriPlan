using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.ViewModels
{
    public class MealPlanViewModel
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
        public string UserProfileName { get; set; }

        public ICollection<MealViewModel> Meals { get; set; }
    }
}
