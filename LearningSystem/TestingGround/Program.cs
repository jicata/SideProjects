using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

using LearningSystem.Data;
using LearningSystem.Models.EntityModels;
using Moq;

namespace TestingGround
{
    class Program
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
            }.AsQueryable();

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
            }.AsQueryable();

            var studentData = new List<Student>
            {
              new Student()
              {
                  Id = 1,
                  User = new ApplicationUser()
              }
            }.AsQueryable();

            var mockStudentSet = new Mock<DbSet<Student>>();
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(studentData.Provider);
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(studentData.Expression);
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(studentData.ElementType);
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(studentData.GetEnumerator());

            var mockCourseSet = new Mock<DbSet<Student>>();
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(studentData.Provider);
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(studentData.Expression);
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(studentData.ElementType);
            mockStudentSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(studentData.GetEnumerator());

            var mockContext = new Mock<LearningSystemContext>();
            mockContext.Setup(c => c.Students).Returns(mockStudentSet.Object);
            mockStudentSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => studentData.FirstOrDefault(d => d.Id == (int)ids[0]));

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
            // - Compare whatever the method has returned to what we have mocked
        }
    }
}
