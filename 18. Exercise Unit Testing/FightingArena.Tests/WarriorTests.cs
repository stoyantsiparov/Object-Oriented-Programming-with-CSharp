namespace FightingArena.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void WarriorConstructor_ShouldWorkCorrectly()
        {
            string expectedName = "Gemata";
            int expectedDamage = 15;
            int expectedHP = 100;

            Warrior warrior = new(expectedName, expectedDamage, expectedHP);

            Assert.AreEqual(expectedName, warrior.Name);
            Assert.AreEqual(expectedDamage, warrior.Damage);
            Assert.AreEqual(expectedHP, warrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void WarriorConstructor_ShouldThrowException_IfNameIsNullOrWhiteSpace(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Warrior(name, 25, 50));

            Assert.AreEqual("Name should not be empty or whitespace!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-22)]
        public void WarriorConstructor_ShouldThrowException_IfDamageIsNotPositive(int dmg)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Warrior("Gemata", dmg, 50));

            Assert.AreEqual("Damage value should be positive!", exception.Message);
        }

        [TestCase(-5)]
        [TestCase(-22)]
        public void WarriorConstructor_ShouldThrowException_IfHPIsNegative(int hp)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
                new Warrior("Gemata", 25, hp));

            Assert.AreEqual("HP should not be negative!", exception.Message);
        }

        [Test]
        public void AttackMethod_ShouldWorkCorrectly()
        {
            int expectedAttackerHp = 95;
            int expectedDefenderHp = 80;

            Warrior attacker = new("Gemata", 10, 100);
            Warrior defender = new("Stoyan", 5, 90);

            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        }

        [TestCase(30)]
        [TestCase(22)]
        [TestCase(5)]
        public void Warrior_ShouldNotAttack_IfHisHPIsEqualOrLessThan30(int hp)
        {
            Warrior attacker = new("Gemata", 10, hp);
            Warrior defender = new("Stoyan", 5, 90);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                attacker.Attack(defender));

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", exception.Message);
        }

        [TestCase(30)]
        [TestCase(22)]
        [TestCase(5)]
        public void Warrior_ShouldNotAttackEnemyWith30HpOrLess(int hp)
        {
            Warrior attacker = new("Gemata", 10, 90);
            Warrior defender = new("Stoyan", 5, hp);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                attacker.Attack(defender));

            Assert.AreEqual("Enemy HP must be greater than 30 in order to attack him!", exception.Message);
        }

        [Test]
        public void Warrior_ShouldNotAttackEnemyWithBiggerDamageThanHisHealth()
        {
            Warrior attacker = new("Gemata", 10, 35);
            Warrior defender = new("Stoyan", 45, 100);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                attacker.Attack(defender));

            Assert.AreEqual("You are trying to attack too strong enemy", exception.Message);
        }

        [Test]
        public void EnemyHp_ShouldBeSetToZero_IfWarriorDamageIsGreaterThanHisHp()
        {
            Warrior attacker = new("Gemata", 50, 100);
            Warrior defender = new("Stoyan", 45, 40);

            int expectedAttackerHp = 55;
            int expectedDefenderHp = 0;

            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHp, attacker.HP);
            Assert.AreEqual(expectedDefenderHp, defender.HP);
        }
    }
}