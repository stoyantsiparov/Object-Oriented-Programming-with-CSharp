using System;
using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Models;

public abstract class Client : IClient
{
    protected Client(string name, string id, int interest, double income)
    {
        Name = name;
        Id = id;
        Interest = interest;
        Income = income;
    }

    private string name;
    private string id;
    private double income;


    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Client name cannot be null or empty.");
                throw new ArgumentException(ExceptionMessages.ClientNameNullOrWhitespace);
            }

            name = value;
        }
    }
    public string Id
    {
        get => id;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Client’s ID cannot be null or empty.");
                throw new ArgumentException(ExceptionMessages.ClientIdNullOrWhitespace);
            }

            id = value;
        }
    }

    public int Interest { get; protected set; }

    public double Income
    {
        get => income;
        private set
        {
            if (value <= 0)
            {
                //throw new ArgumentException("Income cannot be below or equal to 0.");
                throw new ArgumentException(ExceptionMessages.ClientIncomeBelowZero);
            }

            income = value;
        }
    }

    public abstract void IncreaseInterest();
}