using System;
using NUnit.Framework;

namespace FootballTeam.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Constructor_Test()
        {
            FootballTeam team = new FootballTeam("Levski", 15);

            Assert.AreEqual("Levski", team.Name);
            Assert.AreEqual(15, team.Capacity);
            Assert.AreEqual(0, team.Players.Count);
        }

        [Test]
        public void TestName_Throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam(null, 15));
            Assert.Throws<ArgumentException>(() => new FootballTeam(String.Empty, 15));
        }

        [Test]
        public void TestCapacity_Throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => new FootballTeam("Levski", 0));
            Assert.Throws<ArgumentException>(() => new FootballTeam("Levski", 14));
        }

        [Test]
        public void AddNewPlayer_CapacityMinValue()
        {
            FootballTeam team = new FootballTeam("Levski", 15);

            for (int i = 1; i <= 15; i++)
            {
                team.AddNewPlayer(new FootballPlayer($"Stoyan{i}", i, "Forward"));
            }

            FootballPlayer player = new FootballPlayer("Stoyan", 5, "Forward");
            string expectedResult = team.AddNewPlayer(player);

            Assert.AreEqual(expectedResult, "No more positions available!");
        }

        [Test]
        public void AddNewPlayer_WorkCorrectly()
        {
            FootballTeam team = new FootballTeam("Levski", 15);
            FootballPlayer player = new FootballPlayer("Stoyan", 5, "Forward");
            string actualResult = team.AddNewPlayer(player);

            string expectedResult = $"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void PickPlayer_WorkCorrectly()
        {
            FootballTeam team = new FootballTeam("Levski", 15);
            FootballPlayer player = new FootballPlayer("Stoyan", 5, "Forward");

            team.AddNewPlayer(player);

            Assert.AreEqual(team.PickPlayer("Stoyan"), player);
            Assert.AreEqual(team.PickPlayer("Gemata"), null);
        }

        [Test]
        public void PlayerScore_WorkCorrectly()
        {
            FootballTeam team = new FootballTeam("Levski", 15);
            FootballPlayer player = new FootballPlayer("Stoyan", 5, "Forward");
            team.AddNewPlayer(player);

            string expectedResult = team.PlayerScore(5);

            Assert.AreEqual(1, player.ScoredGoals);
            Assert.AreEqual(expectedResult, $"{player.Name} scored and now has {player.ScoredGoals} for this season!");

        }
    }
}
