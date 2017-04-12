using System;
using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Models.ViewModels.Courses
{
    public class DetailsCourseVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
