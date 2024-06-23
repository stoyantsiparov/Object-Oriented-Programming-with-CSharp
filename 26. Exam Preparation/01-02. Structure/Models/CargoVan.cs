using EDriveRent.Models.Contracts;

namespace EDriveRent.Models;

public class CargoVan : Vehicle
{
    public CargoVan(string brand, string model, string licensePlateNumber)
        : base(brand, model, 180, licensePlateNumber)
    {
    }
}