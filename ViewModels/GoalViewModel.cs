using System;
using System.ComponentModel.DataAnnotations;

namespace NutriPlan.ViewModels
{
    public class GoalViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsAchieved { get; set; }

        [Required]
        public int UserProfileId { get; set; }
        
        public string UserProfileName { get; set; }
    }
}
