using System.Text;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Models;

public class LieutenantGeneral : Private, ILieutenantGeneral
{
    public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, IReadOnlyCollection<IPrivate> privates)
        : base(id, firstName, lastName, salary)
    {
        Privates = privates;
    }

    public IReadOnlyCollection<IPrivate> Privates { get; private set; }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine(base.ToString());
        sb.AppendLine("Privates:");

        foreach (var currentPrivate in Privates)
        {
            sb.AppendLine($"  {currentPrivate.ToString()}");
        }

        return sb.ToString().TrimEnd();
    }
}