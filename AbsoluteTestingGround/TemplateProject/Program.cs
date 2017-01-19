namespace TemplateProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml;

    public class LocalCSharpTestRunner
    {

        public static void Main()
        {
            string inputPath = @"D:\CSharpUnitTestsRunnerTestingFolder\InitialProjectDirectory\TestUniting.csproj";
            XmlDocument xdDoc = new XmlDocument();
            xdDoc.Load(inputPath);

            XmlNamespaceManager xnManager =
             new XmlNamespaceManager(xdDoc.NameTable);
            xnManager.AddNamespace("tu",
             "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNode xnRoot = xdDoc.DocumentElement;
            XmlNode node = xnRoot.SelectSingleNode("//tu:ItemGroup", xnManager);
            XmlElement element = xdDoc.CreateElement("Reference", "http://schemas.microsoft.com/developer/msbuild/2003");
            element.SetAttribute("Include", "References.dll");
            node.AppendChild(element);
           
            xdDoc.Save(inputPath);
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