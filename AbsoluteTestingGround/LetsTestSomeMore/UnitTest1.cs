namespace LetsTestSomeMore
{
    //using System;
    //using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TemplateProject;
    using TestInitialize = NUnit.Framework.SetUpAttribute;
    using TestContext = System.Object;
    using TestProperty = NUnit.Framework.PropertyAttribute;
    using TestClass = NUnit.Framework.TestFixtureAttribute;
    using TestMethod = NUnit.Framework.TestAttribute;
    using TestCleanup = NUnit.Framework.TearDownAttribute;
    using NUnit.Framework;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ReferencedClass rc = new ReferencedClass();
            rc.Id = 5;
            Assert.AreEqual(5, rc.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void thisIsGoNNaFailMarkMyWords()
        {
            string[] array = new string[3];
            array[5] = "failed";
        }
    }
}
