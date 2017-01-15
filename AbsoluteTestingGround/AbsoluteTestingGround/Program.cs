namespace AbsoluteTestingGround
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using TemplateProject;

    public  class Program
    {
        public static void Main()
        {
            ReferencedClass rc = new ReferencedClass();
            rc.Id = 5;
            Console.WriteLine(rc.Id);
            Console.ReadLine();
        }
        
    }

}

