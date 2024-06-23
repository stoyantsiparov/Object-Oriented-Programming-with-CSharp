using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Handball.Repositories;

public class TeamRepository : IRepository<ITeam>
{
    private List<ITeam> teams;

    public TeamRepository()
    {
        teams = new List<ITeam>();
    }

    // Създавам колекция от отбори, която може само да се чете
    public IReadOnlyCollection<ITeam> Models => teams.AsReadOnly();

    // Добавям отбор в тази колекция
    public void AddModel(ITeam model) => teams.Add(model);

    public bool RemoveModel(string name)
    {
        // Взимам името на отбора от колекцията
        ITeam team = GetModel(name);

        // Ако има отбор с това име го премахвам
        return teams.Remove(team);
    }

    // Проверявам дали е добавен отбор с дадено име в това репозитори
    public bool ExistsModel(string name) => teams.Any(p => p.Name == name);

    // Връща ми името на даден отбор от колекцията, ако съществува (ако не връща {"null"})
    public ITeam GetModel(string name) => teams.FirstOrDefault(p => p.Name == name);
}