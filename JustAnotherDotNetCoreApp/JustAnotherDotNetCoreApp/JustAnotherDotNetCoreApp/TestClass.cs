
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

public class TestClass
    {
        public void theTest()
        {
            var assembly  = Assembly.GetEntryAssembly();
            var assemblyCount = assembly.DefinedTypes.Count();
            if (assemblyCount != 4)
            {
                throw new AbandonedMutexException("Nope");
            }
            Console.WriteLine("success");
        }
    }

