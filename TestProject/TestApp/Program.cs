using System;
using System.IO;
using System.Text.RegularExpressions;

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

        }
    }
}
