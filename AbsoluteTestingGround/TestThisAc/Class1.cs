namespace ACTesterTests
{
    using ACTester.Controller;
    using ACTester.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class StatusTests
    {
        private IAirConditionerTesterSystem Controller { get; set; }

        [OneTimeSetUp]
        public void Initialize()
        {
            this.Controller = new AirConditionerTesterSystem();
        }

        [Test]
        public void Status_ShouldReturnCorrectlyFormattedResult()
        {
            this.Controller.RegisterStationaryAirConditioner("Toshiba", "EX100", "B", 1000);
            this.Controller.RegisterCarAirConditioner("Toshiba", "C60", 9);
            this.Controller.RegisterPlaneAirConditioner("Hitachi", "P320", 25, 750);
            this.Controller.TestAirConditioner("Hitachi", "P320");
            var result = Controller.Status();
            Assert.AreEqual("Jobs complete: 33.33%", result, "Expected messages did not match!");
        }

        [Test]
        public void Status_ShouldReturnCorrectlyRoundedResult()
        {
            this.Controller.RegisterStationaryAirConditioner("Toshiba", "EX100", "B", 1000);
            this.Controller.RegisterCarAirConditioner("Toshiba", "C60", 9);
            this.Controller.RegisterPlaneAirConditioner("Hitachi", "P320", 25, 750);
            this.Controller.TestAirConditioner("Hitachi", "P320");
            this.Controller.TestAirConditioner("Toshiba", "C60");
            var result = Controller.Status();
            Assert.AreEqual("Jobs complete: 66.67%", result, "Expected messages did not match!");
        }

        [Test]
        public void Status_WithoutAnyAirConditioners_ShouldReturnZeroPercent()
        {
            var result = Controller.Status();
            Assert.AreEqual("Jobs complete: 0.00%", result, "Expected messages did not match!");
        }

        [Test]
        public void Status_WithExistingNonTestedAirConditioners_ShouldReturnZeroPercent()
        {
            this.Controller.RegisterStationaryAirConditioner("Toshiba", "EX100", "B", 1000);
            this.Controller.RegisterCarAirConditioner("Toshiba", "C60", 9);
            this.Controller.RegisterPlaneAirConditioner("Hitachi", "P320", 25, 750);
            var result = Controller.Status();
            Assert.AreEqual("Jobs complete: 0.00%", result, "Expected messages did not match!");
        }
    }
}
