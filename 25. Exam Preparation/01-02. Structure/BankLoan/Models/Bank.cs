using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Models;

public abstract class Bank : IBank
{
    private string name;
    private List<ILoan> loans;
    private List<IClient> clients;


    protected Bank(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
        loans = new List<ILoan>();
        clients = new List<IClient>();
    }

    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Bank name cannot be null or empty.");
                throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
            }

            name = value;
        }
    }
    public int Capacity { get; private set; }
    public IReadOnlyCollection<ILoan> Loans => loans;
    public IReadOnlyCollection<IClient> Clients => clients;
    public double SumRates()
    {
        if (Loans.Count == 0)
        {
            return 0;
        }

        return double.Parse(Loans.Select(l => l.InterestRate).Sum().ToString());
    }

    public void AddClient(IClient client)
    {
        if (Clients.Count > Capacity)
        {
            //throw new ArgumentException("Not enough capacity for this client.");
            throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
        }

        clients.Add(client);
    }

    public void RemoveClient(IClient client) => clients.Remove(client);

    public void AddLoan(ILoan loan) => loans.Add(loan);

    public string GetStatistics()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
        sb.Append("Clients: ");

        // Ако броя на клиентите е 0 изписвам, че няма {sb.AppendLine("none")}, а ако има клиенти ги изписвам един след друг разделени със запетая
        if (clients.Count == 0)
        {
            sb.AppendLine("none");
        }
        else
        {
            string[] names = clients.Select(c => c.Name).ToArray();

            foreach (var client in clients)
            {
                sb.AppendLine(string.Join(", ", names));
            }
        }

        sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");

        return sb.ToString().TrimEnd();
    }
}