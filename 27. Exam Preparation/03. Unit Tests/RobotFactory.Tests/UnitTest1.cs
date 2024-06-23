using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [Test]
        public void Constructor_Test()
        {
            // Arrange
            string factoryName = "PuflesFactory";
            int factoryCapacity = 10;

            // Act
            var factory = new Factory(factoryName, factoryCapacity);
        }

        [Test]
        public void ProduceRobot_ShouldAddRobotToFactory()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);

            // Act
            string result = factory.ProduceRobot("Model1", 100.0, 1);

            Assert.AreEqual("Produced --> Robot model: Model1 IS: 1, Price: 100.00", result);
            Assert.AreEqual(1, factory.Robots.Count);
        }

        [Test]
        public void ProduceRobot_ShouldNotAddMoreRobotsThanTheCapacity()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 1);

            // Act
            string result1 = factory.ProduceRobot("Model1", 100.0, 1);
            string result2 = factory.ProduceRobot("Model2", 200.0, 2);

            Assert.AreEqual("Produced --> Robot model: Model1 IS: 1, Price: 100.00", result1);
            Assert.AreEqual("The factory is unable to produce more robots for this production day!", result2);
            Assert.AreEqual(1, factory.Robots.Count);
        }

        [Test]
        public void ProduceSupplement_ShouldAddSupplement()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);

            // Act
            string result = factory.ProduceSupplement("Supplement1", 1);

            Assert.AreEqual($"Supplement: Supplement1 IS: 1", result);
            Assert.AreEqual(1, factory.Supplements.Count);
        }

        [Test]
        public void UpgradeRobot_ShouldUpgradeRobotWithSupplements()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);
            var robot = new Robot("Pafles", 100, 1);
            var supplement = new Supplement("Supplement1", 1);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.IsTrue(result);
            Assert.AreEqual(1, robot.Supplements.Count);
        }

        [Test]
        public void UpgradeRobot_ShouldNotUpgradeRobotWithBadSupplements()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);
            var robot = new Robot("Pafles", 100, 1);
            var supplement = new Supplement("Supplement1", 2);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.IsFalse(result);
            Assert.AreEqual(0, robot.Supplements.Count);
        }

        [Test]
        public void UpgradeRobot_ShouldNotUpgradeRobotWithExistingSupplements()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);
            var robot = new Robot("Pafles", 100, 1);
            var supplement = new Supplement("Supplement1", 2);
            factory.UpgradeRobot(robot, supplement);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            Assert.IsFalse(result);
            Assert.AreEqual(0, robot.Supplements.Count);
        }

        [Test]
        public void SellRobot_ShouldReturnTheRobotWithTheHighestPrice()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);
            factory.ProduceRobot("Pafles1", 100, 1);
            factory.ProduceRobot("Pafles2", 150, 2);
            factory.ProduceRobot("Pafles3", 200, 1);

            // Act
            var result = factory.SellRobot(200.0);

            Assert.IsNotNull(result);
            Assert.AreEqual("Pafles3", result.Model);
        }

        [Test]
        public void SellRobot_ShouldReturnNullIfNoRobotsCanBeSold()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);
            factory.ProduceRobot("Pafles1", 100, 1);

            // Act
            var result = factory.SellRobot(50.0);

            Assert.IsNull(result);
        }

        [Test]
        public void SellRobot_ShouldReturnNullIfNoRobotsAreCreated()
        {
            // Arrange
            var factory = new Factory("PuflesFactory", 5);

            // Act
            var result = factory.SellRobot(50.0);

            Assert.IsNull(result);
        }
    }
}