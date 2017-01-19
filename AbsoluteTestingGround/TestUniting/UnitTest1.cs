using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUniting
{
    using TemplateProject;
    using TemplateProject.DrugFolder;

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            ReferencedClass reffy = new ReferencedClass();
            reffy.Id = 5;
            Assert.AreEqual(5,reffy.Id);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string message = ForeignClass.message;
            Assert.AreEqual("im foreign", message);


        }
        [TestMethod]
        public void TestMethod3()
        {
            MyClas clas = new MyClas();
        }
    }
}
