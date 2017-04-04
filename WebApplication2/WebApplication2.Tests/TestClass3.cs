using System.Web.Mvc;
using NUnit.Framework;
using ShishaKingdom.Web.Controllers;

namespace WebApplication2.Tests
{
   public  class TestClass3
    {

        [TestCase]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
