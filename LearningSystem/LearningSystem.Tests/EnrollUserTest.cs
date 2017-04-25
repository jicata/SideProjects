﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
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
        private const string ControllerName = "Courses";
        private const string ActionNameToTest = "Details";
        private const string AutomapperConfig = "AutomapperConfig";


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
            //Assembly assembly = Assembly.GetExecutingAssembly();


            var appUserData = new List<ApplicationUser>
            {
                new ApplicationUser()
                {
                    Id = "asd",
                    UserName = "pesho",
                    Email = "ne@znam.bg",
                    PasswordHash = "hashed",
                    Name = "pesho"
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
                    User = appUserData[0],
                    Courses = courseData
                }
            };
            //courseData[0].Students = studentData;

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


            Type automapperType = assembly.GetTypes().FirstOrDefault(t => t.Name == AutomapperConfig);
            MethodInfo configureMappings = automapperType.GetMethod("ConfigureMappings");
            configureMappings.Invoke(null, null);

            Type serviceType = typeof(IService);

            Type controllerType = DiscoverControllerType(assembly);

            ConstructorInfo controllerConstructor = ExtractConstructorFromController(controllerType, serviceType);

            Type serviceInterfaceType = DiscoverServiceInterface(controllerConstructor);

            Type[] typesInLoadedAssembly = DiscoverTypesInServiceAssembly(assembly);

            Type concreteServiceType = DiscoverServiceType(typesInLoadedAssembly, serviceInterfaceType);

            ConstructorInfo serviceConstructor = ExtractServiceConstructor(concreteServiceType);

            object service = serviceConstructor.Invoke(new object[] { mockContext.Object });

            object controller = controllerConstructor.Invoke(new object[] { service });

            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            moqContext.Setup(c => c.User).Returns(user.Object);
            user.Setup(u => u.Identity).Returns(identity.Object);
            identity.Setup(i => i.IsAuthenticated).Returns(true);
            identity.Setup(i => i.Name).Returns("pesho");
            var controllerContext = new ControllerContext(moqContext.Object, new RouteData(), controller as Controller);


            PropertyInfo httpBaseProperty =
                controller.GetType().GetProperties().FirstOrDefault(p => p.Name == "ControllerContext");

            httpBaseProperty.SetValue(controller, controllerContext);

            MethodInfo methodToTest = null;
            var methods =
                controller.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
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

                    if (routeAttribute != null && routeAttribute.Name == ActionNameToTest)
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
            object modelToTest = studentData[0];

            int param = 3;
            HashSet<PropertyInfo> matchedPinfos = new HashSet<PropertyInfo>();
            ActionResult result = methodToTest.Invoke(controller, new object[] { param }) as ActionResult;
            return;
            object model = result; 
            Assert.IsNull(model);
            if (typeof(IEnumerable).IsAssignableFrom(model.GetType()))
            {
                model = (model as IEnumerable<object>).ToArray();
            }
            PropertyInfo[] propertiesOfViewResultModel = model.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            Dictionary<object, List<PropertyInfo>> propertiesOfViewModel = new Dictionary<object, List<PropertyInfo>>();
            propertiesOfViewModel = RetrieveProperties(propertiesOfViewModel, model);

            Dictionary<object, List<PropertyInfo>> propertiesOfActualModel =
                new Dictionary<object, List<PropertyInfo>>();
            propertiesOfActualModel = RetrieveProperties(propertiesOfActualModel, modelToTest);
            int count = 0;

            foreach (var objectInActualModel in propertiesOfActualModel.Keys)
            {
                foreach (PropertyInfo propertyOfModel in propertiesOfActualModel[objectInActualModel])
                {
                    object propertyValue = propertyOfModel.GetValue(objectInActualModel);
                    foreach (var objectInViewModel in propertiesOfViewModel.Keys)
                    {
                        bool matched = false;
                        List<PropertyInfo> propertiesOfObjectInViewModel = propertiesOfViewModel[objectInViewModel]
                            .Where(p => !matchedPinfos.Contains(p))
                            .ToList();
                        foreach (PropertyInfo propertyOfViewModel in propertiesOfObjectInViewModel)
                        {
                            object viewModelPropertyValue = propertyOfViewModel.GetValue(objectInViewModel);
                            if (propertyOfViewModel.PropertyType == propertyOfModel.PropertyType
                                && viewModelPropertyValue.Equals(propertyValue))
                            {
                                count++;
                                matched = true;
                                matchedPinfos.Add(propertyOfViewModel);
                                break;
                            }
                        }
                        if (matched)
                        {
                            matched = false;
                            break;
                        }
                    }
                }
            }

            int totalProperties = propertiesOfViewModel.Values.SelectMany(x => x).Count();
            bool matches = count >= totalProperties;
            Assert.IsTrue(matches);
        }

        private Dictionary<object, List<PropertyInfo>> RetrieveProperties(
            Dictionary<object, List<PropertyInfo>> retrievedProperties,
            params object[] models)
        {
            foreach (var model in models)
            {
                var modelProperties = model.GetType()
                    .GetProperties();
                foreach (var propertyInfo in modelProperties)
                {
                    if (propertyInfo.PropertyType.GetInterface("IEnumerable") != null
                        && propertyInfo.PropertyType.Name.ToLower() != "string"
                        && propertyInfo.PropertyType.Name.ToLower() != "datetime"
                        && !propertyInfo.PropertyType.GetGenericArguments()[0].IsPrimitive)
                    {
                        var collectionOfModels = (propertyInfo.GetValue(model) as IEnumerable<object>).ToArray();


                        RetrieveProperties(retrievedProperties, collectionOfModels);

                    }
                    else if (!propertyInfo.PropertyType.IsPrimitive
                             && propertyInfo.PropertyType.Name.ToLower() != "string"
                             && propertyInfo.PropertyType.Name.ToLower() != "datetime")
                    {

                        object innerObject = propertyInfo.GetValue(model);
                        if (innerObject != null && retrievedProperties.Keys.All(k => k.GetType() != innerObject.GetType()))
                        {
                            RetrieveProperties(retrievedProperties, innerObject);
                        }
                    }
                    else
                    {
                        if (!retrievedProperties.ContainsKey(model))
                        {
                            retrievedProperties.Add(model, new List<PropertyInfo>() { propertyInfo });
                        }
                        else
                        {
                            retrievedProperties[model].Add(propertyInfo);
                        }
                    }

                }
            }

            return retrievedProperties;
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
