using System.Xml.Linq;

namespace FootballTeamGenerator.Models;

public class Player
{

    private const string StartOutOfRangeExceptionMessage = "{0} should be between 0 and 100.";

    private const int StatMinValue = 0;
    private const int StatMaxValue = 100;

    private string name;
    private int endurance;
    private int sprint;
    private int dribble;
    private int passing;
    private int shooting;
    public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
    {
        Name = name;
        Endurance = endurance;
        Sprint = sprint;
        Dribble = dribble;
        Passing = passing;
        Shooting = shooting;
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

    public int Endurance
    {
        get => endurance;
        private set
        {
            if (CheckStartValue(value))
            {
                throw new ArgumentException(string.Format(Exception.StartOutOfRangeExceptionMessage, nameof(Endurance)));
            }

            endurance = value;
        }
    }

    public int Sprint
    {
        get => sprint;
        private set
        {
            if (CheckStartValue(value))
            {
                throw new ArgumentException(string.Format(Exception.StartOutOfRangeExceptionMessage, nameof(Sprint)));
            }

            sprint = value;
        }
    }

    public int Dribble
    {
        get => dribble;
        private set
        {
            if (CheckStartValue(value))
            {
                throw new ArgumentException(string.Format(Exception.StartOutOfRangeExceptionMessage, nameof(Dribble)));
            }

            dribble = value;
        }
    }

    public int Passing
    {
        get => passing;
        private set
        {
            if (CheckStartValue(value))
            {
                throw new ArgumentException(string.Format(Exception.StartOutOfRangeExceptionMessage, nameof(Passing)));
            }

            passing = value;
        }
    }

    public int Shooting
    {
        get => shooting;
        private set
        {
            if (CheckStartValue(value))
            {
                throw new ArgumentException(string.Format(Exception.StartOutOfRangeExceptionMessage, nameof(Shooting)));
            }

            shooting = value;
        }
    }

    public double Stats => 
        ((Endurance + Sprint + Dribble + Passing + Shooting) / 5.0);
    private bool CheckStartValue(int value) =>
        value < StatMinValue || value > StatMaxValue;
}