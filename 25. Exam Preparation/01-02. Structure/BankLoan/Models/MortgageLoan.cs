using BankLoan.Models.Contracts;

namespace BankLoan.Models;

public class MortgageLoan : Loan
{
    public MortgageLoan()
        : base(3, 50_000)
    {
    }
}
