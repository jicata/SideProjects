using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Construction;

namespace CsProjReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows\Temp\2c34j1mh.b4l - Copy\WebApplication2\WebApplicationTRUE.csproj";
            var project = new Project(path);
            Console.WriteLine(project.FullPath);

            var targets = project.Targets.ContainsKey("EnsureNuGetPackageBuildImports");
            if (targets)
            {
                XmlDocument xdDoc = new XmlDocument();
                xdDoc.Load(project.FullPath);
                XmlNamespaceManager xnManager =
                    new XmlNamespaceManager(xdDoc.NameTable);
                xnManager.AddNamespace("tu",
                 "http://schemas.microsoft.com/developer/msbuild/2003");

                XmlNode xnRoot = xdDoc.DocumentElement;
                var xnlPages = xnRoot.SelectSingleNode("//tu:Target[@Name='EnsureNuGetPackageBuildImports']", xnManager);
                xnRoot.RemoveChild(xnlPages);
                xdDoc.Save(project.FullPath);
                xdDoc.Save("D:amane.csproj");
            }
            project.Save(path);

        }
    }
}
