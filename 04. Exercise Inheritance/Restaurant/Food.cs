namespace Restaurant;

public abstract class Food : Product
{
    protected Food(string name, decimal price, double grams)
        : base(name, price)
    {
        Grams = grams;
    }

    public double Grams { get; set; }
}