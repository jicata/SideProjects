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
            string path = @"D:\PathName.txt";

            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\nunit.framework\v4.0_3.6.0.0__2638cd05610744eb\nunit.framework.dll");
         
            //// --OWIN
            //// TODO: probably more OWIN libraries are required, test it out
            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Microsoft.Owin\v4.0_3.0.1.0__31bf3856ad364e35\Microsoft.Owin.dll");

            //// --EntityFramework
            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\EntityFramework\v4.0_6.0.0.0__b77a5c561934e089\EntityFramework.dll");
            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\EntityFramework.SqlServer\v4.0_6.0.0.0__b77a5c561934e089\EntityFramework.SqlServer.dll");

            //// --EFExtended
            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\EntityFramework.Extended\v4.0_6.0.0.0__05b7e29bdd433584\EntityFramework.Extended.dll");

            //// --Ninject
            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Ninject\v4.0_3.2.0.0__c7192dc5380945e7\Ninject.dll");

            //// --Automapper
            //// TODO: Figure autommaper out

            //// --Newton.JSON
            //PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Newtonsoft.Json\v4.0_9.0.0.0__30ad4fe6b2a6aeed\Newtonsoft.Json.dll");

            // -- MOQ
            PrintPathAndAssemblyName(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Moq\v4.0_4.2.1510.2205__69f491c39445e920\Moq.dll");

            return;
            //var project = new Project("D:\\CSharpUnitTestsRunnerTestingFolder\\LetsTestSomeMore.csproj");
            //var ye = project.Build();
            //Console.WriteLine(ye);
            //return;


            string nUnitConsole =
                @"C:\OJS\Open Judge System\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe";
            string arguments = string.Empty;

            string file = @"D:\CSharpUnitTestsRunnerTestingFolder\TestFolder00\CompiledUserSubmission.dll";
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
                Regex errorRegex =
                    new Regex(
                        $@"\d+\)(.*){Environment.NewLine}((?:.|{Environment.NewLine})*?){Environment.NewLine}\s*at \w+(\.[^\.{Environment
                            .NewLine}]*)*?\(\)");
                Regex testResultsRegex =
                    new Regex(
                        @"Test Count: (\d+), Passed: (\d+), Failed: (\d+), Warnings: \d+, Inconclusive: \d+, Skipped: \d+");
                var res = testResultsRegex.Match(result);
                var errors = errorRegex.Matches(result);
                var totalTests = res.Groups[1].Value;
                var passedTests = res.Groups[2].Value;
                var failedTests = res.Groups[3].Value;
                Console.WriteLine($"Total Tests: {totalTests} " +
                                  $"PassedTests: {passedTests} " +
                                  $"FailedTests: {failedTests} ");
                foreach (Match error in errors)
                {
                    var errorMethod = error.Groups[1].Value;
                    var cause = error.Groups[2].Value.Replace(Environment.NewLine, "");
                    Console.WriteLine($"{errorMethod} {cause}");
                }
            }

        }

        public static void PrintPathAndAssemblyName(string assemblyPath)
        {
            string path = @"D:\PathName123.txt";
            File.AppendAllText(path, $"Path: {assemblyPath}; {Environment.NewLine} AssemblyName:{AssemblyName.GetAssemblyName(assemblyPath)} {Environment.NewLine}{Environment.NewLine}");
        }

    }
}

