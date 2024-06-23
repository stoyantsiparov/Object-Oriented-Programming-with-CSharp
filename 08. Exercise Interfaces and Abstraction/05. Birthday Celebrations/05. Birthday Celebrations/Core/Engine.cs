using BorderControl.Core.Interfaces;
using BorderControl.Models.Interfaces;
using BorderControl.Models;

namespace BorderControl.Core;

public class Engine : IEngine
{
    public void Run()
    {
        // Създавам нов списък от {IBirthable}, в който се пазят стойностите на рождените дати (защото {IBirthable} интерфейса инициализира само стойностите на рождените дати)
        List<IBirthable> society = new();
        string input;

        // Въртя 1 списък докато не получа командата "End"
        while ((input = Console.ReadLine()) != "End")
        {
            // Сплитвам инпута по празно пространство (спейс)
            string[] tokens = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string command = tokens[0];

            switch (command)
            {
                case "Citizen":
                    IBirthable citizen = new Citizen(tokens[1], int.Parse(tokens[2]), tokens[3], tokens[4]);
                    society.Add(citizen);
                    break;
                case "Pet":
                    IBirthable pet = new Pet(tokens[1], tokens[2]);
                    society.Add(pet);
                    break;
            }
        }

        // От конзолата получавам година на раждане
        string birthYear = Console.ReadLine();

        // Въртя се по списъка от години на раждане
        foreach (var element in society)
        {
            // Проверявам всяка дата на раждане с (EndsWith(birthYear), зашото последното число винаги е годината
            if (element.Birthdate.EndsWith(birthYear))
            {
                // Ако годината на раждане съвпада -> принтирам цялата дата на раждане
                Console.WriteLine(element.Birthdate);
            }
        }
    }
}