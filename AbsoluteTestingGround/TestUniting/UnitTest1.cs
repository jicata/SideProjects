
namespace TestUniting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TemplateProject;
    using TemplateProject.DrugFolder;


    [TestClass]
    //[DeploymentItem(null,null)]
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
            string[] arr = new string[5];
            Assert.AreEqual(1,2,"FUCKK");
        }  

    }
}
