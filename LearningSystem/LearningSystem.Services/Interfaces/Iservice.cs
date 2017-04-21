using LearningSystem.Data;

namespace LearningSystem.Services.Interfaces
{
    public interface IService
    {
        ILearningSystemContext Context { get; }
    }
}
