namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

        [SetUp]
        public void Setup()
        {
            Person[] persons =
            {
            new Person(1, "Stoyan"),
            new Person(2, "Gemata"),
            new Person(3, "Ivan_Ivan"),
            new Person(4, "Pesho_ivanov"),
            new Person(5, "Gosho_Naskov"),
            new Person(6, "Pesh-Peshov"),
            new Person(7, "Ivan_Kaloqnov"),
            new Person(8, "Ivan_Draganchov"),
            new Person(9, "Asen"),
            new Person(10, "Jivko"),
            new Person(11, "Toshko")
        };

            database = new Database(persons);
        }

        [Test]
        public void CreatingDatabaseCount_ShouldBeCorrect()
        {
            int expectedResult = 11;
            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void CreatingDatabase_ShouldThrowExceptionWhenCountIsMoreThan16()
        {

            Person[] persons =
            {
            new Person(1, "Stoyan"),
            new Person(2, "Gemata"),
            new Person(3, "Ivan_Ivan"),
            new Person(4, "Pesho_ivanov"),
            new Person(5, "Gosho_Naskov"),
            new Person(6, "Pesh-Peshov"),
            new Person(7, "Ivan_Kaloqnov"),
            new Person(8, "Ivan_Draganchov"),
            new Person(9, "Asen"),
            new Person(10, "Jivko"),
            new Person(11, "Toshko"),
            new Person(12, "Moshko"),
            new Person(13, "Foshko"),
            new Person(14, "Loshko"),
            new Person(15, "Roshko"),
            new Person(16, "Boshko"),
            new Person(17, "Kokoshko")
        };

            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                database = new Database(persons));

            Assert.AreEqual("Provided data length should be in range [0..16]!", exception.Message);
        }

        [Test]
        public void DatabaseCount_ShouldWorkCorrectly()
        {
            int expectedResult = 11;
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseAddMethod_ShouldIncreaseCount()
        {
            var person = new Person(12, "Paul");

            database.Add(person);

            int expectedResult = 12;
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseAddMethod_ShouldWorkCorrectly()
        {
            var person = new Person(12, "Paul");

            database.Add(person);

            int expectedResult = 12;
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseAddMethod_ShouldThrowException_IfCountIsMoreThan16()
        {
            Person person1 = new(12, "John");
            Person person2 = new(13, "Paul");
            Person person3 = new(14, "Green");
            Person person4 = new(15, "Brown");
            Person person5 = new(16, "Killer");

            database.Add(person1);
            database.Add(person2);
            database.Add(person3);
            database.Add(person4);
            database.Add(person5);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.Add(new Person(17, "Destroyer")));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
        }

        [Test]
        public void Database_ShouldThrowException_IfPersonWithSameUsernameIsAdded()
        {
            Person person = new(12, "Stoyan");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.Add(person), "There is already user with this username!");

            Assert.AreEqual("There is already user with this username!", exception.Message);
        }

        [Test]
        public void Database_ShouldThrowException_IfPersonWithSameIdIsAdded()
        {
            Person person = new(1, "John");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.Add(person));

            Assert.AreEqual("There is already user with this Id!", exception.Message);
        }

        [Test]
        public void DatabaseRemoveMethod_ShouldWorkCorrectly()
        {
            int expectedResult = 10;

            database.Remove();

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void DatabaseRemoveMethod_ShouldThrowException_IfDatabaseIsEmpty()
        {
            Database database = new();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void DatabaseFindByUsernameMethod_ShouldWorkCorrectly()
        {
            string expectedResult = "Stoyan";
            string actualResult = database.FindByUsername("Stoyan").UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseFindByUsernameMethod_ShouldBeCaseSensitive()
        {
            string expectedResult = "stoYan";
            string actualResult = database.FindByUsername("Stoyan").UserName;

            Assert.AreNotEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void DatabaseFindByUsernameMethod_ShouldThrowException_IfUsernameIsNull(string username)
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                database.FindByUsername(username));

            Assert.AreEqual("Username parameter is null!", exception.ParamName);
        }

        [Test]
        [TestCase("Kiro")]
        [TestCase("Paul")]
        public void DatabaseFindByUsernameMethod_ShouldThrowException_IfUsernameIsNotFound(string username)
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.FindByUsername(username));

            Assert.AreEqual("No user is present by this username!", exception.Message);
        }

        [Test]
        public void DatabaseFindByIdMethod_ShouldWorkCorrectly()
        {
            string expectedResult = "Gemata";
            string actualResult = database.FindById(2).UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseFindByIdMethod_ShouldThrowException_IfIdIsNegative()
        {
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                database.FindById(-1));

            Assert.AreEqual("Id should be a positive number!", exception.ParamName);
        }

        [Test]
        public void DatabaseFindByIdMethod_ShouldThrowException_IfIdIsNotFound()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                database.FindById(25));

            Assert.AreEqual("No user is present by this ID!", exception.Message);
        }
    }
}