namespace BreakJudge
{
    using System;
    using System.Diagnostics;
    using System.IO;

    class Program
    {
        public static void Main()
        {
            string path = @"C:\Users\Maika\AppData\Local\Microsoft\Windows\INetCache\Low\BreakJudge.exe";
            //ProcessStartInfo pinfo = new ProcessStartInfo(path);
            Process.Start(path);
           
        }
    }
}
