namespace TemplateProject
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class LocalCSharpTestRunner
    {
        private const string JsonTestsResult = @"{ ""stats"" : { ""passes"": #totalPasses# }, ""passing"": [ #passingTests# ], ""failures"" : [ { ""err"": { ""message"": ""#message#"" } } ] }";

        private class FuncTestResult
        {
            public int Index { get; set; }

            public bool Passed { get; set; }

            public string Error { get; set; }
        }

        private static readonly IReadOnlyCollection<Func<List<Type>, bool>> tests = new ReadOnlyCollection<Func<List<Type>, bool>>(new List<Func<List<Type>, bool>>
        {
            (types) => types.Any(t=>t.Name=="CompletelyIrrelevantClass"),
            (types) => types.FirstOrDefault(t=>t.Name=="CompletelyIrrelevantClass").IsPublic,
            (types) => types.FirstOrDefault(t=>t.Name=="CompletelyIrrelevantClass").GetMethods().Any(m => m.Name == "IrrelevantMethod"),
            (types) => types.FirstOrDefault(t=>t.Name=="CompletelyIrrelevantClass").IsSubclassOf(types.FirstOrDefault(t=>t.Name=="SomewhatIrrelevantClass")),
            (types) => types.FirstOrDefault(t=>t.Name=="SomewhatIrrelevantClass").GetMembers().Any(m=>m.Name=="Id"),
            (types) => types.FirstOrDefault(t=>t.Name=="Person").GetFields(BindingFlags.Public | BindingFlags.Instance).Count() == 2,
            (types) =>
            {
                Type personType = types.FirstOrDefault(t => t.Name == "Person");
                ConstructorInfo emptyCtor = personType.GetConstructor(new Type[] {});
                ConstructorInfo ageCtor = personType.GetConstructor(new[] {typeof(int)});
                ConstructorInfo nameAgeCtor = personType.GetConstructor(new[] {typeof(string), typeof(int)});
                if (nameAgeCtor == null)
                {
                    nameAgeCtor = personType.GetConstructor(new[] {typeof(int), typeof(string)});
                }


                return (emptyCtor != null) && (ageCtor != null) && (nameAgeCtor != null);
            },
            (types) =>
            {
                Type personType = types.FirstOrDefault(t => t.Name == "Person");
                FieldInfo[] fields = personType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                MethodInfo[] methods = personType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                if (fields.Length != 1 || methods.Length != 5)
                {
                    return false;
                }

                string personName = "Pesho";
                ConstructorInfo cinfo = personType.GetConstructor(new[] {typeof(string)});
                var human = cinfo.Invoke(new object[] {personName});
                var methodResult = methods[0].Invoke(human, new object[] {});
                if (methodResult.ToString() == personName + " says Hello!")
                {
                    return true;
                }

                return false;

            }
        });

        public static void Main()
        {

            string unitTestProjectPath =
                @"C:\Users\Maika\Documents\Programming\SideProjects\AbsoluteTestingGround\TestUniting\TestUniting.dll";
            string vsTestRunnerPath =
                @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe";
            ProcessStartInfo pinfo1 =  new ProcessStartInfo(vsTestRunnerPath,unitTestProjectPath);
            pinfo1.CreateNoWindow = true;
            pinfo1.WindowStyle = ProcessWindowStyle.Hidden;
            pinfo1.UseShellExecute = false;
            pinfo1.RedirectStandardOutput = true;

            //string MstestExePath = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe";
            //string targetOutputPath = @"D:\results.xml";

            //ProcessStartInfo pinfo2 = new ProcessStartInfo(MstestExePath,$"/testcontainer:{unitTestProjectPath}");
            //pinfo2.CreateNoWindow = true;
            //pinfo2.WindowStyle=ProcessWindowStyle.Hidden;
            //pinfo2.UseShellExecute = false;
            //pinfo2.RedirectStandardOutput = true;
            using (Process process = Process.Start(pinfo1))
            {
                //
                // Read in all the text from the process with the StreamReader.
                //
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }

            return;
            var currentDirectory = Environment.CurrentDirectory;
            ReferencedClass classy = new ReferencedClass();
            classy.Id = 5;
            Console.WriteLine(classy.Id);
            var allTypes = new List<Type>();
            foreach (var file in Directory.GetFiles(currentDirectory).Where(f => f.EndsWith(".dll") || f.EndsWith(".exe")))
            {
                var assembly = Assembly.LoadFrom(file);
                foreach (var type in assembly.GetTypes())
                {
                    allTypes.Add(type);
                }

                var referenced = assembly.GetReferencedAssemblies().Where(r => r.Name != "mscorlib" && r.Name != "System.Core");
                foreach (var reference in referenced)
                {
                    var refAssembly = Assembly.Load(reference);
                    foreach (var type in refAssembly.GetTypes())
                    {
                        allTypes.Add(type);
                    }
                }
            }

            var results = new List<FuncTestResult>();
            var index = 0;
            foreach (var test in tests)
            {
                var result = false;
                string error = null;
                try
                {
                    result = test(allTypes);
                    if (!result)
                    {
                        error = "Test failed.";
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }

                results.Add(new FuncTestResult { Passed = result, Error = error, Index = index });
                index++;
            }

            var jsonResult = JsonTestsResult
                .Replace("#totalPasses#", results.Count(t => t.Passed).ToString())
                .Replace("#passingTests#", string.Join(",", results.Where(t => t.Passed).Select(t => t.Index).ToList()))
                .Replace("#message#", string.Join(", ", results.Where(t => t.Error != null).Select(t => t.Error.Replace("\"", string.Empty)).ToList()));

            Console.WriteLine(jsonResult);
        }
    }
}
