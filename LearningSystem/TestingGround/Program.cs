using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using LearningSystem.Services;
using Moq;

namespace TestingGround
{
    public static class Program
    {
        static void Main()
        {
            // Initialize context
            var context = new LearningSystemContext();
            Assembly assembly = Assembly.LoadFrom(@"C:\SideAndTestProjects\LearningSystem\LearningSystem\bin\LearningSystem.Web.dll");
            //In reality we can actually just call -> Assembly.GetExecutingAssembly();

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

            Service service;
            Type t = null;

            var typesInAssembly = assembly.DefinedTypes;
            foreach (var typeInfo in typesInAssembly)
            {
                if (typeInfo.Name.Contains("UsersController"))
                {
                    t = typeInfo.AsType();
                }
            }

            var constructors = t.GetConstructors();
            Type constructorParam = null;
            foreach (var constructorInfo in constructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    if (parameterInfo.ParameterType.IsAssignableFrom(typeof(Service)))
                    {
                        constructorParam = parameterInfo.ParameterType;
                    }
                }
            }
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly1 in loadedAssemblies)
            {
                foreach (var loadedAssembly in assembly1.GetTypes())
                {
                    Console.WriteLine(loadedAssembly);
                }
            }

            var instanceOfClass = FormatterServices.GetUninitializedObject(t);
            

            //foreach (var propertyInfo in properties)
            //{
            //    Console.WriteLine(propertyInfo);
            //}

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


    }
}
