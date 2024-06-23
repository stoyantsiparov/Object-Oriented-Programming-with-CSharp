using MilitaryElite.Enums;

namespace MilitaryElite.Models.Interfaces;

public interface ISpecialisedSoldier : IPrivate
{
    public Corps Corps { get; }
}