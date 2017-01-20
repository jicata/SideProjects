namespace AbsoluteTestingGround
{
    using System;
    using System.CodeDom.Compiler;
    using System.IO;
    using Ionic.Zip;
    using Microsoft.CSharp;

    public  class Program
    {
        public static void Main()
        {
            string outputAssemblyPath = @"D:\Install\Reference.dll";
            string code = File.ReadAllText(@"D:\Install\References.cs");
            var compiler = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters();
            compilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            compilerParameters.GenerateInMemory = false;
            compilerParameters.GenerateExecutable = false;
            compilerParameters.OutputAssembly = "Reference.dll";
            var compilerResult = compiler.CompileAssemblyFromSource(compilerParameters, code);
            foreach (CompilerError error in compilerResult.Errors)
            {
                Console.WriteLine(error.ErrorText);
            }
            File.Move(compilerResult.PathToAssembly, outputAssemblyPath);


           
        }
        
    }

}

