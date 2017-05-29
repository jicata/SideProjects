using LearningSystem.Data;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Services
{
    public abstract class Service : IService
    {
        protected Service(ILearningSystemContext context)
        {
            this.Context = context;
        }

        protected int Count { get; set; }

        public ILearningSystemContext Context { get; }
    }
}
