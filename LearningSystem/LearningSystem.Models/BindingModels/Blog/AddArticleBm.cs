using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Models.BindingModels.Blog
{
    public class AddArticleBm
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
