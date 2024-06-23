using BorderControl.Core.Interfaces;
using BorderControl.Models.Interfaces;
using BorderControl.Models;

namespace BorderControl.Core;

public class Engine : IEngine
{
    public void Run()
    {
        // Създавам нов списък от {IBuyer}, в който се пазят стойностите на хората които са си купили храна
        List<IBuyer> buyers = new();

        // От конзолата получавам броя на хората
        int count = int.Parse(Console.ReadLine());

        // Въртя един цикъл до бройката на хората
        for (int i = 0; i < count; i++)
        {
            // От конзолата получавам информация за хората
            string input = Console.ReadLine();
            string[] tokens = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // Ако дължината на сплитнатия стринг масив е равна на 4 (добавям Гражданин)
            if (tokens.Length == 4)
            {
                IBuyer citizen = new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2], tokens[3]);
                buyers.Add(citizen);
            }
            // Ако дължината на сплитнатия стринг масив не е равна на 4 (добавям Бунтовник)
            else
            {
                IBuyer rebel = new Rebel(tokens[0], int.Parse(tokens[1]), tokens[2]);
                buyers.Add(rebel);
            }
        }

        // Създавам празна променлива за хората купили храна
        string peopleBoughtFood;

        // Въртя цикъл, докато не получа командата "End"
        while ((peopleBoughtFood = Console.ReadLine()) != "End")
        {
            // Проверявам дали в списъка има име на човек, което да съвпада с име на куповач
            // Ако името на човека съвпада с името на куповача, метода {BuyFood()} се активира и храна се купува (с помощта на оператора за условен достъп {?})
            buyers.FirstOrDefault(buyer => buyer.Name == peopleBoughtFood)?.BuyFood();
        }

        // Принтирам сумата която да дали куповачите за храна
        Console.WriteLine(buyers.Sum(b => b.Food));
    }
}