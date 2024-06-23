using Vehicles.Core.Interfaces;
using Vehicles.Factories.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models.Interfaces;

namespace VehiclesExtension.Core;

public class Engine : IEngine
{
    // {readonly} се инциализира САМО веднъж (повече не може да извиква надолу в кода)
    private readonly IReader reader;
    private readonly IWriter writer;
    private readonly IVehicleFactory vehicleFactory;

    // Създавам колекция {ICollection}, а не {List}, защото не ми трябват токова методи, не използвам и {IEnumerable}, защото няма метода {.Add} (добавям {ICollection}, защото искам само да {.Add-на} и {foreeach-на} елементите)
    private readonly ICollection<IVehicle> vehicles;

    public Engine(IReader reader, IWriter writer, IVehicleFactory vehicleFactory)
    {
        this.reader = reader;
        this.writer = writer;
        this.vehicleFactory = vehicleFactory;

        vehicles = new List<IVehicle>();
    }
    public void Run()
    {
        // Довабям ново превозно средство в колекцията {vehicles} с метода {CreateVehicle} го създавам
        vehicles.Add(CreateVehicle()); //add Car
        vehicles.Add(CreateVehicle()); //add Truck
        vehicles.Add(CreateVehicle()); //add Bus

        int commandCount = int.Parse(reader.ReadLine());

        for (int i = 0; i < commandCount; i++)
        {
            try
            {
                ProcessCommand();
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
            }
        }

        foreach (var vehicle in vehicles)
        {
            writer.WriteLine(vehicle.ToString());
        }
    }

    // Създавам метод, в който създавам ново превозно средство
    private IVehicle CreateVehicle()
    {
        string[] tokens = reader.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        // Създавам ново превозно средство от тип {IVehicle}
        // {IVehicleFactory} приема 4 парамета -> тип на превозно средство, гориво в резервуара, колко гориво харчи за км. и капацитет на резервуара (а метода {Create} го създава)
        IVehicle vehicle = vehicleFactory.Create(tokens[0], double.Parse(tokens[1]), double.Parse(tokens[2]), double.Parse(tokens[3]));

        return vehicle;
    }

    // Създавам метод, в който изпълнявам командите дадени ми от конзолата ({"Drive"} и {"Refuel"})
    private void ProcessCommand()
    {
        string[] commandTokens = reader.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        string command = commandTokens[0];
        string vehicleType = commandTokens[1];

        IVehicle vehicle = vehicles
            .FirstOrDefault(v => v.GetType().Name == vehicleType);

        if (vehicle == null)
        {
            throw new ArgumentException("Invalid vehicle type");
        }

        if (command == "Drive")
        {
            double distance = double.Parse(commandTokens[2]);
            writer.WriteLine(vehicle.Drive(distance));
        }
        else if (command == "DriveEmpty")
        {
            double distance = double.Parse(commandTokens[2]);
            writer.WriteLine(vehicle.Drive(distance, false));
        }
        else if (command == "Refuel")
        {
            double fuelAmount = double.Parse(commandTokens[2]);
            vehicle.Refuel(fuelAmount);
        }
    }
}