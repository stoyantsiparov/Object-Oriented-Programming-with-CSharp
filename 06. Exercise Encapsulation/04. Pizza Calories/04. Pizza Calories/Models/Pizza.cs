namespace PizzaCalories.Models;

public class Pizza
{
    private string name;
    private List<Topping> toppings;

    public Pizza(string name, Dough dough)
    {
        Name = name;
        Dough = dough;
        toppings = new List<Topping>();
    }

    public string Name
    {
        get => name;
        private set
        {
            if (value.Length < 1 || value.Length > 15)
            {
                throw new ArgumentException(ExceptionPizzaMessage.NameException);
            }

            name = value;
        }
    }

    public Dough Dough { get; set; }

    public double Calories
    {
        get
        {
            // Смятам калориите на пицата (калориите на тестото + сумата от калориите на всички топинги)
            return Dough.Calories + toppings.Sum(t => t.Calories);
        }
    }

    public void AddTopping(Topping topping)
    {
        if (toppings.Count == 10)
        {
            throw new ArgumentException(ExceptionPizzaMessage.ToppingsException);
        }

        toppings.Add(topping);
    }

    public override string ToString() => $"{Name} - {Calories:F2} Calories.";
}