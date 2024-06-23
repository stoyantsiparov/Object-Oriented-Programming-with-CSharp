namespace EDriveRent.Models;

public class PassengerCar : Vehicle
{
    public PassengerCar(string brand, string model, string licensePlateNumber)
        : base(brand, model, 450, licensePlateNumber)
    {
    }
}