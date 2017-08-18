namespace EntityFrameworkCore
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Xml;


    class Program
    {

       
        static void Main()
        {
            ProcessStartInfo pinfo = new ProcessStartInfo(@"C:\Program Files\dotnet\dotnet.exe")
            {
                Arguments = "restore C:\\SideAndTestProjects\\OneMoreTime\\OneMoreTime\\OneMoreTime.csproj",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                ErrorDialog = false,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.Default
            };
          
            var wat = Process.Start(pinfo);
            string aha = wat.StandardOutput.ReadToEnd();
            Console.WriteLine(aha);


        }
    }
}
