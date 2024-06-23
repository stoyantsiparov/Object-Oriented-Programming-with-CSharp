namespace Handball.Models;

public class ForwardWing : Player
{
    public ForwardWing(string name)
        : base(name, 5.5)
    {
    }

    public override void IncreaseRating()
    {
        Rating += 1.25;
    }

    public override void DecreaseRating()
    {
        Rating -= 0.75;
    }
}