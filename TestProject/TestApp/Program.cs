using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace TestApp
{
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Linq;
    using Microsoft.Build.Evaluation;
    using Wintellect.PowerCollections;


    class Program
    {
        private const string FileNameAndTypeIndicatorPattern = @"(@PropertySources?\((?:.*?)\))";
        protected const string DependenciesNodePath = @"//pomns:dependencies/pomns:dependency[pomns:groupId='javax.el']";
        protected const string StartClassNodePath = @"//pomns:start-class";
        protected const string MicrosoftCsProjXmlNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        protected const string NuGetXmlNodeXPath = @"//msns:Import[@Project='$(VSToolsPath)\WebApplications\Microsoft.WebApplication.targets']";

        static void Main(string[] args)
        {
            string csprojFilePath = @"C:\SideAndTestProjects\WebApplication2\WebApplication.csproj";
            Project pr = new Project(csprojFilePath);

        //    var wat = pr.AllEvaluatedProperties.FirstOrDefault(x => x.Name == "AssemblyName");

            var ye = pr.FullPath;
            string getProjectName = ExtractProjectNameFromCsProjfile(ye);
            var kur = pr.SetProperty("AssemblyName", getProjectName);
            pr.Save();


            OrderedBag<string> baggy = new OrderedBag<string>();
            return;
            ProcessStartInfo psi = new ProcessStartInfo(@"C:\Ruby24-x64\bin\ruby.exe", @"C:\SideAndTestProjects\Ruby\first.rb");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            Process process = new Process();
            process.StartInfo = psi;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(output);
            return;
            

            string csprojPath = @"C:\Users\Administrator\Downloads\New folder\LearningSystem\LearningSystem.Web.csproj";
            Project project = new Project(csprojPath);

            var vsToolsImport =
                project.Imports.Any(
                    i =>
                        i.ImportingElement.Project ==
                        "$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets");

            XmlDocument csprojXml = new XmlDocument();
            csprojXml.Load(csprojPath);
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(csprojXml.NameTable);
            namespaceManager.AddNamespace("msns", MicrosoftCsProjXmlNamespace);

            XmlNode rootNode = csprojXml.DocumentElement;
            var nuGetTargetNode = rootNode.SelectSingleNode(NuGetXmlNodeXPath, namespaceManager);
          //  rootNode.RemoveChild(nuGetTargetNode);
            csprojXml.Save(csprojPath);

            string pomXmlPath = @"C:\SideAndTestProjects\JavaORM\photography-workshops\pom.xml";
            string pomXmlNamepace = @"http://maven.apache.org/POM/4.0.0";


            //XmlDocument doc = new XmlDocument();
            //doc.Load(pomXmlPath);

            //XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            //namespaceManager.AddNamespace("pomns", pomXmlNamepace);

            //XmlNode rootNode= doc.DocumentElement;
            //var dependenciesNode = rootNode.SelectSingleNode(DependenciesNodePath, namespaceManager);

            //foreach (XmlNode childNode in dependenciesNode.ChildNodes)
            //{
            //    foreach (XmlNode innerDependencyProperties in childNode.ChildNodes)
            //    {
            //        if (innerDependencyProperties.LocalName == "groupId")
            //        {
            //            if (innerDependencyProperties.InnerText.Contains("javax.el"))
            //            {                                 
                                                          
            //            }                                 
            //            if (innerDependencyProperties.InnerText.Contains("junit"))
            //            {                                  
                                                           
            //            }                                  
            //            if (innerDependencyProperties.InnerText.Contains("org.hsqldb"))
            //            {
                            
            //            }
            //        }
            //    }   
            //}
            //XmlNode dependencyNode = doc.CreateNode(XmlNodeType.Element, "dependency", pomXmlNamepace);

            //XmlNode groupId = doc.CreateNode(XmlNodeType.Element, "groupId", pomXmlNamepace);
            //groupId.InnerText = "javaxxxxx.el";
            //XmlNode artifactId = doc.CreateNode(XmlNodeType.Element, "artifactId", pomXmlNamepace);
            //artifactId.InnerText = "el-api";

            //dependencyNode.AppendChild(groupId);
            //dependencyNode.AppendChild(artifactId);

            //dependenciesNode.AppendChild(dependencyNode);
            //doc.Save("D:\\example.xml");
            return;


   
            string zipPath = @"D:\_$submission";
            string fileToInsert = @"D:\OjsFiles\ipr.reg";
            string pathInside = @"src/main/test/com/photographyworkshops/";
            using (var zip = new ZipFile(zipPath))
            {
                zip.UpdateFile(fileToInsert, pathInside);
                zip.Save();
            }
            return;
            string path = @"C:\SideAndTestProjects\testFile.txt";
            string delimiter = $@"~~!!!==#==!!!~~{Environment.NewLine}";


            var file = File.ReadAllText(path);
            string[] headersAndCppFiles = file.Split(new string[] {delimiter}, StringSplitOptions.RemoveEmptyEntries);
            Regex fileNameAndTypeMatcher = new Regex(FileNameAndTypeIndicatorPattern);
            foreach (var headersAndCppFile in headersAndCppFiles)
            {
                var match = fileNameAndTypeMatcher.Match(headersAndCppFile);
                Console.WriteLine(match.Groups[1]);
                Console.WriteLine(match.Groups[2]);
                Console.WriteLine("-----------------------");
            }


            string zippath = @"C:\SideAndTestProjects\GenericCPlusPlusProject\main.zip";
            List<string> incomingFiles = new List<string>()
            {
                @"C:\SideAndTestProjects\GenericCPlusPlusProject\gosho.cpp",
                @"C:\SideAndTestProjects\GenericCPlusPlusProject\gosho.h",
                @"C:\SideAndTestProjects\GenericCPlusPlusProject\pesho.cpp",
                @"C:\SideAndTestProjects\GenericCPlusPlusProject\pesho.h"
            };

            Dictionary<string, string> destinationPathsInZip = new Dictionary<string, string>();
            using (ZipFile zip = ZipFile.Read(zippath))
            {
                foreach (var zipEntryFileName in zip.EntryFileNames)
                {
                    string pathInZip = string.Empty;
                    int indexOfLastSlash = zipEntryFileName.LastIndexOf("/");
                    if (indexOfLastSlash != -1)
                    {
                        string fileNameInsideOfZip = zipEntryFileName.Substring(indexOfLastSlash+1);
                        pathInZip = zipEntryFileName;
                        if (incomingFiles.Any(i => Path.GetFileName(i) == fileNameInsideOfZip))
                        {
                        }
                    }
                    
                    zip.UpdateFile(@"C:\SideAndTestProjects\GenericCPlusPlusProject\pesho.cpp", pathInZip);
                    zip.Save();
                }
            }
        }

        private static string ExtractProjectNameFromCsProjfile(string ye)
        {
            var indexOfLastSlash = ye.LastIndexOf("\\");
            var result = ye.Substring(indexOfLastSlash + 1);
            result = result.Replace(".csproj", "");
            return result;
        }
    }
}
