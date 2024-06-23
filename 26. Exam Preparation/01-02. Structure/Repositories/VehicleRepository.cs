using System.Collections.Generic;
using System.Linq;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories;

public class VehicleRepository : IRepository<IVehicle>
{
    private List<IVehicle> vehicles;

    public VehicleRepository()
    {
        vehicles = new List<IVehicle>();
    }

    public void AddModel(IVehicle model) => vehicles.Add(model);

    public bool RemoveById(string identifier) => vehicles.Remove(FindById(identifier));

    public IVehicle FindById(string identifier) => vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);

    public IReadOnlyCollection<IVehicle> GetAll() => vehicles.AsReadOnly();
}