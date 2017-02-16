namespace TemplateProject
{
    using System.Data.Entity;

    public class SampleContext : DbContext
    {
        public SampleContext()
            : base(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TemplateProject.SampleContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public DbSet<ReferencedClass> ReferencedClasses { get; set; }
    }
}