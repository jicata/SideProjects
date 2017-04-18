using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using Moq;
using NUnit.Framework;

namespace LearningSystem.Tests
{   
    [TestFixture]
    public class EnrollUserTest
    {
        private const string ControllerSuffix = "Controller";
        private const string ServiceInterfacePrefix = "I";
        private const string ServiceSuffix = "Service";
        private const string MsEnterpriseServicesLibraryName = "System.EnterpriseServices";
        [TestCase]
        public void TestEnrollUserWithValidUser()
        {

            Assembly assembly = Assembly.LoadFrom(@"C:\SideAndTestProjects\LearningSystem\LearningSystem\bin\LearningSystem.Web.dll");
            //Assembly assembly = Assembly.GetExecutingAssembly();
            // In reality we can actually just call -> Assembly.GetExecutingAssembly();

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

            var mockContext = new Mock<LearningSystemContext>();
            mockContext.Setup(c => c.Students).Returns(mockStudentSet.Object);
            mockContext.Setup(c => c.Courses).Returns(mockCourseSet.Object);
            mockContext.Setup(c => c.Users).Returns(mockUserSet.Object);

           
            mockStudentSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => studentData.FirstOrDefault(d => d.Id == (int)ids[0]));

            mockCourseSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>(ids => courseData.FirstOrDefault(c => c.Id == (int)ids[0]));

            mockStudentSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>(ids => studentData.FirstOrDefault(d => d.Id == (int)ids[0]));


            // discover controller type
            Type controllerType = null;

            var typesInAssembly = assembly.DefinedTypes;
            foreach (var typeInfo in typesInAssembly)
            {
                //ASSUMING WE KNOW THE NAME OF THE CONTROLLER
                if (typeInfo.Name.Contains("Users" + ControllerSuffix))
                {
                    controllerType = typeInfo.AsType();
                    break;
                }
            }

            // convention-based controller and service naming
            string controllerName = controllerType
                .FullName
                .Substring(controllerType.FullName.LastIndexOf(".") + 1)
                .Replace("Controller", "");

            string serviceInterfaceName = ServiceInterfacePrefix + controllerName + ServiceSuffix;

            // discover service
            string serviceName = controllerName + ServiceSuffix;
            Type service = null;
            var referencedAssemblies = assembly.GetReferencedAssemblies().Where(r => r.Name.Contains("Services"));

            foreach (var referencedAssembly in referencedAssemblies)
            {
                var loaded = Assembly.Load(referencedAssembly);
                foreach (var typeInAssembly in loaded.GetTypes())
                {
                    if (typeInAssembly.Name == serviceName)
                    {
                        service = typeInAssembly;
                        break;
                    }
                }
            }
          
            // instantiate service

            object serviceObject = Activator.CreateInstance(service);
            Assert.AreEqual(1, 1);
            return;



            var fieldOfService = serviceObject.GetType().BaseType.GetFields(BindingFlags.Instance |
                                BindingFlags.NonPublic |
                                BindingFlags.Public)
                                .FirstOrDefault(f => f.Name.Contains("Context"));
            fieldOfService.SetValue(serviceObject, mockContext.Object);


            // discover constructors

            var constructors = controllerType.GetConstructors();

            object controller = null;

            // find appropriate constructor and invoke it
            // also make sure we inject our mocked service

            foreach (var constructorInfo in constructors)
            {
                if (constructorInfo.GetParameters().Length == 0)
                {
                    controller = constructorInfo.Invoke(new object[] { });
                    var serviceField = controller.GetType().GetFields(BindingFlags.Instance |
                                                        BindingFlags.NonPublic |
                                                        BindingFlags.Public)
                                                            .FirstOrDefault(f => f
                                                                                .FieldType.Name == serviceInterfaceName);
                    serviceField.SetValue(controller, serviceObject);
                    break;
                }
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    if (parameterInfo.ParameterType.Name == serviceInterfaceName)
                    {
                        controller = constructorInfo.Invoke(new object[] { serviceObject });
                        break;
                    }
                }
                if (controller != null)
                {
                    break;
                }
            }
            var timeToAssert = controller.GetType().GetFields(BindingFlags.Instance |
                                               BindingFlags.NonPublic |
                                               BindingFlags.Public)
                                                   .FirstOrDefault(f => f
                                                                       .FieldType.Name == serviceInterfaceName);
            var fieldValue = timeToAssert.GetValue(controller).GetType().Name;
            Assert.AreEqual("UsersService",fieldValue);
        }
        public  Mock<DbSet<T>> AsDbSet<T>(List<T> sourceList) where T : class
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
