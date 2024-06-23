namespace Restaurant;

public abstract class Beverage : Product
{
    protected Beverage(string name, decimal price, double milliliters)
        : base(name, price)
    {
        Milliliters = milliliters;
    }

    public double Milliliters { get; private set; }
}