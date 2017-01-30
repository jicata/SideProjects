
namespace LetsTestSomeMore
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TemplateProject.DrugFolder;

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Testy()
        {
            ForeignClass fc = new ForeignClass();
            string message = ForeignClass.message;
            Assert.AreEqual("I AM foreign", message, "got the wrong message, pal");
        }
    }
}
