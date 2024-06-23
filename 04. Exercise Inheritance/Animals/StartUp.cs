using System;
using static System.Net.Mime.MediaTypeNames;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string animalType;
            while ((animalType = Console.ReadLine()) != "Beast!")
            {
                string[] tokens = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string gender = tokens[2];

                // В {try} и {catch} проверявам кода за {Exception-и} и ако има да такива да изпише на конзолата съобщението от {Animal} класа -> ("Invalid input!")
                try
                {
                    switch (animalType)
                    {
                        case "Dog":
                            Dog dog = new(name, age, gender);
                            Console.WriteLine(animalType);
                            Console.WriteLine(dog.ToString());
                            break;
                        case "Frog":
                            Frog frog = new(name, age, gender);
                            Console.WriteLine(animalType);
                            Console.WriteLine(frog.ToString());
                            break;
                        case "Cat":
                            Cat cat = new(name, age, gender);
                            Console.WriteLine(animalType);
                            Console.WriteLine(cat.ToString());
                            break;
                        case "Tomcat":
                            Tomcat tomcat = new(name, age);
                            Console.WriteLine(animalType);
                            Console.WriteLine(tomcat.ToString());
                            break;
                        case "Kitten":
                            Kitten kitten = new(name, age);
                            Console.WriteLine(animalType);
                            Console.WriteLine(kitten.ToString());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
