using Telephony.Models.Interfaces;

namespace Telephony.Models;

public class Smartphone : ICallable, IBrowsable
{
    public string Call(string phoneNumber)
    {
        if (!ValidatePhoneNumber(phoneNumber))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Calling... {phoneNumber}";
    }

    public string Browse(string url)
    {
        if (!ValidateUrl(url))
        {
            throw new ArgumentException("Invalid URL!");
        }

        return $"Browsing: {url}!";
    }


    private bool ValidatePhoneNumber(string phoneNumber)
    {
        // Проверявам всички елементи на телефонния номер и ако всички са числа е валиден тел.номер
        return phoneNumber.All(c => char.IsDigit(c));
    }
    private bool ValidateUrl(string url)
    {
        // Проверявам всички елементи на url адреса и ако всички не са числа е валиден url адрес
        return url.All(c => !char.IsDigit(c));
    }
}