namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Tests
    {
        [Test]
        public void Constructor_Test()
        {
            // Arrange
            string stationName = "TestStation";

            // Act
            var station = new RailwayStation(stationName);

            // Assert
            Assert.AreEqual(stationName, station.Name);
            Assert.IsNotNull(station.ArrivalTrains);
            Assert.IsNotNull(station.DepartureTrains);
            Assert.AreEqual(0, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void Constructor_ThrowsArgumentException()
        {
            // Arrange
            string stationName = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new RailwayStation(stationName));
        }

        [Test]
        public void NewArrivalOnBoard_AddsTrainToTrainsQueue()
        {
            // Arrange
            var station = new RailwayStation("TestStation");
            string trainInfo = "Train123";

            // Act
            station.NewArrivalOnBoard(trainInfo);

            // Assert
            Assert.AreEqual(trainInfo, station.ArrivalTrains.Peek());
        }

        [Test]
        public void TrainHasArrived_MovesTrainToDepartureQueue()
        {
            // Arrange
            var station = new RailwayStation("TestStation");
            string trainInfo = "Train123";
            station.NewArrivalOnBoard(trainInfo);

            // Act
            string result = station.TrainHasArrived(trainInfo);

            // Assert
            Assert.AreEqual($"{trainInfo} is on the platform and will leave in 5 minutes.", result);
            Assert.AreEqual(0, station.ArrivalTrains.Count);
            Assert.AreEqual(trainInfo, station.DepartureTrains.Peek());
        }

        [Test]
        public void TrainHasArrived_ReturnsErrorMessage()
        {
            // Arrange
            var station = new RailwayStation("TestStation");
            string trainInfo1 = "Train123";
            string trainInfo2 = "Train456";
            station.NewArrivalOnBoard(trainInfo1);

            // Act
            string result = station.TrainHasArrived(trainInfo2);

            // Assert
            Assert.AreEqual($"There are other trains to arrive before {trainInfo2}.", result);
            Assert.AreEqual(trainInfo1, station.ArrivalTrains.Peek());
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasLeft_RemovesTrainFromDepartureQueue()
        {
            // Arrange
            var station = new RailwayStation("TestStation");
            string trainInfo = "Train123";
            station.NewArrivalOnBoard(trainInfo);
            station.TrainHasArrived(trainInfo);

            // Act
            bool result = station.TrainHasLeft(trainInfo);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TrainHasLeft_ReturnsFalse()
        {
            // Arrange
            var station = new RailwayStation("TestStation");
            string trainInfo1 = "Train123";
            string trainInfo2 = "Train456";
            station.NewArrivalOnBoard(trainInfo1);
            station.TrainHasArrived(trainInfo1);

            // Act
            bool result = station.TrainHasLeft(trainInfo2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(trainInfo1, station.DepartureTrains.Peek());
        }
    }
}