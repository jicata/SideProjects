using LearningSystem.Models.EntityModels;

namespace LearningSystem.Services.Interfaces
{
    public interface IAccountService : IService
    {
        void CreateStudent(ApplicationUser user);
    }
}