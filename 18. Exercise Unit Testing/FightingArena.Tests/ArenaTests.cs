namespace FightingArena.Tests
{
    using System.Linq;
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void Setup()
        {
            arena = new Arena();
        }

        [Test]
        public void ArenaConstructor_ShouldWorkCorrectly()
        {
            Assert.IsNotNull(arena);
            Assert.IsNotNull(arena.Warriors);
        }

        [Test]
        public void ArenaCount_ShouldWorkCorrectly()
        {
            int expectedResult = 1;

            Warrior warrior = new("Stoyan", 5, 100);

            arena.Enroll(warrior);

            Assert.IsNotEmpty(arena.Warriors);
            Assert.AreEqual(expectedResult, arena.Count);
        }

        [Test]
        public void ArenaEnroll_ShouldWorkCorrectly()
        {
            Warrior warrior = new("Stoyan", 5, 100);

            arena.Enroll(warrior);

            Assert.IsNotEmpty(arena.Warriors);
            Assert.AreEqual(warrior, arena.Warriors.Single());
        }

        [Test]
        public void ArenaEnroll_ShouldThrowException_IfWarriorIsAlreadyEnrolled()
        {
            Warrior warrior = new("Stoyan", 5, 100);

            arena.Enroll(warrior);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                arena.Enroll(warrior));

            Assert.AreEqual("Warrior is already enrolled for the fights!", exception.Message);
        }


        [Test]
        public void ArenaFight_ShouldWorkCorrectly()
        {
            Warrior attacker = new("Stoyan", 15, 100);
            Warrior defender = new("Gemata", 5, 50);

            arena.Enroll(attacker);
            arena.Enroll(defender);

            int expectedAttackerHp = 95;
            int expectedDefenderHp = 35;

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        }

        [Test]
        public void ArenaFight_ShouldThrowException_IfAttackerNotFound()
        {
            Warrior attacker = new("Stoyan", 15, 100);
            Warrior defender = new("Gemata", 5, 50);

            arena.Enroll(defender);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                arena.Fight(attacker.Name, defender.Name));

            Assert.AreEqual($"There is no fighter with name {attacker.Name} enrolled for the fights!", exception.Message);
        }

        [Test]
        public void ArenaFight_ShouldThrowException_IfDefenderNotFound()
        {
            Warrior attacker = new("Stoyan", 15, 100);
            Warrior defender = new("Gemata", 5, 50);

            arena.Enroll(attacker);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                arena.Fight(attacker.Name, defender.Name));

            Assert.AreEqual($"There is no fighter with name {defender.Name} enrolled for the fights!", exception.Message);
        }
    }
}
