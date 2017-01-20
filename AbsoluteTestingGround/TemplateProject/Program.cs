namespace TemplateProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Microsoft.Build.Evaluation;
    using Microsoft.Build.Execution;
    public class LocalCSharpTestRunner
    {

        public static void Main()
        {

            int one = 1;
            Console.WriteLine(one.ToString("00"));
            return;
            string unitTestProjectPath =
                @"D:\TestUniting.dll.exe";
            string vsTestRunnerPath =
                @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe";
            ProcessStartInfo pinfo1 = new ProcessStartInfo(vsTestRunnerPath, unitTestProjectPath);
            pinfo1.CreateNoWindow = true;
            pinfo1.WindowStyle = ProcessWindowStyle.Hidden;
            pinfo1.UseShellExecute = false;
            pinfo1.RedirectStandardOutput = true;

            //string MstestExePath = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe";
            //string targetOutputPath = @"D:\results.xml";

            //ProcessStartInfo pinfo2 = new ProcessStartInfo(MstestExePath, $"/testcontainer:{unitTestProjectPath} /resultsfile:{targetOutputPath}");
            //pinfo2.CreateNoWindow = true;
            //pinfo2.WindowStyle = ProcessWindowStyle.Hidden;
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

        }
    }
}