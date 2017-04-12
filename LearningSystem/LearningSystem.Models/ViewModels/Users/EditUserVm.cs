using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Models.ViewModels.Users
{
    public class EditUserVm
    {                               
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
