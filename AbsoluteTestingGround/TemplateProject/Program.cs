namespace TemplateProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    public class LocalCSharpTestRunner
    {
        public static void Main()
        {
            string unitTestProjectPath =
                @"D:\CSharpUnitTestsRunnerTestingFolder\TestFolder00\CompiledUserSubmission.dll";
            string vsTestRunnerPath =
                @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe";
            ProcessStartInfo pinfo1 = new ProcessStartInfo(vsTestRunnerPath, unitTestProjectPath);
            pinfo1.CreateNoWindow = true;
            pinfo1.WindowStyle = ProcessWindowStyle.Hidden;
            pinfo1.UseShellExecute = false;
            pinfo1.RedirectStandardOutput = true;
           
            using (Process process = Process.Start(pinfo1))
            {
                if (System.Environment.OSVersion.Version.Major >= 6)
                {
                    process.StartInfo.Verb = "runas";
                    Console.WriteLine("ye");
                }
                Console.WriteLine(process.StandardOutput.ReadToEnd());

                //using (StreamReader reader = process.StandardOutput)
                //{
                //    string result = reader.ReadToEnd();
                //    Console.Write(result);
                //    File.AppendAllText(@"D:\CSharpUnitTestsRunnerTestingFolder\log2.txt", result);
                //}
            }
        }
    }
}
