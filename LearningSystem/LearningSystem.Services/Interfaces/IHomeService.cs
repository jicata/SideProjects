using System.Collections.Generic;
using LearningSystem.Models.ViewModels.Courses;

namespace LearningSystem.Services.Interfaces
{
    public interface IHomeService : IService
    {
        IEnumerable<CourseVm> GetAllCourses();
    }
}