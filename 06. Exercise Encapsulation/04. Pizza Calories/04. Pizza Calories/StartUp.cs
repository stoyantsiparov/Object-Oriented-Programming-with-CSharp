using PizzaCalories.Models;

try
{
    string[] pizzaTokens = Console.ReadLine().Split(" ");
    string[] doughTokens = Console.ReadLine().Split(" ");

    string pizzaName = pizzaTokens[1];

    string flourType = doughTokens[1];
    string bakingTechnique = doughTokens[2];
    string flourWeight = doughTokens[3];

    Dough dough = new(flourType, bakingTechnique, double.Parse(flourWeight));

    Pizza pizza = new(pizzaName, dough);

    string toppingsInput;
    while ((toppingsInput = Console.ReadLine()) != "END")
    {
        string[] toppingTokens = toppingsInput.Split(" ");

        string toppingType = toppingTokens[1];
        string toppingsWeight = toppingTokens[2];

        Topping topping = new(toppingType, double.Parse(toppingsWeight));

        pizza.AddTopping(topping);
    }

    Console.WriteLine(pizza.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}