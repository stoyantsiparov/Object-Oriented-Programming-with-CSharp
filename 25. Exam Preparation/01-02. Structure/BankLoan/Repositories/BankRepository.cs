using System.Collections.Generic;
using System.Linq;
using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;

namespace BankLoan.Repositories;

public class BankRepository : IRepository<IBank>
{
    private List<IBank> banks;

    public BankRepository()
    {
        banks = new List<IBank>();
    }

    public IReadOnlyCollection<IBank> Models => banks.AsReadOnly();

    public void AddModel(IBank model) => banks.Add(model);

    public bool RemoveModel(IBank model) => banks.Remove(model);

    public IBank FirstModel(string name) => banks.FirstOrDefault(b => b.Name == name);
}