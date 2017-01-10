namespace AbsoluteTestingGround
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;

    class Program
    {
        static void Main()
        {
            byte[] dataToWrite = new byte[8];
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, dataToWrite);
            File.Move(tempFilePath, @"D:\JSTestingTools\CSharo\PowerCollections\kur.txt");
            IReadOnlyCollection<Func<List<Type>, bool>> tests = new ReadOnlyCollection<Func<List<Type>, bool>>(new List<Func<List<Type>, bool>>
            {
                (kur)=>kur.Contains(typeof(string))
            });
            return;
            string dirParth = tempFilePath.Substring(0, tempFilePath.LastIndexOf(@"\"));
            string[] files = Directory.GetFiles(dirParth);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine(tempFilePath);
            File.Delete(tempFilePath);
        }
    }


    namespace LocalDefinedCSharpTestRunner
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
# allTests#
            });

            public static void Main()
            {
                var currentDirectory = Environment.CurrentDirectory;

                var allTypes = new List<Type>();

                foreach (var file in Directory.GetFiles(currentDirectory).Where(f => f.EndsWith("".dll"") || f.EndsWith("".exe"")))
                {
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
            }
        }
    }
}

