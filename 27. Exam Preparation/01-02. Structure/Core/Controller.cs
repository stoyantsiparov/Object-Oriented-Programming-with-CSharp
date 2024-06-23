using System.Linq;
using System.Text;
using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Core;

public class Controller : IController
{
    private IRepository<ISupplement> supplements;
    private IRepository<IRobot> robots;

    public Controller()
    {
        supplements = new SupplementRepository();
        robots = new RobotRepository();
    }

    public string CreateRobot(string model, string typeName)
    {
        IRobot newRobot;

        switch (typeName)
        {
            case "DomesticAssistant":
                newRobot = new DomesticAssistant(model);
                break;
            case "IndustrialAssistant":
                newRobot = new IndustrialAssistant(model);
                break;
            default:
                // return $"Robot type {typeName} cannot be created.";
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
        }

        robots.AddNew(newRobot);
        // return "{typeName} {model} is created and added to the RobotRepository.";
        return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
    }

    public string CreateSupplement(string typeName)
    {
        ISupplement newSupplement;

        switch (typeName)
        {
            case "SpecializedArm":
                newSupplement = new SpecializedArm();
                break;
            case "LaserRadar":
                newSupplement = new LaserRadar();
                break;
            default:
                // return "{typeName} is not compatible with our robots."
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
        }

        supplements.AddNew(newSupplement);
        // return "{typeName} is created and added to the SupplementRepository.";
        return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
    }

    public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
    {
        var selectedRobots = this.robots.Models().Where(r => r.InterfaceStandards.Any(i => i == intefaceStandard)).OrderByDescending(y => y.BatteryLevel);

        if (selectedRobots.Count() == 0)
        {
            return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
        }

        int powerSum = selectedRobots.Sum(r => r.BatteryLevel);

        if (powerSum < totalPowerNeeded)
        {
            return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - powerSum);
        }

        int usedRobotsCount = 0;

        foreach (var robot in selectedRobots)
        {
            usedRobotsCount++;

            if (totalPowerNeeded <= robot.BatteryLevel)
            {
                robot.ExecuteService(totalPowerNeeded);
                break;
            }
            else
            {
                totalPowerNeeded -= robot.BatteryLevel;
                robot.ExecuteService(robot.BatteryLevel);
            }

        }

        return string.Format(OutputMessages.PerformedSuccessfully, serviceName, usedRobotsCount);
    }

    public string Report()
    {
        StringBuilder sb = new StringBuilder();

        var robotReportCollection = this.robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(b => b.BatteryCapacity);

        foreach (var robot in robotReportCollection)
        {
            sb.AppendLine(robot.ToString());
        }

        return sb.ToString().TrimEnd();
    }

    public string RobotRecovery(string model, int minutes)
    {
        var selectedRobots = this.robots.Models().Where(r => r.Model == model && r.BatteryLevel * 2 < r.BatteryCapacity);
        int robotsFed = 0;

        foreach (var robot in selectedRobots)
        {
            robot.Eating(minutes);
            robotsFed++;
        }

        return string.Format(OutputMessages.RobotsFed, robotsFed);
    }

    public string UpgradeRobot(string model, string supplementTypeName)
    {
        ISupplement supplement = this.supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);

        var selectedModels = this.robots.Models().Where(r => r.Model == model);
        var stillNotUpgraded = selectedModels.Where(r => r.InterfaceStandards.All(s => s != supplement.InterfaceStandard));
        var robotForUpgrade = stillNotUpgraded.FirstOrDefault();

        if (robotForUpgrade == null)
        {
            return string.Format(OutputMessages.AllModelsUpgraded, model);
        }


        robotForUpgrade.InstallSupplement(supplement);
        this.supplements.RemoveByName(supplementTypeName);

        return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
    }
}