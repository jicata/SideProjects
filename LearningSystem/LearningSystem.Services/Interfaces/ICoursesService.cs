using LearningSystem.Models.ViewModels.Courses;

namespace LearningSystem.Services.Interfaces
{
    public interface ICoursesService : IService
    {
        DetailsCourseVm GetDetails(int id);
    }
}