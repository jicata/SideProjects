namespace AbsoluteTestingGround
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Microsoft.Build.Evaluation;
    using System.Collections;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class Program
    {

        public static void Main()
        {

            string x = "kurche";
            Func<string> func = () =>
            {
                string y = x;
                return y;
            };
            x = null;
            string resultttt =func.Invoke();
            Console.WriteLine(resultttt);

            return;

            string nUnitConsole =
                @"C:\Users\Maika\Documents\Programming\SideProjects\AbsoluteTestingGround\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe";
            string arguments = string.Empty;

            string file = @"D:\CSharpUnitTestsRunnerTestingFolder\bin\Debug\TestThisAc.dll";
            arguments += file + " ";
            arguments += "-inprocess ";
            arguments += "-noresult ";
    


            ProcessStartInfo pinfo1 = new ProcessStartInfo(nUnitConsole, arguments);
            pinfo1.CreateNoWindow = false;
            pinfo1.WindowStyle = ProcessWindowStyle.Hidden;
            pinfo1.UseShellExecute = false;
            pinfo1.RedirectStandardOutput = true;

            Process process = Process.Start(pinfo1);

            //
            // Read in all the text from the process with the StreamReader.
            //
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                //Regex errorRegex =
                //    new Regex(
                //        $@"\d+\)(.*){Environment.NewLine}((?:.|{Environment.NewLine})*?){Environment.NewLine}\s*at \w+(\.[^\.{Environment
                //            .NewLine}]*)*?\(\)");
                //Regex testResultsRegex =
                //    new Regex(
                //        @"Test Count: (\d+), Passed: (\d+), Failed: (\d+), Warnings: \d+, Inconclusive: \d+, Skipped: \d+");
                //var res = testResultsRegex.Match(result);
                //var errors = errorRegex.Matches(result);
                //var totalTests = res.Groups[1].Value;
                //var passedTests = res.Groups[2].Value;
                //var failedTests = res.Groups[3].Value;
                //Console.WriteLine($"Total Tests: {totalTests} " +
                //                  $"PassedTests: {passedTests} " +
                //                  $"FailedTests: {failedTests} ");
                //foreach (Match error in errors)
                //{
                //    var errorMethod = error.Groups[1].Value;
                //    var cause = error.Groups[2].Value.Replace(Environment.NewLine, "");
                //    Console.WriteLine($"{errorMethod} {cause}");
                //}
                Console.WriteLine(result);
            }

        }

        public static void PrintPathAndAssemblyName(string assemblyPath)
        {
            string path = @"D:\PathName123.txt";
            File.AppendAllText(path, $"Path: {assemblyPath}; {Environment.NewLine} AssemblyName:{AssemblyName.GetAssemblyName(assemblyPath)} {Environment.NewLine}{Environment.NewLine}");
        }
        
    }
}

