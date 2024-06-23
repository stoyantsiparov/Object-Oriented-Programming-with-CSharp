using Telephony.Core.Interfaces;
using Telephony.Models;
using Telephony.Models.Interfaces;

namespace Telephony.Core;

public class Engine : IEngine
{
    public void Run()
    {
        string[] phoneNumbers = Console.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        string[] urls = Console.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        // Създавам интерфейс променлива {phone}, която е празна
        ICallable phone;

        // Обикалям всикчи номера и проверявам ако са с 10 цифри -> звъни смартфона, ако са 7 стационарния
        foreach (string phoneNumber in phoneNumbers)
        {
            if (phoneNumber.Length == 10)
            {
                // Извиквам интерфейса в класа {Smartphone}
                phone = new Smartphone();
            }
            else
            {
                // Извиквам интерфейса в класа {StationaryPhone}
                phone = new StationaryPhone();
            }

            // Проверявам за грешки
            try
            {
                // Ако няма грешки принтирам (имам само 1 промелива за 2та телефона, заради интерфейса)
                // Интерфейса прави кода преизползваем (само се интанцират класовете, за да се разбере кой телефон звъни)
                Console.WriteLine(phone.Call(phoneNumber));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Създавам интерфейс променлива {browsable}, която не е празна, защото САМО смартфона може да влиза в интернет
        IBrowsable browsable = new Smartphone();

        foreach (string url in urls)
        {
            try
            {
                Console.WriteLine(browsable.Browse(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}