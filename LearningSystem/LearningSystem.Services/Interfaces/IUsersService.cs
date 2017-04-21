using LearningSystem.Models.BindingModels.Users;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels.Users;

namespace LearningSystem.Services.Interfaces
{
    public interface IUsersService : IService
    {
        Student GetCurrentStudent(string userName);
        void EnrollStudentInCourse(int courseId, Student student);
        ProfileVm GetProfileVm(string userName);
        EditUserVm GetEditVm(string userName);
        void EditUser(EditUserBm bind, string currentUsername);
    }
}