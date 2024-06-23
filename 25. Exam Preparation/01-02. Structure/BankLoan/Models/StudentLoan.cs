namespace BankLoan.Models;

public class StudentLoan : Loan
{
    public StudentLoan() 
        : base(1, 10_000)
    {
    }
}