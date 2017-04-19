using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels.Admin;
using LearningSystem.Models.ViewModels.Courses;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Services
{
    public class AdminService : Service, IAdminService
    {
        public AdminService(LearningSystemContext context) : base(context)
        {
        }

        public AdminPageVm GetAdminPage()
        {
            AdminPageVm page = new AdminPageVm();
            IEnumerable<Course> courses = this.Context.Courses;
            IEnumerable<Student> students = this.Context.Students;

            IEnumerable<CourseVm> courseVms = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseVm>>(courses);
            IEnumerable<AdminPageUserVm> userVms =
                Mapper.Map<IEnumerable<Student>, IEnumerable<AdminPageUserVm>>(students);

            page.Users = userVms;
            page.Courses = courseVms;

            return page;
        }
    }
}

