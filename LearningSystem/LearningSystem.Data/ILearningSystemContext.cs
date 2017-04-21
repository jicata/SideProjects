using System.Data.Entity;
using LearningSystem.Models.EntityModels;

namespace LearningSystem.Data
{
    public interface ILearningSystemContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        DbSet<Student> Students { get; set; }

        DbSet<Course> Courses { get; set; }

        DbSet<Article> Articles { get; set; }

        int SaveChanges();
    }
}
