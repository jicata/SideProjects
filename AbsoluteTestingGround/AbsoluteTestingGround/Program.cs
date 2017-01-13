namespace AbsoluteTestingGround
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Principal;

   public  class Program
    {
        public static void Main()
        {
            // path to the compiled UserCode
            string path = @"D:\Install\CompiledUserSubmission.exe";

            List<Type> types = new List<Type>();

            // load its assembly
            var assembly = Assembly.LoadFrom(path);

            // Loop through its types
            foreach (var type in assembly.GetTypes())
            {
                Console.WriteLine(type.Name);
                types.Add(type);

            }

            //get the assemblies referenced by that assembly
            var referencedAssemblies =
               assembly.GetReferencedAssemblies().Where(r => r.Name != "mscorlib" && r.Name != "System.Core");

            // loop through each
            foreach (var referencedAssembly in referencedAssemblies)
            {
                //Console.WriteLine(referencedAssembly);
                // load it and loop through its types
                var refAssembly = Assembly.Load(referencedAssembly);
                foreach (var refType in refAssembly.GetTypes())
                {
                    Console.WriteLine(refType);
                    types.Add(refType);
                }
            }
            IReadOnlyCollection<Func<List<Type>, bool>> tests = new ReadOnlyCollection<Func<List<Type>, bool>>(new List<Func<List<Type>, bool>>
             {
                    (x)=>types.Any(t=>t.Name=="CompletelyIrrelevantClass"),
                    (y)=>types.FirstOrDefault(t=>t.Name=="CompletelyIrrelevantClass").IsPublic,
                    (z)=>types.FirstOrDefault(t=>t.Name=="CompletelyIrrelevantClass").GetMethods().Any(m => m.Name == "IrrelevantMethod"),
                    (a)=>types.FirstOrDefault(t=>t.Name=="CompletelyIrrelevantClass").IsSubclassOf(a.FirstOrDefault(t=>t.Name=="SomewhatIrrelevantClass")),
                    (b)=>types.FirstOrDefault(t=>t.Name=="SomewhatIrrelevantClass").GetMembers().Any(m=>m.Name=="Id")
            });

            //(y) =>
            //{
            //    var thing = y.FirstOrDefault(t => t.Name == "CompletelyIrrelevantClass");
            //    if (thing != null)
            //    {
            //        return thing.GetMethods().Any(m => m.Name == "IrrelevantMethod");

            //    }
            //    return false;
            //},
            //foreach (var test in tests)
            //{
            //    bool result = test(types);
            //    Console.WriteLine(result);
            //}
            foreach (var test in tests)
            {
                var result = false;
                string error = null;
                try
                {
                    result = test(types);
                    if (!result)
                    {
                        Console.WriteLine("Test Failed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(result);
                
            }


        }

       public static int Whatever()
       {
           return 1 + 1;
       }
    }

}

