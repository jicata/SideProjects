namespace LetsTestSomeMore
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TemplateProject;

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

    }
}
