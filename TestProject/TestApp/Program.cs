using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace TestApp
{
    class Program
    {
        private const string FileNameAndTypeIndicatorPattern = @"##(\w+\.(cpp|h))##";
        static void Main(string[] args)
        {
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
            }
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
                    }

                    zip.UpdateFile(@"C:\SideAndTestProjects\GenericCPlusPlusProject\pesho.cpp", pathInZip);
                    zip.Save();
                }
            }
        }
    }
}
