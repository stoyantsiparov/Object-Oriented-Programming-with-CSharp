using System.IO;
using System.Text;
using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;

namespace NauticalCatchChallenge.Core;

public class Controller : IController
{
    private IRepository<IDiver> divers;
    private IRepository<IFish> fishes;

    public Controller()
    {
        divers = new DiverRepository();
        fishes = new FishRepository();
    }

    public string DiveIntoCompetition(string diverType, string diverName)
    {
        if (diverType != "FreeDiver" && diverType != "ScubaDiver")
        {
            return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
        }

        var diver = divers.GetModel(diverName);

        if (diver != null)
        {
            return string.Format(OutputMessages.DiverNameDuplication, diverName, "DiverRepository");
        }
        else
        {
            if (diverType == "FreeDiver")
            {
                diver = new FreeDiver(diverName);
            }
            else if (diverType == "ScubaDiver")
            {
                diver = new ScubaDiver(diverName);
            }

            divers.AddModel(diver);

            return string.Format(OutputMessages.DiverRegistered, diverName, "DiverRepository");
        }
    }

    public string SwimIntoCompetition(string fishType, string fishName, double points)
    {
        if (fishType != "PredatoryFish" && fishType != "ReefFish" && fishType != "DeepSeaFish")
        {
            return string.Format(OutputMessages.FishTypeNotPresented, fishType);
        }

        var fish = fishes.GetModel(fishName);

        if (fish != null)
        {
            return string.Format(OutputMessages.FishNameDuplication, fishName, "FishRepository");
        }
        else
        {
            if (fishType == "PredatoryFish")
            {
                fish = new PredatoryFish(fishName, points);
            }
            else if (fishType == "ReefFish")
            {
                fish = new ReefFish(fishName, points);
            }
            else if (fishType == "DeepSeaFish")
            {
                fish = new DeepSeaFish(fishName, points);
            }
        }

        fishes.AddModel(fish);

        return string.Format(OutputMessages.FishCreated, fishName);
    }

    public string ChaseFish(string diverName, string fishName, bool isLucky)
    {
        var diver = divers.GetModel(diverName);
        if (diver == null)
        {
            return string.Format(OutputMessages.DiverNotFound, "DiverRepository", diverName);
        }

        var fish = fishes.GetModel(fishName);
        if (fish == null)
        {
            return string.Format(OutputMessages.FishNotAllowed, fishName);
        }

        if (diver.HasHealthIssues)
        {
            return string.Format(OutputMessages.DiverHealthCheck, diverName);
        }

        int timeToCatch = diver.OxygenLevel;
        if (timeToCatch < fish.TimeToCatch)
        {
            diver.Miss(fish.TimeToCatch);

            if (diver.OxygenLevel <= 0)
            {
                diver.UpdateHealthStatus();
            }

            return string.Format(OutputMessages.DiverMisses, diverName, fishName);
        }
        else if (timeToCatch == fish.TimeToCatch)
        {
            if (isLucky)
            {
                diver.Hit(fish);

                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
            }
            else
            {
                diver.Miss(fish.TimeToCatch);

                if (diver.OxygenLevel <= 0)
                {
                    diver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
        }
        else
        {
            diver.Hit(fish);

            if (diver.OxygenLevel <= 0)
            {
                diver.UpdateHealthStatus();
            }

            return string.Format(OutputMessages.DiverHitsFish, diverName, fish.Points, fishName);
        }
    }

    public string HealthRecovery()
    {
        var diversWithHealthIssues = divers.Models.Where(diver => diver.HasHealthIssues);
        int recoveredDiversCount = 0;

        foreach (var diver in diversWithHealthIssues)
        {
            diver.UpdateHealthStatus();
            diver.RenewOxy();
            recoveredDiversCount++;
        }

        return string.Format(OutputMessages.DiversRecovered, recoveredDiversCount);
    }

    public string DiverCatchReport(string diverName)
    {
        var diver = divers.GetModel(diverName);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(diver.ToString());
        sb.AppendLine("Catch Report:");

        foreach (var fishName in diver.Catch)
        {
            var fish = fishes.GetModel(fishName);
            sb.AppendLine($"{fish.GetType().Name}: {fishName} [ Points: {fish.Points}, Time to Catch: {fish.TimeToCatch} seconds ]");
        }

        return sb.ToString().TrimEnd();
    }

    public string CompetitionStatistics()
    {
        var diversWithoutHealthIssues = divers.Models
            .Where(diver => !diver.HasHealthIssues)
            .OrderByDescending(diver => Math.Round(diver.CompetitionPoints))
            .ThenByDescending(diver => diver.Catch.Count)
            .ThenBy(diver => diver.Name)
            .ToList();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("**Nautical-Catch-Challenge**");

        foreach (var diver in diversWithoutHealthIssues)
        {
            sb.AppendLine(diver.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}