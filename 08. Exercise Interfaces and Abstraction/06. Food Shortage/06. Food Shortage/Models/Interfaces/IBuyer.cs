namespace BorderControl.Models.Interfaces;

public interface IBuyer : INameable
{
    public int Food { get; }
    public void BuyFood();
}