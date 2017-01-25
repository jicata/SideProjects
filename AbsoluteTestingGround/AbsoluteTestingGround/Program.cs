namespace AbsoluteTestingGround
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using NUnit.Engine;
    using NUnit.Engine.Agents;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using TestFilter = NUnit.Engine.TestFilter;

    public class Program
    {
        public static string Template = @" 
private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
                testsPassed++;
            else
                testsFailed++;            
        }

        //[ClassCleanup]
        //public static void ClassCleanUp()
        //{
        //    File.AppendAllText(@""D:\logged.txt"", ""Tests passed:"" + testsPassed + ""\n"");
        //    File.AppendAllText(@""D:\logged.txt"", ""Tests failed:"" + testsFailed + ""\n"");
        //}

        private static int testsPassed;
        private static int testsFailed;";

        public static void Main()
        {
            // set up the options
            string path = Assembly.GetExecutingAssembly().Location;
            TestPackage package = new TestPackage(path);
            package.AddSetting("WorkDirectory", Environment.CurrentDirectory);

            // prepare the engine
            ITestEngine engine = TestEngineActivator.CreateInstance();
            var _filterService = engine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = _filterService.GetTestFilterBuilder();
            TestFilter emptyFilter = builder.GetFilter();
            XmlDocument doc = new XmlDocument();
            using (ITestRunner runner = engine.GetRunner(package))
            {
                // execute the tests            
                XmlNode result = runner.Run(null, emptyFilter);
                Console.WriteLine(result.InnerText);
               
            }
            Console.ReadLine();
        }
        [TestFixture]
        public class MyTests
        {
            [Test]
            public void Testche1()
            {
                Assert.AreEqual(1, 1, "failed");
            }
            [Test]
            public void Testche2()
            {
                Assert.AreEqual(1,2,"failed");
            }
        }
    }


}

