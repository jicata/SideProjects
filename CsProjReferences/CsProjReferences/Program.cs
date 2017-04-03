using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;

namespace CsProjReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows\Temp\2c34j1mh.b4l - Copy\WebApplication2\WebApplication2.csproj";
            var project = new Project(path);
            var imports = project.Imports;

            foreach (var resolvedImport in imports)
            {
                Console.WriteLine(resolvedImport.ImportedProject.FullPath);
            }

        }
    }
}
