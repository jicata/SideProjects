using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace TestApp
{
    class Program
    {
        private const string FileNameAndTypeIndicatorPattern = @"(@PropertySources?\((?:.*?)\))";
        static void Main(string[] args)
        {

            string applicationProps = @"C:\SideAndTestProjects\JavaORM\application.properties";
            string zipPath = @"C:\SideAndTestProjects\JavaORM\photography-workshops.zip";
            using (var zip = new ZipFile(zipPath))
            {
                var entries = zip.EntryFileNames;
                string resourceDirectory = entries.Where(e => e.EndsWith("/test/")).OrderByDescending(s=>s.Length).FirstOrDefault();
               // zip.Entries.FirstOrDefault(f => f.FileName.EndsWith("main.java")).Extract();
                string notfile =
                    Directory.EnumerateFiles(Environment.CurrentDirectory, "main.java", SearchOption.AllDirectories)
                        .FirstOrDefault();
                string mainContents = File.ReadAllText(notfile);
                Regex rgx = new Regex(FileNameAndTypeIndicatorPattern);
                MatchCollection matches = rgx.Matches(mainContents);

                zip.UpdateItem(applicationProps,resourceDirectory);
                IEnumerable<string> paths = entries
                     .Where(x => !x.EndsWith("/") && x.EndsWith("java"))
                     .Select(x => x.Contains("main/java") ? x.Substring(x.LastIndexOf("main/java", StringComparison.Ordinal)+"main.java".Length+1) : x)
                     .Select(x => x.Contains(".") ? x.Substring(0, x.LastIndexOf(".", StringComparison.Ordinal)) : x)                    
                     .Select(x => x.Replace("/", "."));


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
