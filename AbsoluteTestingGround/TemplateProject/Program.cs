namespace TemplateProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class LocalCSharpTestRunner
    {
        public static void Main()
        {
            SampleContext context = new SampleContext();
            context.ReferencedClasses.Count();
        }
    }
}
