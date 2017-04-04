using System.Web.Mvc;
using NUnit.Framework;
using ShishaKingdom.Web.Controllers;


namespace WebApplication2.Tests
{
    public class TestClass2
    {
        [TestCase]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}
