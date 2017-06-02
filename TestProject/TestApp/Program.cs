using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace TestApp
{
    using System.Xml;
    using System.Xml.Linq;

    class Program
    {
        private const string FileNameAndTypeIndicatorPattern = @"(@PropertySources?\((?:.*?)\))";
        protected const string PropertiesNodePath = @"//pomns:properties/pomns:start-class";
        protected const string StartClassNodePath = @"//pomns:start-class";
        static void Main(string[] args)
        {

            decimal studioPrice = 1.1m;
            decimal nights = 3.3m;
            Console.WriteLine($"Studio: {(studioPrice * nights)} lv.");
            string ran = $"{(studioPrice * nights)}";
            return;
            string pomXmlPath = @"C:\SideAndTestProjects\JavaORM\photography-workshops\pom.xml";
            string pomXmlNamepace = @"http://maven.apache.org/POM/4.0.0";


            XmlDocument doc = new XmlDocument();
            doc.Load(pomXmlPath);

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("pomns", pomXmlNamepace);

            XmlNode rootNode = doc.DocumentElement;
            var nuGetTargetNode = rootNode.SelectSingleNode(PropertiesNodePath, namespaceManager);          
            Console.WriteLine(nuGetTargetNode.InnerText.Trim());
            //   var packageName = doc.Root.Element("properties").Element("start-class").Value;


   
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
    }
}
