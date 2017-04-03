using System.Web.Mvc;
using NUnit.Framework;
using WebApplication2.Controllers;

namespace WebApplication2.Tests
{
    [TestFixture]
    public class TestClass1
    {
        [TestCase]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
