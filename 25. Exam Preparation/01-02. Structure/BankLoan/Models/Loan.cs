using BankLoan.Models.Contracts;

namespace BankLoan.Models;

public abstract class Loan : ILoan
{
    protected Loan(int interestRate, double amount)
    {
        InterestRate = interestRate;
        Amount = amount;
    }

    public int InterestRate { get; private set; }
    public double Amount { get; private set; }
}