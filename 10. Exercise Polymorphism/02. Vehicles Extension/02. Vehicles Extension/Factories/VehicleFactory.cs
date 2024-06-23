using Vehicles.Factories.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Factories;

public class VehicleFactory : IVehicleFactory
{
    //Създавам този клас, за да нямамам преповтаряне на код за всяко ново превозно средство, което трябва да се добавя
    public IVehicle Create(string type, double fuelQuantity, double fuelConsumption, double tankCapacity)
    {
        // Приемам типа на превозното средство
        switch (type)
        {
            // Създавам превозно средство, ако отговяря на данените кейсове
            case "Car":
                return new Car(fuelQuantity, fuelConsumption, tankCapacity);
            case "Truck":
                return new Truck(fuelQuantity, fuelConsumption, tankCapacity);
            case "Bus":
                return new Bus(fuelQuantity, fuelConsumption, tankCapacity);
            // Ако няма такова превозно средства хвърлям грешка
            default:
                throw new ArgumentException("Invalid vehicle type");
        }
    }
}