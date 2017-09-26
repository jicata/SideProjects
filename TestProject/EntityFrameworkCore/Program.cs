namespace EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {

        static void Main()
        {
            string dotNetCoreAssemblyPath =
                @"C:\SideAndTestProjects\NewDotNetCoreWebProject\NewDotNetCoreWebProject\bin\Debug\netcoreapp2.0\NewDotNetCoreWebProject.dll";
            string compiledPath = @"C:\Windows\Temp\k5r1l5ml.glh\NewDotNetCoreWebProject\CompileDir\NewDotNetCoreWebProject.dll";

            var currentDirectory = Environment.CurrentDirectory;

            var allTypes = new List<Type>();

            foreach (var file in Directory.GetFiles(currentDirectory).Where(f => f.EndsWith(".dll")))
            {
                var assembly = Assembly.LoadFrom(dotNetCoreAssemblyPath);
                foreach (var type in assembly.GetTypes())
                {
                    allTypes.Add(type);
                }

                var referenced = assembly
                    .GetReferencedAssemblies()
                    .Where(r => r.Name.StartsWith("Microsoft") && r.Name.StartsWith("System"));
                foreach (var reference in referenced)
                {
                    Console.WriteLine(reference);
                    var refAssembly = Assembly.Load(reference);
                    foreach (var type in refAssembly.GetTypes())
                    {
                        allTypes.Add(type);
                    }
                }
            }
            //var assembly = Assembly.LoadFrom(dotNetCoreAssemblyPath);
            //var types = assembly.GetTypes();


            //foreach (TypeInfo type in types)
            //{
            //    if (type.Name == "TestClass")
            //    {
            //        var activated = Activator.CreateInstance(type);
            //        var method = activated.GetType().GetMethods()[0];
            //        try
            //        {
            //            var invoked = method.Invoke(activated, null);
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e.Message);
            //        }
            //    }
            //}


            IReadOnlyCollection<Func<List<Type>, bool>> tests = new ReadOnlyCollection<Func<List<Type>, bool>>(
                new List<Func<List<Type>, bool>>

                {
                    (typess) =>
                    {
                        var testClass = typess.FirstOrDefault(t => t.Name == "TestClass");
                        var method = testClass.GetMethods()[0];
                        var activatedInstance = Activator.CreateInstance(testClass);
                        var methodResult = method.Invoke(activatedInstance, null);
                        return "heehhe" == methodResult.ToString();
                    },
                });

          
            
        
        }
    }
}