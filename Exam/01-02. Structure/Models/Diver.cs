using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;

namespace NauticalCatchChallenge.Models;

public abstract class Diver : IDiver
{
    private string name;
    private int oxygenLevel;
    private readonly List<string> catchList;

    protected Diver(string name, int oxygenLevel)
    {
        Name = name;
        OxygenLevel = oxygenLevel;
        catchList = new List<string>();
        CompetitionPoints = 0.0;
        HasHealthIssues = false;
    }

    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.DiversNameNull);
            }

            name = value;
        }
    }

    public int OxygenLevel
    {
        get => oxygenLevel;
        protected set
        {
            if (value < 0)
            {
                oxygenLevel = 0;
            }
            else
            {
                oxygenLevel = value;
            }

            oxygenLevel = value;
        }
    }

    public IReadOnlyCollection<string> Catch => catchList.AsReadOnly();
    public double CompetitionPoints { get; private set; }
    public bool HasHealthIssues { get; private set; }
    public void Hit(IFish fish)
    {
        OxygenLevel -= fish.TimeToCatch;

        catchList.Add(fish.Name);

        CompetitionPoints += Math.Round(fish.Points, 2);
    }

    public abstract void Miss(int TimeToCatch);

    public void UpdateHealthStatus()
    {
        if (HasHealthIssues)
        {
            HasHealthIssues = false;
        }
        else
        {
            HasHealthIssues = true;
        }
    }

    public abstract void RenewOxy();

    public override string ToString() => $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {Catch.Count}, Points earned: {CompetitionPoints} ]";

}