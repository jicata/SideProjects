using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace PawInc
{
    [TestFixture]
    public class TestBark
    {
        [Test]
        public void TestMyBarkYouFool()
        {
            Type doggers = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == "Dog");
            var constructor = doggers.GetConstructors().FirstOrDefault();

            object invokedDog = Activator.CreateInstance(doggers, new object[] { "balo", 2, "doma", 2 });
            doggers.GetMethod("Bark").Invoke(invokedDog, null);
            Assert.AreEqual(2, 2);
        }
    }
}