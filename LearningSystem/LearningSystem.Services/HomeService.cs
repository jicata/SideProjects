using System.Collections.Generic;
using AutoMapper;
using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels.Courses;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Services
{
    public class HomeService : Service, IHomeService
    {
        public HomeService(ILearningSystemContext context) : base(context)
        {
        }

        public IEnumerable<CourseVm> GetAllCourses()
        {
            IEnumerable<Course> courses = this.Context.Courses;
            IEnumerable<CourseVm> vms = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseVm>>(courses);
            return vms;
        }
    }
}
