using System.Collections.Generic;

namespace TestingTemplateOne
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using Microsoft.CSharp;


    class Program
    {
        static void Main(string[] args)
        {

            const string testRunnerCode = @"namespace LocalDefinedCSharpTestRunner
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class LocalCSharpTestRunner
    {
        private const string JsonTestsResult = @""{ """"stats"""" : { """"passes"""": #totalPasses# }, """"passing"""": [ #passingTests# ], """"failures"""" : [ { """"err"""": { """"message"""": """"#message#"""" } } ] }"";

        private class FuncTestResult
        {
            public int Index { get; set; }

            public bool Passed { get; set; }

            public string Error { get; set; }
        }

        private static readonly IReadOnlyCollection<Func<List<Type>, bool>> tests = new ReadOnlyCollection<Func<List<Type>, bool>>(new List<Func<List<Type>, bool>>
        { 
              types=>(types.Any(t=>t.Name == ""TestModel""))
        });

        public static void Main()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var allTypes = new List<Type>();

            foreach (var file in Directory.GetFiles(currentDirectory).Where(f => f.EndsWith("".dll"") || f.EndsWith("".exe"")))
            {
                Console.WriteLine(file);
                var assembly = Assembly.LoadFrom(file);
                foreach (var type in assembly.GetTypes())
                {

                    allTypes.Add(type);
                }

                var referenced = assembly.GetReferencedAssemblies().Where(r => r.Name != ""mscorlib"" && r.Name != ""System.Core"");
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
                        error = ""Test failed."";
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
                .Replace(""#totalPasses#"", results.Count(t => t.Passed).ToString())
                .Replace(""#passingTests#"", string.Join("","", results.Where(t => t.Passed).Select(t => t.Index).ToList()))
                .Replace(""#message#"", string.Join("", "", results.Where(t => t.Error != null).Select(t => t.Error.Replace(""\"""", string.Empty)).ToList()));

            Console.WriteLine(jsonResult);
            Console.ReadLine();
        }
    }
}";
            const string outputDirPath =
          @"C:\Users\Maika\Documents\Programming\SideProjects\WorkingWIthTemplateCode\TestingTemplateOne";
            string currentDir = Environment.CurrentDirectory;
            string[] allFiles = Directory.GetFiles(currentDir);
            foreach (var file in allFiles)
            {
                int posisitionOfLastSeparator = file.LastIndexOf(@"\") + 1;
                Console.WriteLine(file.Substring(posisitionOfLastSeparator));
            }

            var compiler = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters();
            compilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            compilerParameters.ReferencedAssemblies.AddRange(Directory.GetFiles(outputDirPath).Where(f => f.EndsWith(".dll") || f.EndsWith(".exe")).ToArray());
            compilerParameters.GenerateInMemory = false;
            compilerParameters.GenerateExecutable = true;
            var compilerResult = compiler.CompileAssemblyFromSource(compilerParameters, testRunnerCode);
            foreach (var result in compilerResult.Errors)
            {
                Console.WriteLine(result);
            }
            var outputAssemblyPath = outputDirPath + "\\LocalTestRunner.exe";
            Console.WriteLine(compilerResult.PathToAssembly);
            File.Move(compilerResult.PathToAssembly, outputAssemblyPath);

            IReadOnlyCollection<Func<List<Type>, bool>> tests = new ReadOnlyCollection<Func<List<Type>, bool>>(new List<Func<List<Type>, bool>>
            {
                 types=>(types.Any(t=>t.Name == "TestModel")),
            });
            }
    }
}
