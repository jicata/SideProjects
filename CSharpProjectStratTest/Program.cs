using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PawInc.Models.Animals;

namespace PawInc
{
    using Microsoft.Build.Evaluation;
    using PawInc.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            //TextWriter writer = new StringWriter();
            //Console.SetOut(writer);
            //Dog dog = new Dog("balo",2,"doma",3);
            //dog.Bark();
            //return;
            string dirPath = @"C:\SideAndTestProjects\LearningSystem";
            string pattern = "*.csproj";

            string penis = FindFileMatchingPattern(dirPath, pattern,f=>new FileInfo(f).Length);
            Project pr = new Project(penis);
            Console.WriteLine(pr.DirectoryPath);

            Engine engine = new Engine();
            engine.Run();
        }
        public static string FindFileMatchingPattern<TDest>
            (string workingDirectory, string pattern, Func<string,TDest> orderBy = null) 
        {
            var files = new List<string>(
                Directory.GetFiles(
                    workingDirectory,
                    pattern,
                    SearchOption.AllDirectories));
            if (files.Count == 0)
            {
                throw new ArgumentException(
                    $"'{pattern}' file not found in output directory!",
                    nameof(pattern));
            }

            var test = files
                .OrderByDescending(orderBy);
            string discoveredFile = files
            .OrderByDescending(orderBy).First();

            return discoveredFile;
        }
    }
}
