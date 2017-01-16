using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUniting
{
    using System.IO;
    using TemplateProject;

    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        private TestContext m_testContext;
        public  ConsoleOutput cOut;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }


        [TestCleanup]
        public void TestCleanup()
        {
            this.cOut = new ConsoleOutput();
            if (this.m_testContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
               Console.WriteLine("DINGDONG");
               //File.WriteAllText("D:\\log.txt",Environment.CurrentDirectory);
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
          
        }
        [TestMethod]
        public void TestMethod2()
        {
            ReferencedClass classy = new ReferencedClass();
            classy.Id = 5;

            Assert.AreEqual(5,classy.Id);
           

        }
    }
}
