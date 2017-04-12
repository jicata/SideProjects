using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningSystem.Models.ViewModels.Courses;

namespace LearningSystem.Models.ViewModels.Admin
{
    public class AdminPageVm
    {
        public IEnumerable<CourseVm> Courses { get; set; }

        public IEnumerable<AdminPageUserVm> Users { get; set; }
    }
}
