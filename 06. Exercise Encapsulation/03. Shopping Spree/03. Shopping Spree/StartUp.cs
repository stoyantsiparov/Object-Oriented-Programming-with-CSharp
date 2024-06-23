using ShoppingSpree.Models;

List<Person> people = new();
List<Product> products = new();

try
{
    string[] nameMoneyPairs = Console.ReadLine()
        .Split(";", StringSplitOptions.RemoveEmptyEntries);

    foreach (var nameMoneyPair in nameMoneyPairs)
    {
        string[] nameMoney = nameMoneyPair
            .Split("=", StringSplitOptions.RemoveEmptyEntries);

        string personName = nameMoney[0];
        string personMoney = nameMoney[1];
        Person person = new(personName, decimal.Parse(personMoney));

        people.Add(person);
    }

    string[] productCostPairs = Console.ReadLine()
        .Split(";", StringSplitOptions.RemoveEmptyEntries);

    foreach (var productCostPair in productCostPairs)
    {
        string[] productCost = productCostPair
            .Split("=", StringSplitOptions.RemoveEmptyEntries);

        string productName = productCost[0];
        string costOfProduct = productCost[1];
        Product product = new(productName, decimal.Parse(costOfProduct));

        products.Add(product);
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);

    return;
}

string input;
while ((input = Console.ReadLine()) != "END")
{
    string[] personProduct = input
        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string personName = personProduct[0];
    string productName = personProduct[1];

    // Изписвам името на човека
    Person person = people.FirstOrDefault(p => p.Name == personName);
    // Изписвам името на продукта
    Product product = products.FirstOrDefault(p => p.Name == productName);

    // Проверявам ако елементите не съществуват в двата списъка {person} и {product} (ако не съществуват ги добавям)
    if (person is not null && product is not null)
    {
        Console.WriteLine(person.Add(product));
    }
}

// Принтирам резултатите на нов ред {Environment.NewLine}
Console.WriteLine(string.Join(Environment.NewLine, people));