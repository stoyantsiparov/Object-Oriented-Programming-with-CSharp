using System.Xml.Linq;

namespace FootballTeamGenerator.Models;

public class Team
{
    private string name;
    private List<Player> players;

    public Team(string name)
    {
        Name = name;
        players = new List<Player>();
    }
    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Exception.NameExceptionMessage);
            }

            name = value;
        }
    }

    public double Rating
    {
        get
        {
            if (players.Any())
            {
                return players.Average(p => p.Stats);
            }

            return 0;
        }
    }

    public void AddPlayer(Player player) => 
        players.Add(player);

    public void RemovePlayer(string playerName)
    {
        // Премахвам играча ако ми е подадено името (и го има в списъка)
        Player player = players.FirstOrDefault(p => p.Name == playerName);

        if (player == null)
        {
            throw new ArgumentException(string.Format(Exception.PlayerNotFoundExceptionMessage, playerName, Name));
        }

        players.Remove(player);
    }
}