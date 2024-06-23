namespace Restaurant;

public abstract class ColdBeverage : Beverage
{
    protected ColdBeverage(string name, decimal price, double milliliters)
        : base(name, price, milliliters)
    {
    }
}