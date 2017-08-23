namespace EntityFrameworkCore
{
    using System;
    using System.Reflection;

    class Program
    {

        static void Main()
        {
            string dotNetCoreAssemblyPath =
                @"C:\SideAndTestProjects\JustAnotherDotNetCoreApp\JustAnotherDotNetCoreApp\JustAnotherDotNetCoreApp\bin\Debug\netcoreapp1.1\JustAnotherDotNetCoreApp.dll";

            string publishedDll =
                @"C:\SideAndTestProjects\JustAnotherDotNetCoreApp\JustAnotherDotNetCoreApp\JustAnotherDotNetCoreApp\bin\Debug\netcoreapp1.1\win10-x64\publish\JustAnotherDotNetCoreApp.dll";

            string standardDotNetFrameworkAssemblyPath =
                @"C:\SideAndTestProjects\TestProject\EntityFrameworkCore\bin\Debug\EntityFrameworkCore.exe";

            // var assembly = Assembly.LoadFrom(dotNetCoreAssemblyPath);
            var assembly = Assembly.LoadFrom(dotNetCoreAssemblyPath);
            var types = assembly.DefinedTypes;
            foreach (TypeInfo type in types)
            {
                if (type.Name == "TestClass")
                {
                    var activated = Activator.CreateInstance(type);
                    var method = activated.GetType().GetMethods()[0];
                    try
                    {
                        var invoked = method.Invoke(activated, null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
