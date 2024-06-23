using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Models;

public abstract class Robot : IRobot
{
    private string model;
    private int batteryCapacity;
    private List<int> interfaceStandards;

    protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
    {
        Model = model;
        BatteryCapacity = batteryCapacity;
        ConvertionCapacityIndex = convertionCapacityIndex;
    }


    public string Model
    {
        get => model;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Model cannot be null or empty.");
                throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
            }

            model = value;
        }
    }

    public int BatteryCapacity
    {
        get => batteryCapacity;
        private set
        {
            if (value < 0)
            {
                //throw new ArgumentException("Battery capacity cannot drop below zero.");
                throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
            }

            batteryCapacity = value;
        }
    }
    public int BatteryLevel { get; private set; }
    public int ConvertionCapacityIndex { get; private set; }
    public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();
    public void Eating(int minutes)
    {
        int totalCapacity = ConvertionCapacityIndex * minutes;

        if (BatteryCapacity - BatteryLevel < totalCapacity)
        {
            BatteryLevel = BatteryCapacity;
        }
        else
        {
            int newBatteryLevel = BatteryLevel + totalCapacity;
            BatteryLevel = newBatteryLevel;
        }
    }

    public void InstallSupplement(ISupplement supplement)
    {
        interfaceStandards.Add(supplement.InterfaceStandard);

        BatteryCapacity -= supplement.BatteryUsage;

        BatteryLevel -= supplement.BatteryUsage;
    }

    public bool ExecuteService(int consumedEnergy)
    {
        if (BatteryLevel >= consumedEnergy)
        {
            BatteryLevel -= consumedEnergy;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{GetType().Name} {Model}:");
        sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
        sb.AppendLine($"--Current battery level: {BatteryLevel}");

        if (InterfaceStandards.Any())
        {
            sb.AppendLine(string.Join(" ", InterfaceStandards));
        }
        else
        {
            sb.AppendLine("none");
        }

        return sb.ToString().TrimEnd();
    }
}