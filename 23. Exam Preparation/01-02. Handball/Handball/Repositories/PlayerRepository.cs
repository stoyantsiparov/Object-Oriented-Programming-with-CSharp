using System.Collections.Generic;
using System.Linq;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories;

public class PlayerRepository : IRepository<IPlayer>
{
    private List<IPlayer> players;

    public PlayerRepository()
    {
        players = new List<IPlayer>();
    }

    // Създавам колекция от играчи, която може само да се чете
    public IReadOnlyCollection<IPlayer> Models => players.AsReadOnly();

    // Добавям играч в тази колекция
    public void AddModel(IPlayer model) => players.Add(model);

    public bool RemoveModel(string name)
    {
        // Взимам името на играча от колекцията
        IPlayer player = GetModel(name);

        // Ако има играч с това име го премахвам
        return players.Remove(player);
    }

    // Проверявам дали е добавен играч с дадено име в това репозитори
    public bool ExistsModel(string name) => players.Any(p => p.Name == name);

    // Връща ми името на даден играч от колекцията, ако съществува (ако не връща {"null"})
    public IPlayer GetModel(string name) => players.FirstOrDefault(p => p.Name == name);
}