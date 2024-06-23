﻿using System;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models;

public abstract class Vehicle : IVehicle
{
    private string brand;
    private string model;
    private string licensePlateNumber;

    protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
    {
        Brand = brand;
        Model = model;
        MaxMileage = maxMileage;
        LicensePlateNumber = licensePlateNumber;
        BatteryLevel = 100;
        IsDamaged = false;
    }

    public string Brand
    {
        get => brand;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Brand cannot be null or whitespace!");
                throw new ArgumentException(ExceptionMessages.BrandNull);
            }

            brand = value;
        }
    }

    public string Model
    {
        get => model;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Model cannot be null or whitespace!");
                throw new ArgumentException(ExceptionMessages.ModelNull);
            }

            model = value;
        }
    }
    public double MaxMileage { get; private set; }

    public string LicensePlateNumber
    {
        get => licensePlateNumber;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("License plate number is required!");
                throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
            }

            licensePlateNumber = value;
        }
    }
    public int BatteryLevel { get; private set; }
    public bool IsDamaged { get; private set; }
    public void Drive(double mileage)
    {
        double percentage = Math.Round((mileage / MaxMileage) * 100);
        BatteryLevel -= (int)percentage;

        if (GetType().Name == "CargoVan")
        {
            BatteryLevel -= 5;
        }
    }

    public void Recharge() => BatteryLevel = 100;

    public void ChangeStatus()
    {
        if (!IsDamaged)
        {
            IsDamaged = true;
        }
        else
        {
            IsDamaged = false;
        }
    }

    public override string ToString()
    {
        string vehicleCondition;

        if (IsDamaged)
        {
            vehicleCondition = "damaged";
        }
        else
        {
            vehicleCondition = "OK";
        }

        return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {vehicleCondition}";
    }
}