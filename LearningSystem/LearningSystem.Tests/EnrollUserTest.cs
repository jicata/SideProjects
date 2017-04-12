using NUnit.Framework;

namespace LearningSystem.Tests
{   
    [TestFixture]
    public class EnrollUserTest
    {
        [OneTimeSetUp]
        public void TestInit()
        {
            // MockDb - > Mock Serivice - > Insert it into controller
            // Discover controller alongside its Constructor
            // Mock HTTPContext
            // Use it somehow
        }

        [TestCase]
        public void TestEnrollUserWithValidUser()
        {
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
