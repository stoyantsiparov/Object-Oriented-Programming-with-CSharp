using MilitaryElite.Enums;

namespace MilitaryElite.Models.Interfaces;

public interface IMission
{
    public string CodeName { get; }
    State State { get; }
    void CompleteMission();
}