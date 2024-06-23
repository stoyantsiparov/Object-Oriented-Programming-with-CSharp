using Raiding.Factories.Interfaces;
using Raiding.Models.Interfaces;
using Raiding.Models;

namespace Raiding.Factories;

public class HeroFactory : IHeroFactory
{
    public IHero Create(string type, string name)
    {
        switch (type)
        {
            case "Druid":
                return new Druid(name);
            case "Paladin":
                return new Paladin(name);
            case "Rogue":
                return new Rogue(name);
            case "Warrior":
                return new Warrior(name);
            default:
                throw new ArgumentException("Invalid hero!");
        }
    }
}