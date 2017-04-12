using LearningSystem.Data;

namespace LearningSystem.Services
{
    public abstract class Service
    {
        public Service()
        {
            this.Context = new LearningSystemContext();
        }

        protected LearningSystemContext Context { get; }
    }
}
