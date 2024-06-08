using AccoSystem.DataLayer;
using AccoSystem.Services;
using AccoSystem.Utility;

namespace AccoSystem.Commands;

public class TransactionCommand : Command
{
    private ITransactionService _transactionService;

    public TransactionCommand(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public override void Get()
    {
        var accounting = _transactionService.Get();
        Print(accounting);
    }

    public override void Add()
    {
        var customerId = GetOneCustomerFromIds();
        var addDictionary = GetPropertyValueDictionary<Accounting>(
            a => a.Id,
            a => a.CustomerId,
            a => a.DateTime,
            a => a.Customer,
            a => a.TransactionType);
        var amount = Convert.ToInt32(addDictionary["Amount"]);
        var description = addDictionary["Description"];
        var type = GetType();

        var accounting = _transactionService.Add(customerId, amount, type, description);
        Result(accounting);
    }

    public override void Edit()
    {
        var id = GetId();
        var editDictionary = GetPropertyValueDictionary<Accounting>(
            a => a.Id,
            a => a.CustomerId,
            a => a.DateTime,
            a => a.Customer,
            a => a.TransactionType);
        var amount = Convert.ToInt32(editDictionary["Amount"]);
        var description = editDictionary["Description"];
        var type = GetType();
        var customerId = GetCustomer(id);
        var accounting = _transactionService.Edit(id, customerId, amount, type, description);
        Result(accounting);
    }

    public override void Delete()
    {
        var id = GetId();
        var accounting = _transactionService.Delete(id);
        Result(accounting);
    }

    public override void Search()
    {
        Console.WriteLine("From what date do you want to filter transactions? ");
        var startDate = Console.ReadLine();
        Console.WriteLine("Until what date do you want to filter transactions? ");
        var endDate = Console.ReadLine();
        DateTime fromDate = DateConvertor.ToMiladi(Convert.ToDateTime(startDate));
        DateTime toDate = DateConvertor.ToMiladi(Convert.ToDateTime(endDate));
        var accounting = _transactionService.Search(fromDate, toDate);
        Print(accounting);
    }

    private void Print(List<Accounting> accountings)
    {
        foreach (var accounting in accountings)
        {
            Console.WriteLine(accounting.Customer.FullName +
                              " - " +
                              accounting.Amount +
                              " - " +
                              accounting.TransactionType +
                              " - " +
                              accounting.Description);
        }
    }

    private static void PrintById(List<Accounting> accountings)
    {
        foreach (var accounting in accountings)
        {
            Console.WriteLine(accounting.Id +
                              " - "
                              + accounting.Customer.FullName +
                              " - " +
                              accounting.Amount +
                              " - " +
                              accounting.TransactionType +
                              " - " +
                              accounting.Description);
        }
    }

    private int GetId(TransactionType type = default)
    {
        var tranactions = _transactionService.Get(type);
        PrintById(tranactions);
        Console.WriteLine("Which transaction do you want ? Please enter its ID");
        var id = Convert.ToInt32(Console.ReadLine());
        return id;
    }

    private int GetOneCustomerFromIds(TransactionType type = default)
    {
        Console.WriteLine("Which customer do you want ? Please enter its ID");
        var accountings = _transactionService.Get(type);
        var groupedAccountings = accountings
            .GroupBy(a => a.CustomerId)
            .ToList();
        foreach (var group in groupedAccountings)
        {
            var representativeAccounting = group.First(); // Take the first accounting entry from the group
            Console.WriteLine($"{representativeAccounting.CustomerId} - {representativeAccounting.Customer.FullName}");
        }

        var id = Convert.ToInt32(Console.ReadLine());

        return id;
    }

    private int GetCustomer(int id)
    {
        List<Accounting> accountings = _transactionService.Get();
        foreach (var accounting in accountings)
        {
            if (accounting.Id == id)
            {
                return accounting.Customer.CustomerId;
            }
        }

        return -1;
    }

    private TransactionType GetType()
    {
        Console.WriteLine("1 => income");
        Console.WriteLine("2 => const");
        var id = Convert.ToInt32(Console.ReadLine());
        return Enum.Parse<TransactionType>(id.ToString());
    }

    private static void Result(bool isSuccessful)
    {
        if (isSuccessful)
        {
            Console.WriteLine("Congratulations. done successfully");
        }
        else
        {
            Console.WriteLine("I am sorry. You have failed");
        }
    }
}