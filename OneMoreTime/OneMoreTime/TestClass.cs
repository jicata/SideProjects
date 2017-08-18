namespace OneMoreTime
{
    using Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class TestClass
    {
        [Test]
        public void heheTest()
        {
            var controller = new AboutController();
            var country = controller.Country();
            Assert.AreEqual("USA",country);
        }
    }
}
