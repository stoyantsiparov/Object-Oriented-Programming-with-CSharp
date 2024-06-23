using WildFarm.Models.Foods;

namespace WildFarm.Models.Animal;

public class Hen : Bird
{
    private const double HenWeightMultiplier = 0.35;
    public Hen(string name, double weight, double wingSize)
        : base(name, weight, wingSize)
    {
    }

    protected override double WeightMultiplier =>
        HenWeightMultiplier;
    protected override IReadOnlyCollection<Type> PreferredFoodTypes =>
        new HashSet<Type> { typeof(Meat), typeof(Vegetable), typeof(Fruit), typeof(Seeds) };

    public override string ProduceSound() =>
        "Cluck";
}