using LearningSystem.Models.ViewModels.Admin;

namespace LearningSystem.Services.Interfaces
{
    public interface IAdminService : IService
    {
        AdminPageVm GetAdminPage();
    }
}