using System;

namespace Database.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;
        [SetUp]
        public void Setup()
        {
            // �������� �� ��������, � ����� ������� ������� 1 � 2
            database = new(1, 2);
        }

        [Test]
        public void CreatingDatabaseCount_ShouldBeCorrect()
        {
            // Arrange
            int expectedResult = 2;

            // Act
            int actualResult = database.Count;

            //Assert
            Assert.NotNull(database);
            Assert.AreEqual(expectedResult, actualResult);
        }

        // Arrange-a �� � � [TestCase] ��������
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void CreateDatabase_WhenTheRangeIsWithin16_ShouldAddElementsCorrectly(int[] data)
        {
            //Act
            database = new(data);
            int[] actualResult = database.Fetch();

            //Assert
            Assert.AreEqual(data, actualResult);
        }

        // Arrange-a �� � � [TestCase] ��������
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        public void CreateDatabase_WhenCountIsMoreThan16_ShouldThrowException(int[] data)
        {
            //Act � Assert �� �� 1 �����, ������ ���������� �� ������
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database = new(data));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
        }

        [Test]
        public void DatabaseCount_ShouldWorkCorrect()
        {
            // Arrange
            int expectedResult = 2;

            // Act
            int actualResult = database.Count;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        // Arrange-a �� � � [TestCase] ��������
        [TestCase(-3)]
        [TestCase(0)]
        public void CreateDatabaseAddMethod_ShouldIncreaseCount(int number)
        {
            int expectedResult = 3;
            database.Add(number);

            // Act
            int actualResult = database.Count;

            //Assert
            Assert.NotNull(database);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void DatabaseAddMethod_ShouldAddElementsCorrectly(int[] data)
        {
            database = new Database();

            // ������� �������� ����� ���� {.Add} ������, � �� ���� ������������
            foreach (var number in data)
            {
                database.Add(number);
            }

            int[] actualResult = database.Fetch();

            Assert.AreEqual(data, actualResult);
        }

        [Test]
        public void Database_WhenCountIsMoreThan16_ShouldThrowException()
        {
            // ����� 1 {for} �����, �� �� ������ ��� 14 ����� (�� �� ������ ���� 16)
            for (int i = 0; i < 14; i++)
            {
                database.Add(i);
            }

            //Act � Assert �� �� 1 �����, ������ ���������� �� ������
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.Add(321));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
        }

        [Test]
        public void Database_WhenRemoveMethodIsCalled_ShouldDecreaseCount()
        {
            int expectedResult = 1;

            database.Remove();

            // {database.Count} � ������ {actualResult}
            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void Database_WhenRemoveMethodIsCalled_ShouldRemoveElements()
        {
            int[] expectedResult = Array.Empty<int>();

            database.Remove();
            database.Remove();

            int[] actualResult = database.Fetch();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Database_WhenRemoveMethodDataIsEmpty_ShouldThrowException()
        {
            database = new Database();
            //Act � Assert �� �� 1 �����, ������ ���������� �� ������
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.Remove());
            
            Assert.AreEqual("The collection is empty!", exception.Message);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void DatabaseFetchMethod_ShouldReturnElementsCorrectly(int[] data)
        {
            database = new Database(data);

            int[] actualResult = database.Fetch();

            Assert.AreEqual(data, actualResult);
        }
    }
}