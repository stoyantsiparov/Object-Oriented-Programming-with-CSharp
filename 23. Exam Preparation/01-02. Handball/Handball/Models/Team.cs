using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Handball.Models.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Models;

public class Team : ITeam
{
    private string name;
    private int pointsEarned;
    private List<IPlayer> players;

    public Team(string name)
    {
        Name = name;
        players = new List<IPlayer>();
    }

    public string Name
    {
        get => name;

        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(ExceptionMessages.TeamNameNull);
            }

            name = value;
        }
    }

    public int PointsEarned
    {
        get => pointsEarned;

        private set
        {
            pointsEarned = value;
        }
    }

    public double OverallRating
    {
        get
        {
            if (players.Count == 0)
            {
                return 0;
            }

            // Взимам средния рейтинг на играч и го закръглям до 2 знака след десетичната запетая
            return Math.Round(players.Average(p => p.Rating), 2);
        }
    }

    // Създавам си списък от играчи който може дамо да се чете
    public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();

    public void SignContract(IPlayer player)
    {
        players.Add(player);
    }

    public void Win()
    {
        PointsEarned += 3;

        foreach (var player in players)
        {
            player.IncreaseRating();
        }
    }

    public void Lose()
    {
        foreach (var player in players)
        {
            player.DecreaseRating();
        }
    }

    public void Draw()
    {
        PointsEarned += 1;

        foreach (var player in players)
        {
            IPlayer goalKeeper = players.FirstOrDefault(p => p is Goalkeeper);

            if (goalKeeper != null)
            {
                goalKeeper.IncreaseRating();
            }
        }
    }

    public override string ToString()
    {
        // Създавам един стринг със стойност {"none"}
        string playersToStr = "none";
        // Ако броя на играчите е повече от 0 влизам в проверката
        if (players.Count > 0)
        {
            // Към стринга със стойност {"none"} добавям имената на различните играчи, разделени с запетая
            playersToStr = string.Join(", ", players.Select(p => p.Name));
        }

        StringBuilder result = new StringBuilder();
        result.AppendLine($"Team: {Name} Points: {PointsEarned}");
        result.AppendLine($"--Overall rating: {OverallRating}");
        // --Players: {name1}, {name2}…/none" -> пречата се така на конзолата
        result.AppendLine($"--Players: {playersToStr}");

        return result.ToString().Trim();
    }
}