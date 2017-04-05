using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using WebApplication2.Models;
using WebApplication2.Models.Enums;

namespace Reflections
{
    [TestFixture]
    public class CatsTest
    {
        [TestCase]
        public void TestCatsIndex()
        {
            var data = new List<Cat>
            {
                new Cat()
                {
                    Breed = "Long Hair",
                    Gender = Gender.Male,
                    Name = "Sharo"
                },
                new Cat()
                {
                    Breed = "Short Hair",
                    Gender = Gender.Male,
                    Name = "Belcho"
                },
                new Cat()
                {
                    Breed = "Fluffination",
                    Gender = Gender.Female,
                    Name = "Roshla"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cat>>();
            mockSet.As<IQueryable<Cat>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Cat>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cat>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cat>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Cats).Returns(mockSet.Object);

            var controller = new CatsController(mockContext.Object);
            var cats = (controller.Index() as ViewResult).Model;
            // Assert.IsInstanceOf<ViewResult>(cats);
            Assert.IsInstanceOf<IEnumerable<Cat>>(cats);
            Assert.AreEqual(data,cats);
        }
    }
}
