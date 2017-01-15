using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUniting
{
    using System.IO;

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
        private ConsoleOutput cOut;
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
               File.WriteAllText("D:\\log.txt",this.cOut.GetOuput());
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1,1);
        }
    }
}
