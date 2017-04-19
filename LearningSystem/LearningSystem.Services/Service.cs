using LearningSystem.Data;

namespace LearningSystem.Services
{
    public abstract class Service
    {
        protected Service(LearningSystemContext context)
        {
            this.Context = context;
        }

        protected LearningSystemContext Context { get; }
    }
}
