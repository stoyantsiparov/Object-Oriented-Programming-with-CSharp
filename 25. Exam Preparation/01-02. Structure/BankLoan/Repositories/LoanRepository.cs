using System.Collections.Generic;
using System.Linq;
using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;

namespace BankLoan.Repositories;

public class LoanRepository : IRepository<ILoan>
{
    private List<ILoan> loans;

    public LoanRepository()
    {
        loans = new List<ILoan>();
    }

    public IReadOnlyCollection<ILoan> Models => loans.AsReadOnly();

    public void AddModel(ILoan model) => loans.Add(model);

    public bool RemoveModel(ILoan model) => loans.Remove(model);

    public ILoan FirstModel(string name) => loans.FirstOrDefault(l => l.GetType().Name == name);
}