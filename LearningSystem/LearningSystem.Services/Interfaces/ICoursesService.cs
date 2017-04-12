using LearningSystem.Models.ViewModels.Courses;

namespace LearningSystem.Services.Interfaces
{
    public interface ICoursesService
    {
        DetailsCourseVm GetDetails(int id);
    }
}