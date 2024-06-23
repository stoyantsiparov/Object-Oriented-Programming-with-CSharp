using BorderControl.Core.Interfaces;
using BorderControl.Models.Interfaces;
using BorderControl.Models;

namespace BorderControl.Core;

public class Engine : IEngine
{
    public void Run()
    {
        // Създавам нов списък от {IIdentifiable}, в който се пазят стойностите на {Id-тата} (защото {IIdentifiable} интерфейса инициализира само стойностите на {Id-тата})
        List<IIdentifiable> society = new();
        string input;

        // Въртя 1 списък докато не получа командата "End"
        while ((input = Console.ReadLine()) != "End")
        {
            // Сплитвам инпута по празно пространство (стейс)
            string[] tokens = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            // Проверявам дължината на сплитнатия инпут
            if (tokens.Length == 3)
            {
                // Ако е равна на 3 създавам Гражданин с име,години и ИД
                IIdentifiable citizen = new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2]);
                society.Add(citizen);
            }
            else
            {
                // Ако не е с 3 създавам Робот с модел и ИД
                IIdentifiable citizen = new Robot(tokens[0], tokens[1]);
                society.Add(citizen);
            }
        }

        // От конзолата получавам число, което ако съвпада с края на ИД-то на Робот или Гражданин (трябва да се изпринтят)
        string invalidIdNumbers = Console.ReadLine();

        // Въртя се по списъка от ИД-та
        foreach (var element in society)
        {
            // Проверявам края на всяко ИД (EndsWith(invalidIdNumbers) дали завършва с числото, което е дадено от конзолата
            if (element.Id.EndsWith(invalidIdNumbers))
            {
                // Ако да го принтя
                Console.WriteLine(element.Id);
            }
        }
    }
}