using Vehicles.Models.Interfaces;

namespace Vehicles.Factories.Interfaces;

public interface IVehicleFactory
{
    // Създавам този интерфейс и в него приемам типа на превозното средство, колко гориво има, колко гориво харчи за км. и колко е капацитета на резервуара
    IVehicle Create(string type, double fuelQuantity, double fuelConsumption, double tankCapacity);
}