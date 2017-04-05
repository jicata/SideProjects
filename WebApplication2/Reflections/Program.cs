using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication2.Models;

namespace Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var referenced = assembly.GetReferencedAssemblies();
            
            foreach (var assemblyName in referenced)
            {
                if (!Assembly.Load(assemblyName).GlobalAssemblyCache)
                {
                  assembly = Assembly.Load(assemblyName);
                }
            }
            var types = assembly.GetTypes();
            Type dbType = null;
            foreach (Type type in types)
            {
                
                if (type.IsSubclassOf(typeof(DbContext)))
                {
                    dbType = type;
                    break;
                }
            }

            object dbcontext = Activator.CreateInstance(dbType);
            dynamic context = dbcontext;
            DbSet<Cat> cats = context.Cats;

            var catsList = cats.ToList();
            foreach (var cat in catsList)
            {
                Console.WriteLine(cat.Name);
            }
        }
    }
}
