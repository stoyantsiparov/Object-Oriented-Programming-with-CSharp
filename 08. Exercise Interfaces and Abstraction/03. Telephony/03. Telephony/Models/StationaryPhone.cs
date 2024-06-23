using Telephony.Models.Interfaces;

namespace Telephony.Models;

public class StationaryPhone : ICallable
{
    public string Call(string phoneNumber)
    {
        if (!ValidatePhoneNumber(phoneNumber))
        {
            throw new ArgumentException("Invalid number!");
        }

        return $"Dialing... {phoneNumber}";
    }

    private bool ValidatePhoneNumber(string phoneNumber)
    {
        // Проверявам всички елементи на телефонния номер и ако всички са числа е валиден тел.номер
        return phoneNumber.All(c => char.IsDigit(c));
    }
}