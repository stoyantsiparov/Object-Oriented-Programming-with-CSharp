using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;

namespace NauticalCatchChallenge.Repositories;

public class FishRepository : IRepository<IFish>
{
    private List<IFish> fish;

    public FishRepository()
    {
        fish = new List<IFish>();
    }

    public IReadOnlyCollection<IFish> Models => fish.AsReadOnly();
    public void AddModel(IFish model) => fish.Add(model);

    public IFish GetModel(string name) => fish.FirstOrDefault(f => f.Name == name);
}