namespace OneMoreTime
{
    using Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class TestTest
    {
        [Test]
        public void IzobshtoNeESmeshno()
        {
            var controller = new HomeController();
            var index = controller.Index();
            Assert.AreEqual("Jello",index);
        }
    }
}
