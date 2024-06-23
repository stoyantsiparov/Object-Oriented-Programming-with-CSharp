namespace NauticalCatchChallenge.Models;

public class ScubaDiver : Diver
{
    private const int givenOxygenLevel = 540;

    public ScubaDiver(string name)
        : base(name, givenOxygenLevel)
    {
    }

    public override void Miss(int TimeToCatch)
    {
        double amountDecreased = 0.3 * TimeToCatch;

        int roundedDecrease = (int)Math.Round(amountDecreased);

        OxygenLevel -= roundedDecrease;
    }

    public override void RenewOxy()
    {
        OxygenLevel = givenOxygenLevel;
    }
}