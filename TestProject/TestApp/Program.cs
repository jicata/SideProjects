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
        private const string FileNameAndTypeIndicatorPattern = @"##(\w+\.(cpp|h))##";
        static void Main(string[] args)
        {


            var testResultsRegex = new Regex(@"Test Count: (\d+), Passed: (\d+), Failed: (\d+), Warnings: \d+, Inconclusive: \d+, Skipped: \d+");
            string receivedOutput = @"Test Count: 2, Passed: 1, Failed: 1, Warnings: 0, Inconclusive: 0, Skipped: 0" +
                               Environment.NewLine +
                               "Test Count: 2222, Passed: 1, Failed: 1, Warnings: 0, Inconclusive: 0, Skipped: 0";
            var res = testResultsRegex.Matches(receivedOutput);            
            int totalTests = int.Parse(res[res.Count - 1].Groups[1].Value);
            int  passedTests = int.Parse(res[res.Count - 1].Groups[2].Value);
            Console.WriteLine($"total { totalTests}");
            Console.WriteLine($"passed {passedTests}");
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
