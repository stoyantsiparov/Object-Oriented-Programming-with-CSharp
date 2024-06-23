using System;
using System.Linq;
using System.Text;
using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Core;

public class Controller : IController
{
    private IRepository<IUser> users;
    private IRepository<IVehicle> vehicles;
    private IRepository<IRoute> routes;

    public Controller()
    {
        users = new UserRepository();
        vehicles = new VehicleRepository();
        routes = new RouteRepository();
    }

    public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
    {
        var user = users.FindById(drivingLicenseNumber);

        if (user != null)
        {
            //return $"{drivingLicenseNumber} is already registered in our platform.";
            return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
        }

        user = new User(firstName, lastName, drivingLicenseNumber);
        users.AddModel(user);

        //return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
        return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
    }

    public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
    {
        if (vehicleType != "PassengerCar" && vehicleType != "CargoVan")
        {
            //return $"{vehicleType} is not accessible in our platform.";
            return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
        }

        var vehicle = vehicles.FindById(licensePlateNumber);

        if (vehicle != null)
        {
            //return $"{drivingLicenseNumber} is already registered in our platform.";
            return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
        }
        else
        {
            if (vehicleType == "PassengerCar")
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else if (vehicleType == "CargoVan")
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
        }

        vehicles.AddModel(vehicle);

        //return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
        return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
    }

    public string AllowRoute(string startPoint, string endPoint, double length)
    {
        var routeId = routes.GetAll().Count + 1;

        var route = routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint);

        if (route != null)
        {
            if (route.Length == length)
            {
                //return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
                return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }
            else if (route.Length < length)
            {
                //return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
                return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }
            else if (route.Length > length)
            {
                route.LockRoute();
            }
        }

        var newRoute = new Route(startPoint, endPoint, length, routeId);
        routes.AddModel(newRoute);

        //return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
    }

    public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
    {
        var user = users.FindById(drivingLicenseNumber);
        var vehicle = vehicles.FindById(licensePlateNumber);
        var route = routes.FindById(routeId);

        if (user.IsBlocked == true)
        {
            //return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
        }
        if (vehicle.IsDamaged == true)
        {
            //return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
        }
        if (route.IsLocked == true)
        {
            //return $"Route {routeId} is locked! Trip is not allowed.";
            return string.Format(OutputMessages.RouteLocked, routeId);
        }

        vehicle.Drive(route.Length);

        if (isAccidentHappened)
        {
            vehicle.ChangeStatus();
            user.DecreaseRating();
        }
        else
        {
            user.IncreaseRating();
        }

        return vehicle.ToString().TrimEnd();
    }

    public string RepairVehicles(int count)
    {
        var damagedVehicles = vehicles
            .GetAll()
            .Where(v => v.IsDamaged == true)
            .OrderBy(v => v.Brand)
            .ThenBy(v => v.Model);

        int vehiclesCount = 0;

        if (damagedVehicles.Count() < count)
        {
            vehiclesCount = damagedVehicles.Count();
        }
        else
        {
            vehiclesCount = count;
        }

        var chosenVehicles = damagedVehicles.Take(vehiclesCount).ToArray();

        foreach (var vehicle in chosenVehicles)
        {
            vehicle.ChangeStatus();
            vehicle.Recharge();
        }

        //return $"{vehiclesCount} vehicles are successfully repaired!";
        return string.Format(OutputMessages.RepairedVehicles, vehiclesCount);
    }

    public string UsersReport()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("*** E-Drive-Rent ***");

        var userInfo = users
            .GetAll()
            .OrderByDescending(u => u.Rating)
            .ThenBy(u => u.LastName)
            .ThenBy(u => u.FirstName);

        foreach (var user in userInfo)
        {
            sb.AppendLine(user.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}