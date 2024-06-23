namespace CarManager.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]
        public void SetUp()
        {
            car = new Car("Mercedes", "g500", 10.5, 80.0);
        }

        [Test]
        public void Car_ShouldBeCreatedSuccessfully()
        {
            string expectedMake = "Mercedes";
            string expectedModel = "g500";
            double expectedFuelConsumption = 10.5;
            double expectedFuelCapacity = 80.0;

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
        }

        [Test]
        public void Car_ShouldBeCreatedWithZeroFuelAmount()
        {
            Assert.AreEqual(0, car.FuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CarMake_ShouldThrowException_IfIsSetToNull(string make)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Car(make, "g500", 10.5, 80.0));

            Assert.AreEqual("Make cannot be null or empty!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CarModel_ShouldThrowException_IfIsSetToNull(string model)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Car("Mercedes", model, 10.5, 80.0));

            Assert.AreEqual("Model cannot be null or empty!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-3)]
        public void CarFuelConsumption_ShouldThrowException_IfIsNegativeOrZero(int fuelConsumption)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Car("Mercedes", "g500", fuelConsumption, 80.0));

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", exception.Message);
        }

        [Test]
        public void CarFuelAmount_ShouldThrowExceptionIf_IsNegative()
        {
            Assert.Throws<InvalidOperationException>(() =>
                car.Drive(12), "Fuel amount cannot be negative!");
        }

        [TestCase(0)]
        [TestCase(-2)]
        public void CarFuelCapacity_ShouldThrowException_IfIsNegativeOrZero(int fuelCapacity)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Car("Mercedes", "g500", 10.5, fuelCapacity));

            Assert.AreEqual("Fuel capacity cannot be zero or negative!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void CarRefuel_ShouldThrowException_IfFuelIsNegativeOrZero(double fuelToRefuel)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                car.Refuel(fuelToRefuel));

            Assert.AreEqual("Fuel amount cannot be zero or negative!", exception.Message);
        }

        [Test]
        public void CarRefuel_ShouldIncreaseFuelAmount()
        {
            int expectedResult = 22;

            car.Refuel(22);
            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CarFuelAmount_ShouldNotBeMoreThanFuelCapacity()
        {
            int expectedResult = 80;

            car.Refuel(95);
            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CarDriveMethod_ShouldDecreaseFuelAmount()
        {
            double expectedResult = 47.9;

            car.Refuel(50);
            car.Drive(20);
            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CarDriveMethod_ShouldThrowException_IfFuelNeededIsMoreThanFuelAmount()
        {
            car.Refuel(5);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                car.Drive(50));

            Assert.AreEqual("You don't have enough fuel to drive!", exception.Message);
        }
    }
}