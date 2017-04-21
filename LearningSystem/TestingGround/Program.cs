using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using LearningSystem.Services;
using Moq;

namespace TestingGround
{
    public static class Program
    {
        private const string ControllerSuffix = "Controller";
        private const string ServiceInterfacePrefix = "I";
        private const string ServiceSuffix = "Service";
        private const string MsEnterpriseServicesLibraryName = "System.EnterpriseServices";
        public delegate T ObjectActivator<T>(params object[] args);

        static void Main()
        {

            FindLargestFile(@"C:\SideAndTestProjects\LearningSystem\LearningSystem");
            Assembly assembly = Assembly.LoadFrom(@"C:\SideAndTestProjects\LearningSystem\LearningSystem\bin\LearningSystem.Web.dll");
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

            var mockStudentSet = studentData.AsDbSet();
            var mockCourseSet = courseData.AsDbSet();
            var mockUserSet = appUserData.AsDbSet();

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

         
            // discover controller type
            Type controllerType = null;

            var typesInAssembly = assembly.DefinedTypes;
            foreach (var typeInfo in typesInAssembly)
            {
                //ASSUMING WE KNOW THE NAME OF THE CONTROLLER
                if (typeInfo.Name.Contains("Users"+ControllerSuffix))
                {
                    controllerType = typeInfo.AsType();
                }
            }

            // convention-based controller and service naming
            string controllerName = controllerType.FullName.Substring(controllerType.FullName.LastIndexOf(".") + 1).Replace("Controller", "");
            string serviceInterfaceName = ServiceInterfacePrefix + controllerName + ServiceSuffix;
          
            // discover service
            string serviceName = controllerName + ServiceSuffix;
            Type service = null;
            var loadedAssemblies = assembly.GetReferencedAssemblies().Where(r => r.Name.Contains("Services"));

            foreach (var loadedAssembly in loadedAssemblies)
            {
                var loaded = Assembly.Load(loadedAssembly);
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
            ConstructorInfo ctor = service
                .GetConstructors()
                .FirstOrDefault();

            object serviceObject = FormatterServices.GetUninitializedObject(service);


            //object serviceObject = service
            //    .GetConstructors()
            //    .FirstOrDefault()
            //    .Invoke(new object[] {mockContext.Object});


            var fieldOfService = serviceObject.GetType().BaseType.GetFields(BindingFlags.Instance |
                                BindingFlags.NonPublic |
                                BindingFlags.Public)
                                .FirstOrDefault(f=>f.Name.Contains("Context"));
            fieldOfService.SetValue(serviceObject,mockContext.Object);

            Console.WriteLine(fieldOfService.Name);

            // discover constructors

            var constructors = controllerType.GetConstructors();

            object controller = null;

            // find appropriate constructor and invoke it
            // also make sure we inject our mocked service

            foreach (var constructorInfo in constructors)
            {
                if (constructorInfo.GetParameters().Length == 0)
                {
                    controller = constructorInfo.Invoke(new object[] {});
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
                        controller = constructorInfo.Invoke(new object[] {serviceObject});
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
    

            //foreach (var propertyInfo in properties)
            //{
            //    Console.WriteLine(propertyInfo);
            //}
            // var instanceOfClass = FormatterServices.GetUninitializedObject(t);
            // MockDb - > Mock Serivice - > Insert it into controller
            // Discover controller alongside its Constructor
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
        public static Mock<DbSet<T>> AsDbSet<T>(this List<T> sourceList) where T : class
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

        private static  void FindLargestFile(string dirPath)
        {
            List<string> files = Directory.EnumerateFiles(dirPath).ToList();

            files[0] = files.OrderByDescending(f => new FileInfo(f).Length).First();
            Console.WriteLine(files[0]);

        }
        public static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }


    }
}
