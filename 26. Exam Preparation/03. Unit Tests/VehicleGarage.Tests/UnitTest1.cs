using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {

        [Test]
        public void Constructor_Test()
        {
            // Arrange
            int capacity = 3;

            // Act
            Garage garage = new Garage(capacity);

            // Assert
            Assert.AreEqual(capacity, garage.Capacity);
            Assert.IsNotNull(garage.Vehicles);
            Assert.IsInstanceOf<List<Vehicle>>(garage.Vehicles);
            Assert.AreEqual(0, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicle_ValidVehicle_ReturnsTrue()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", "ABC123");

            // Act
            bool result = garage.AddVehicle(vehicle);

            // Assert
            Assert.IsTrue(result);
            Assert.Contains(vehicle, garage.Vehicles);
        }

        [Test]
        public void AddVehicle_FullCapacity_ReturnsFalse()
        {
            // Arrange
            Garage garage = new Garage(1);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");

            // Act
            garage.AddVehicle(vehicle1);
            bool result = garage.AddVehicle(vehicle2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicle_DuplicateLicensePlate_ReturnsFalse()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "ABC123");

            // Act
            garage.AddVehicle(vehicle1);
            bool result = garage.AddVehicle(vehicle2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void AddVehicle_FullCapacityAndDuplicateLicensePlate_ReturnsFalse()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "ABC123");

            // Act
            garage.AddVehicle(vehicle1);
            bool result = garage.AddVehicle(vehicle2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void ChargeVehicles_AllVehiclesCharged_ReturnsCorrectCount()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            // Act
            int chargedCount = garage.ChargeVehicles(100);

            // Assert
            Assert.AreEqual(2, chargedCount);
            Assert.AreEqual(100, vehicle1.BatteryLevel);
            Assert.AreEqual(100, vehicle2.BatteryLevel);
        }

        [Test]
        public void ChargeVehicles_NoVehiclesChargedWhenAboveMaxLevel_ReturnsZero()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            // Act
            int chargedCount = garage.ChargeVehicles(90);

            // Assert
            Assert.AreEqual(0, chargedCount);
            Assert.AreEqual(100, vehicle1.BatteryLevel);
            Assert.AreEqual(100, vehicle2.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_ValidDrivingConditions_DrivesVehicleSuccessfully()
        {
            // Arrange
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 30, false);

            // Assert
            Assert.AreEqual(70, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]
        public void DriveVehicle_DamagedVehicle_DoesNotDrive()
        {
            // Arrange
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            vehicle.IsDamaged = true;
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 30, false);

            // Assert
            Assert.AreEqual(100, vehicle.BatteryLevel); // No change in battery level
            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]
        public void DriveVehicle_ExcessiveBatteryDrainage_DoesNotDrive()
        {
            // Arrange
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 120, false);

            // Assert
            Assert.AreEqual(100, vehicle.BatteryLevel); // No change in battery level
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]
        public void DriveVehicle_AccidentOccurred_DamagesVehicle()
        {
            // Arrange
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 30, true);

            // Assert
            Assert.AreEqual(70, vehicle.BatteryLevel);
            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]
        public void RepairVehicles_SomeDamagedVehicles_RepairVehicles()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            // Act
            garage.DriveVehicle("ABC123", 30, true);
            garage.DriveVehicle("XYZ789", 20, true);
            string repairResult = garage.RepairVehicles();

            // Assert
            Assert.AreEqual("Vehicles repaired: 2", repairResult);
            Assert.IsFalse(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]
        public void RepairVehicles_NoDamagedVehicles_ReturnsZero()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            // Act
            string repairResult = garage.RepairVehicles();

            // Assert
            Assert.AreEqual("Vehicles repaired: 0", repairResult);
            Assert.IsFalse(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]
        public void RepairVehicles_SomeDamagedVehiclesAfterRepair_RepairVehicles()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            // Act
            garage.DriveVehicle("ABC123", 30, true);
            garage.DriveVehicle("XYZ789", 20, true);
            garage.RepairVehicles();

            // Assert
            Assert.IsFalse(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);

            // Act (try repairing again)
            string repairResult = garage.RepairVehicles();

            // Assert
            Assert.AreEqual("Vehicles repaired: 0", repairResult);
        }
    }
}