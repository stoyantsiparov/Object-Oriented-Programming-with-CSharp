using System.Collections.Generic;
using System.Linq;
using System.Text;
using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Core;

public class Controller : IController
{
    private IRepository<IPlayer> players;
    private IRepository<ITeam> teams;

    // Създавам стринг масив от валидни позиции за играчи
    private string[] validPlayerTypes = { "Goalkeeper", "CenterBack", "ForwardWing" };

    public Controller()
    {
        players = new PlayerRepository();
        teams = new TeamRepository();
    }

    // Метод за добавяне на нов отбор
    public string NewTeam(string name)
    {
        Team team = new Team(name);

        // Ако името на отбора съществува
        if (teams.ExistsModel(name))
        {
            // Изписвам че името на отбора вече е добавено в {TeamRepository}
            return string.Format(OutputMessages.TeamAlreadyExists, name, "TeamRepository");
        }

        // Създавам отбора
        teams.AddModel(team);

        // Ако името на отбора НЕ съществува -> добавям името на отбора в {TeamRepository}
        return string.Format(OutputMessages.TeamSuccessfullyAdded, name, "TeamRepository");
    }

    // Метод за добавяне на нов играч
    public string NewPlayer(string typeName, string name)
    {
        // Проверявам дали валидните позиции за играчи НЕ отговарят на дадената позиция от конзолата
        if (!validPlayerTypes.Contains(typeName))
        {
            // Ако не отговаря -> връщам съобщение, че е опитано да се въведе невалидна позиция на играч
            return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
        }

        // Ако името на играча съществува
        if (players.ExistsModel(name))
        {
            // Права променлива за съществуващ играч
            IPlayer existingPlayer = players.GetModel(name);

            // Изписвам че името на играча вече е добавено в {PlayerRepository}, с дадена позиция
            return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, "PlayerRepository", existingPlayer.GetType().Name);
        }

        // Създавам променлива за нов играч с нулева стойност
        IPlayer newPlayer = null;

        // Създавам нов играч в дадена графа, според позицията която играе
        switch (typeName)
        {
            case "Goalkeeper":
                newPlayer = new Goalkeeper(name);
                break;
            case "CenterBack":
                newPlayer = new CenterBack(name);
                break;
            case "ForwardWing":
                newPlayer = new ForwardWing(name);
                break;
        }

        // Добавям новия играч
        players.AddModel(newPlayer);

        // Принтирам съобщение, че играча е добавен успещно
        return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
    }

    // Метод за създаване на нов договор между играч и отбор
    public string NewContract(string playerName, string teamName)
    {
        // Проверявам дали играча НЕ съществува
        if (!players.ExistsModel(playerName))
        {
            // Ако не съществува го изписвам
            return string.Format(OutputMessages.PlayerNotExisting, playerName, "PlayerRepository");
        }

        // Проверявам дали отбора НЕ съществува
        if (!teams.ExistsModel(teamName))
        {
            // Ако не съществува го изписвам
            return string.Format(OutputMessages.TeamNotExisting, teamName, "TeamRepository");
        }

        // Създавам играч
        IPlayer player = players.GetModel(playerName);
        // Създавам отбор
        ITeam team = teams.GetModel(teamName);


        // Проверявам дали играча няма отбор
        if (player.Team != null)
        {
            // Ако има изписвам, че играча вече има договор с дадения отбор
            return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
        }

        // Ако няма
        // Играча влиза в дадения отбор
        player.JoinTeam(team.Name);
        // Играча подписва договор
        team.SignContract(player);

        // Изписвам, че играча е подписал договор с дадения отбор
        return string.Format(OutputMessages.SignContract, playerName, teamName);
    }

    // Метод за симулация на игра
    public string NewGame(string firstTeamName, string secondTeamName)
    {
        // Взимам името на отборите
        ITeam firstTeam = teams.GetModel(firstTeamName);
        ITeam secondTeam = teams.GetModel(secondTeamName);

        // Създаваме печелещ отбор и губещ отбор
        ITeam winningTeam = null;
        ITeam loosingTeam = null;
        // Създавам променлива за равенство
        bool isDraw = false;

        // Проверявам кой отбор печели и дали има равенство
        if (teams.GetModel(firstTeamName).OverallRating > teams.GetModel(secondTeamName).OverallRating)
        {
            winningTeam = firstTeam;
            loosingTeam = secondTeam;
        }
        else if (teams.GetModel(firstTeamName).OverallRating > teams.GetModel(secondTeamName).OverallRating)
        {
            winningTeam = secondTeam;
            loosingTeam = firstTeam;
        }
        else
        {
            isDraw = true;
        }

        // Ако не е равенство влизам в проверката
        if (!isDraw)
        {
            // Спечелилия отбор взима 3 точки и рейтинга на играчите се повишава
            winningTeam.Win();
            // Губещия отбор не взима точки и рейтинга на играчите се спада
            loosingTeam.Lose();
            // Изписвам, че има победител в мача и 2та отбора (спечелилия и загубилия)
            return string.Format(OutputMessages.GameHasWinner, winningTeam.Name, loosingTeam.Name);
        }
        else // Ако е равенство
        {
            // Отборите получават по 1 точка и САМО рейтинга на вратарите се повишава
            firstTeam.Draw();
            secondTeam.Draw();
            // Изписвам, че играта е равенство и 2та отбора
            return string.Format(OutputMessages.GameIsDraw, firstTeam.Name, secondTeam.Name);
        }
    }

    // Метод който връща статистиките на отборите и техните играчи
    public string PlayerStatistics(string teamName)
    {
        ITeam team = teams.GetModel(teamName);

        StringBuilder sb = new StringBuilder();

        // Изписвам името на отбора
        sb.AppendLine($"***{teamName}***");

        // Създава списък, в който изписвам играчите подредени по рейтинг (низходящо) и по име (възходящо)
        List<IPlayer> sortedPlayers = team.Players
            .OrderByDescending(p => p.Rating)
            .ThenBy(p => p.Name)
            .ToList();

        // Обикалям списъка и добавям в {StringBuilder-а} подредените играчи
        foreach (var player in sortedPlayers)
        {
            sb.AppendLine(player.ToString());
        }

        // Принтирам списъка
        return sb.ToString().Trim();
    }

    // Метод за печатане на цялото {TeamRepository} и местата в лигата на отборите
    public string LeagueStandings()
    {
        StringBuilder sb = new StringBuilder();

        // Изписвам този ред
        sb.AppendLine("***League Standings***");

        // Създава списък, в който изписвам отборите подредени по спечелени точки (низходящо), по рейтинг(низходящо) и по име (възходящо)
        List<ITeam> sortedtTeams = teams.Models
            .OrderByDescending(t => t.PointsEarned)
            .ThenByDescending(t => t.OverallRating)
            .ThenBy(t => t.Name)
            .ToList();

        // Обикалям списъка и добавям в {StringBuilder-а} подредените отбори
        foreach (var team in sortedtTeams)
        {
            sb.AppendLine(team.ToString());
        }

        // Принтирам списъка
        return sb.ToString().Trim();
    }
}