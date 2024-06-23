using WildFarm.Models.Interfaces;

namespace WildFarm.Models.Animal;

public abstract class Animal : IAnimal
{
    protected Animal(string name, double weight)
    {
        Name = name;
        Weight = weight;
    }

    public string Name { get; private set; }
    public double Weight { get; private set; }
    public int FoodEaten { get; private set; }

    protected abstract double WeightMultiplier { get; }
    protected abstract IReadOnlyCollection<Type> PreferredFoodTypes { get; }

    public void Eat(IFood food)
    {
        if (!PreferredFoodTypes.Any(pf => food.GetType().Name == pf.Name))
        {
            throw new ArgumentException($"{GetType().Name} does not eat {food.GetType().Name}!");
        }

        Weight += food.Quantity * WeightMultiplier;
        FoodEaten += food.Quantity;
    }

    public abstract string ProduceSound();

    public override string ToString() => 
        $"{GetType().Name} [{Name}, ";
}