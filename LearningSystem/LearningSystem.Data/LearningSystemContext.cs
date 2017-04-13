using LearningSystem.Models.EntityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSystem.Data
{
    using System.Data.Entity;

    public class LearningSystemContext : IdentityDbContext<ApplicationUser>
    {
        public LearningSystemContext()
            : base("data source=(LocalDb)\\MSSQLLocalDB;initial catalog=LearningSystem;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }     

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public static LearningSystemContext Create()
        {
            return new LearningSystemContext();
        }
    }
}