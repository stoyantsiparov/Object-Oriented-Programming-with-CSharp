using System;
using System.Security.Cryptography;
using System.Text;
using Handball.Models.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Models;

public abstract class Player : IPlayer
{
    private string name;
    private double rating;
    private string team;

    protected Player(string name, double rating)
    {
        Name = name;
        Rating = rating;
    }

    public string Name
    {
        get => name;

        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.PlayerNameNull);
            }

            name = value;
        }
    }

    // Тук е {protected set}, зашото рейтинга се ползва в абстрактен метод и така децата ще могат да променят рейтинга (затова не е {private set})
    public double Rating
    {
        get => rating;
        protected set
        {
            if (value > 10)
            {
                rating = 10;
                return;
            }
            else if (value < 1)
            {
                rating = 1;
                return;
            }

            rating = value;
        }
    }

    // Има само {get}, зашото ще го променям само през метода
    public string Team
    {
        get => team;
    }
    public void JoinTeam(string name)
    {
        team = name;
    }

    public abstract void IncreaseRating();

    public abstract void DecreaseRating();

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"{GetType().Name}: {Name}");
        result.AppendLine($"--Rating: {Rating}");

        return result.ToString().TrimEnd();
    }
}