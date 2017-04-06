﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using WebApplication2;
using WebApplication2.Models;
using WebApplication2.Models.Enums;


namespace Reflections
{
    [TestFixture]
    public class CatsTest
    {
        private Mock<HttpContextBase> moqContext;
        private Mock<HttpRequestBase> moqRequest;
        private NameValueCollection formValues;

        [OneTimeSetUp]
        public void SetupTests()
        {
            moqContext = new Mock<HttpContextBase>();
            moqRequest = new Mock<HttpRequestBase>();
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            formValues = new NameValueCollection
            {
                { "FirstName", "Jonathan" },
                { "LastName", "Danylko" }
            };
        }
        public Type ContextType { get; set; }

        private void FindTheTypeOfDb()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var referenced = assembly.GetReferencedAssemblies();

            foreach (var assemblyName in referenced)
            {
                if (!Assembly.Load(assemblyName).GlobalAssemblyCache)
                {
                    assembly = Assembly.Load(assemblyName);
                }
            }
            var types = assembly.GetTypes();
            Type dbType = null;
            foreach (Type type in types)
            {

                if (type.IsSubclassOf(typeof(DbContext)))
                {
                    dbType = type;
                    break;
                }
            }

            object dbcontext = Activator.CreateInstance(dbType);
            dynamic context = dbcontext;
        }
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
            Assert.AreEqual(data, cats);
        }

        [TestCase]
        public void MoqRoutingTest()
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            moqRequest.Setup(e => e.AppRelativeCurrentExecutionFilePath).Returns("~/Home/Index");
            // Act
            RouteData routeData = routes.GetRouteData(moqContext.Object);
            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [TestCase]
        public void MoqFormsTest()
        {
            // Arrange            
            moqRequest.Setup(r => r.Form).Returns(formValues);
            // Act
            var forms = moqContext.Object.Request.Form;
            // Assert
            Assert.IsNotNull(forms);
            Assert.AreEqual("Jonathan", forms["FirstName"]);
            Assert.AreEqual("Danylko", forms["LastName"]);
        }
    }
}
