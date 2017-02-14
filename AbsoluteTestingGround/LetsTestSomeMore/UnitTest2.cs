
namespace LetsTestSomeMore
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TemplateProject;
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
    
        [TestMethod]
        public void VenciVenc()
        {
            SampleContext context = new SampleContext();
            ReferencedClass classy = new ReferencedClass();
            context.ReferencedClasses.Add(classy);
            Assert.AreEqual(context.ReferencedClasses.Count(), 1);
        }
    }
}
