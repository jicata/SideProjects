namespace AbsoluteTestingGround
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Principal;

    class Program
    {
        static void Main()
        {
            // path to the compiled UserCode
            string path = @"D:\Install\CompiledUserSubmission.exe";

            // load its assembly
            var assembly = Assembly.LoadFrom(path);

            // Loop through its types
            foreach (var type in assembly.GetTypes())
            {
                Console.WriteLine(type.Name);
                
            }

            // get the assemblies referenced by that assembly
            var referencedAssemblies =
                assembly.GetReferencedAssemblies().Where(r => r.Name != "mscorlib" && r.Name != "System.Core");

            // loop through each
            foreach (var referencedAssembly in referencedAssemblies)
            {
                // load it and loop through its types
                var refAssembly = Assembly.Load(referencedAssembly);
                foreach (var refType in refAssembly.GetTypes())
                {
                    Console.WriteLine(refType.Name);
                }
            }
        }
    }

}

