namespace NauticalCatchChallenge.Models;

public class FreeDiver : Diver
{
    private const int givenOxygenLevel = 120;
    public FreeDiver(string name)
        : base(name, givenOxygenLevel)
    {
    }

    public override void Miss(int TimeToCatch)
    {
        double amountDecreased = 0.6 * TimeToCatch;

        int roundedDecrease = (int)Math.Round(amountDecreased);

        OxygenLevel -= roundedDecrease;
    }

    public override void RenewOxy()
    {
        OxygenLevel = givenOxygenLevel;
    }
}