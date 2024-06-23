namespace PizzaCalories.Models;

public class Dough
{
    private const double BaseCaloriesPerGram = 2;

    private Dictionary<string, double> flowerTypesCalories;
    private Dictionary<string, double> bakingTechniquesCalories;

    private string flourType;
    private string bakingTechnique;
    private double weight;

    // Правя конструктор
    public Dough(string flourType, string bakingTechnique, double weight)
    {
        // Въвеждам видовете тесто и техните калории като начални стойности на {flowerTypesCalories} колекцията (const)
        flowerTypesCalories =
            new Dictionary<string, double>
            {
                { "white", 1.5 }, 
                { "wholegrain", 1.0 }
            };

        // Въвеждам начините на печене и техните калории като начални стойности на {bakingTechniquesCalories} колекцията (const)
        bakingTechniquesCalories =
            new Dictionary<string, double>
            {
                { "crispy", 0.9 }, 
                { "chewy", 1.1 }, 
                { "homemade", 1.0 }
            };

        FlourType = flourType;
        BakingTechnique = bakingTechnique;
        Weight = weight;
    }

    public string FlourType
    {
        get => flourType;
        private set
        {
            if (!flowerTypesCalories.ContainsKey(value.ToLower()))
            {
                throw new ArgumentException(ExceptionDoughMessages.FlourTypeException);
            }

            flourType = value.ToLower();
        }
    }
    public string BakingTechnique
    {
        get => bakingTechnique;
        private set
        {
            if (!bakingTechniquesCalories.ContainsKey(value.ToLower()))
            {
                throw new ArgumentException(ExceptionDoughMessages.FlourTypeException);
            }

            bakingTechnique = value.ToLower();
        }
    }
    public double Weight
    {
        get => weight;
        private set
        {
            if (value < 0 || value > 200)
            {
                throw new ArgumentException(ExceptionDoughMessages.WeightException);
            }

            weight = value;
        }
    }

    public double Calories
    {
        get
        {
            // От {flowerTypesCalories} колекцията взимам [FlourType], което ми дава калориите на вида тесто
            double flourTypeModifier = flowerTypesCalories[FlourType];

            // От {bakingTechniqueModifier} колекцията взимам [BakingTechnique], което ми дава калориите на техниката за печене
            double bakingTechniqueModifier = bakingTechniquesCalories[BakingTechnique]; 

            // Изчислявам калориите на изпеченото тесто
            return BaseCaloriesPerGram * Weight * flourTypeModifier * bakingTechniqueModifier;
        }
    }
}