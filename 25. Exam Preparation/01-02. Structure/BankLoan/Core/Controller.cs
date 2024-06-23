using System;
using System.Linq;
using System.Text;
using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;

namespace BankLoan.Core;

public class Controller : IController
{
    private LoanRepository loans;
    private BankRepository banks;

    public Controller()
    {
        loans = new LoanRepository();
        banks = new BankRepository();
    }

    public string AddBank(string bankTypeName, string name)
    {
        IBank bank;

        if (bankTypeName == "BranchBank")           // if (bankTypeName == nameof(BranchBank))
        {
            bank = new BranchBank(name);
        }
        else if (bankTypeName == "CentralBank")     // else if (bankTypeName == nameof(CentralBank))
        {
            bank = new CentralBank(name);
        }
        else
        {
            // throw new ArgumentException("Invalid bank type.");
            throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
        }

        banks.AddModel(bank);

        //return $"{bankTypeName} is successfully added.";
        return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
    }

    public string AddLoan(string loanTypeName)
    {
        ILoan loan;

        if (loanTypeName == "MortgageLoan")         // if (loanTypeName == nameof(MortgageLoan))
        {
            loan = new MortgageLoan();
        }
        else if (loanTypeName == "StudentLoan")     // else if (loanTypeName == nameof(StudentLoan))
        {
            loan = new StudentLoan();
        }
        else
        {
            // throw new ArgumentException("Invalid loan type.");
            throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
        }

        loans.AddModel(loan);

        //return $"{loanTypeName} is successfully added.";
        return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
    }

    public string ReturnLoan(string bankName, string loanTypeName)
    {
        var loan = loans.FirstModel(loanTypeName);
        var bank = banks.FirstModel(bankName);

        if (loan == null)
        {
            // throw new ArgumentException("Loan of type {loanTypeName} is missing.");
            throw new ArgumentException(string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName));
        }

        bank.AddLoan(loan);
        loans.RemoveModel(loan);

        // return $"{loanTypeName} successfully added to {bankName}.";
        return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
    }

    public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
    {
        IClient client;

        if (clientTypeName == "Student")            // if (clientTypeName == nameof(Student))
        {
            client = new Student(clientName, id, income);
        }
        else if (clientTypeName == "Adult")         // else if (clientTypeName == nameof(Adult))
        {
            client = new Adult(clientName, id, income);
        }
        else
        {
            // throw new ArgumentException("Invalid client type.");
            throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
        }

        var bank = banks.FirstModel(bankName);

        if (bank.GetType().Name == "BranchBank" && clientTypeName != "Student" ||
            bank.GetType().Name == "CentralBank" && clientTypeName != "Adult")
        {
            //return "Unsuitable bank.";
            return string.Format(OutputMessages.UnsuitableBank);
        }

        bank.AddClient(client);

        // return $"{clientTypeName} successfully added to {bankName}.";
        return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
    }

    public string FinalCalculation(string bankName)
    {
        var bank = banks.Models.FirstOrDefault(b => b.Name == bankName);

        var totalIncome = bank.Clients.Sum(c => c.Income);
        var totalLoanAmount = bank.Loans.Sum(l => l.Amount);
        string totalFunds = (totalLoanAmount + totalIncome).ToString("F2");

        //return $"The funds of bank {bankName} are {totalFunds:F2}.";
        return string.Format(OutputMessages.BankFundsCalculated, bankName, totalFunds);

    }

    public string Statistics()
    {
        StringBuilder sb = new StringBuilder();

        var banksStatistics = banks.Models;

        foreach (var bank in banksStatistics)
        {
            sb.AppendLine(bank.GetStatistics());
        }

        return sb.ToString().TrimEnd();
    }
}