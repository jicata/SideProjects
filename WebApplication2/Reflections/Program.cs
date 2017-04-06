using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

            string workingDir = @"C:\ASP-Projects\ShishaKingdom";
            string pattern = @"*.csproj";

            var result = FindFirstFileMatchingPattern(workingDir, pattern);
            string dllPath = @"C:\ASP-Projects\ShishaKingdom\ShishaKingdom.Web\bin\ShishaKingdom.Web.dll";


            var assembly = Assembly.LoadFrom(dllPath);
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
            var properties = dbcontext.GetType().GetProperties();
            string[] expectedModels = new[] {"Products", "Categories"};
            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType.Name.Contains("DbSet");
                //Console.WriteLine(propertyInfo.PropertyType.Name);
                if (propertyType && expectedModels.Any(m=>m.Contains(propertyInfo.Name)))
                {
                    var isGeneric = propertyInfo.PropertyType.IsGenericType;
                    Type t = propertyInfo.PropertyType.GetGenericArguments()[0];
                    foreach (var property in t.GetProperties())
                    {
                        if (!property.PropertyType.IsPrimitive)
                        {
                            Console.WriteLine(property.PropertyType);
                        }
                       
                    }
                }
            }

        }
        public static string FindFirstFileMatchingPattern(string workingDirectory, string pattern)
        {
            var files = new List<string>(
                Directory.GetFiles(
                    workingDirectory,
                    pattern,
                    SearchOption.AllDirectories));
            if (files.Count == 0)
            {
                throw new ArgumentException(
                    $"'{pattern}' file not found in output directory!",
                    nameof(pattern));
            }
            if (files.Count > 1)
            {
                files[0] = files.OrderByDescending(f => new FileInfo(f).Length).FirstOrDefault();
            }

            return files[0];
        } 
    }

}
