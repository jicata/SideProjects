using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using LearningSystem.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace LearningSystem.Tests
{
    [TestFixture]
    public class EnrollUserTest
    {
        private const string ControllerSuffix = "Controller";
        private const string ServiceAssemblyName = "LearningSystem.Services";

        private const string ContextInterfaceName = "ILearningSystemContext";
        private const string ControllerName = "Users";
        private const string ActionNameToTest = "Profile";

        private HttpContextBase rmContext;
        private HttpRequestBase rmRequest;
        private Mock<HttpContextBase> moqContext;
        private Mock<HttpRequestBase> moqRequest;
        private NameValueCollection formValues;

        [OneTimeSetUp]
        public void TestSetup()
        {
            moqContext = new Mock<HttpContextBase>();
            moqRequest = new Mock<HttpRequestBase>();
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            RouteCollection routes = new RouteCollection();

            // what do?
            // RouteConfig.RegisterRoutes(routes);

            moqRequest.Setup(e => e.AppRelativeCurrentExecutionFilePath).Returns("~/Home/Index");

            moqContext = new Mock<HttpContextBase>();
            moqRequest = new Mock<HttpRequestBase>();
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Create a "fake" form
            formValues = new NameValueCollection
            {
                { "FirstName", "Jonathan" },
                { "LastName", "Danylko" }
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
        }

        [TestCase]
        public void TestEnrollUserWithValidUser()
        {

            Assembly assembly = Assembly.LoadFrom(@"C:\SideAndTestProjects\LearningSystem\LearningSystem\bin\LearningSystem.Web.dll");
          //  Assembly assembly = Assembly.GetExecutingAssembly();


            var appUserData = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    Id = "asd",
                    Email = "ne@znam.bg",
                    PasswordHash = "hashed"
                }
            };

            var courseData = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Description = "Best course",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    Name = "Generic Course",
                    Trainer = new ApplicationUser()
                }
            };

            var studentData = new List<Student>
            {
                new Student()
                {
                    Id = 1,
                    User = new ApplicationUser()
                }
            };

            var mockStudentSet = AsDbSet(studentData);
            var mockCourseSet = AsDbSet(courseData);
            var mockUserSet = AsDbSet(appUserData);

            var mockContext = new Mock<ILearningSystemContext>();
            mockContext.Setup(c => c.Students).Returns(mockStudentSet.Object);
            mockContext.Setup(c => c.Courses).Returns(mockCourseSet.Object);
            mockContext.Setup(c => c.Users).Returns(mockUserSet.Object);


            mockStudentSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => studentData.FirstOrDefault(d => d.Id == (int)ids[0]));

            mockCourseSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>(ids => courseData.FirstOrDefault(c => c.Id == (int)ids[0]));

            mockStudentSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>(ids => studentData.FirstOrDefault(d => d.Id == (int)ids[0]));


            Type serviceType = typeof(IService);

            Type controllerType = DiscoverControllerType(assembly);

            ConstructorInfo controllerConstructor = ExtractConstructorFromController(controllerType, serviceType);

            Type serviceInterfaceType = DiscoverServiceInterface(controllerConstructor);

            Type[] typesInLoadedAssembly = DiscoverTypesInServiceAssembly(assembly);

            Type concreteServiceType = DiscoverServiceType(typesInLoadedAssembly, serviceInterfaceType);

            ConstructorInfo serviceConstructor = ExtractServiceConstructor(concreteServiceType);

            object service = serviceConstructor.Invoke(new object[] { mockContext.Object });

            object controller = controllerConstructor.Invoke(new object[] { service });

      

            var controllerContext = new ControllerContext(moqContext.Object, new RouteData(), controller as Controller);
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity("asd"), new string[0]);


            PropertyInfo httpBaseProperty =
                controller.GetType().GetProperties().FirstOrDefault(p => p.Name == "ControllerContext");

            httpBaseProperty.SetValue(controller, controllerContext);

            MethodInfo methodToTest = null;
            var methods = controller.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (var methodInfo in methods)
            {
                if (methodInfo.Name == ActionNameToTest)
                {
                    methodToTest = methodInfo;
                    break;
                }
                object[] attributes = methodInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    RouteAttribute routeAttribute = attribute as RouteAttribute;

                    if (routeAttribute != null && routeAttribute.Name==ActionNameToTest)
                    {
                        methodToTest = methodInfo;
                        break;
                    }
                }
                if (methodToTest != null)
                {
                    break;
                }
            }
            var result = methodToTest.Invoke(controller, new object[] {});
            Assert.AreEqual(ActionNameToTest,methodToTest.Name);
            // Mock HTTPContext
            // Use it somehow

            //Arrange
            // - Discover correct Action
            // -- via reflection and attributes?
            // - Disover its parameters and possibly return type

            //Act
            // - Execute action (via HTTPContext?)
            // - Make sure we have results in appropriate format

            //Assert
            // - Compare whatever the method has returned to what we have 


        }

        private static ConstructorInfo ExtractServiceConstructor(Type concreteServiceType)
        {
            var serviceConstructor = concreteServiceType
                .GetConstructors()
                .FirstOrDefault(c => c.GetParameters()
                                         .Any(p => p.ParameterType.Name == ContextInterfaceName)
                                     && c.GetParameters().Length == 1);
            return serviceConstructor;
        }

        private static Type DiscoverServiceType(Type[] typesInLoadedAssembly, Type serviceInterfaceType)
        {
            Type concreteServiceType = typesInLoadedAssembly.FirstOrDefault(t => serviceInterfaceType.IsAssignableFrom(t));
            return concreteServiceType;
        }

        private static Type DiscoverServiceInterface(ConstructorInfo controllerConstructor)
        {
            Type serviceInterfaceType = controllerConstructor
                .GetParameters()
                .FirstOrDefault(p => typeof(IService).IsAssignableFrom(p.ParameterType)).ParameterType;
            return serviceInterfaceType;
        }

        private static Type[] DiscoverTypesInServiceAssembly(Assembly assembly)
        {
            AssemblyName serviceAssembly = assembly
                .GetReferencedAssemblies().FirstOrDefault(a => a.Name == ServiceAssemblyName);

            Type[] typesInLoadedAssembly = Assembly.Load(serviceAssembly).GetTypes();
            return typesInLoadedAssembly;
        }

        private static Type DiscoverControllerType(Assembly assembly)
        {
            Type controllerType;
            IEnumerable<TypeInfo> typesInAssembly = assembly.DefinedTypes;
            foreach (var typeInfo in typesInAssembly)
            {
                if (typeInfo.Name.Contains(ControllerName + ControllerSuffix))
                {
                    return controllerType = typeInfo.AsType();
                }
            }
            return null;
        }

        private static ConstructorInfo ExtractConstructorFromController(Type controllerType, Type serviceType)
        {

            return controllerType
                .GetConstructors()
                .FirstOrDefault(c => c.GetParameters()
                                     .Any(p => serviceType.IsAssignableFrom(p.ParameterType)));
        }

        public static Mock<DbSet<T>> AsDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            mockDbSet.Setup(x => x.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(sourceList.AddRange);
            mockDbSet.Setup(x => x.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            mockDbSet.Setup(x => x.Remove(It.IsAny<T>())).Returns<T>(x => { if (sourceList.Remove(x)) return x; else return null; });
            mockDbSet.Setup(set => set.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(ts =>
            {
                foreach (var t in ts) { sourceList.Remove(t); }
            });

            return mockDbSet;
        }
    }
}
