using AccoSystem.DataLayer;
using AccoSystem.Services;

namespace AccoSystem.Commands;

public class TransactionCommand : Command
{
    private ITransactionService _transactionService;

    public TransactionCommand(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public void Get(int typeId = 0)
    {
        var accounting = _transactionService.Get(typeId);
        Print(accounting);
    }

    public void Add(int customerId, int amount, int typeId, string description)
    {
        var accounting = _transactionService.Add(customerId, amount, typeId, description);
        Result(accounting);
    }

    public void Edit(int id, int customerId, int amount, int typeId, string description)
    {
        var accounting = _transactionService.Edit(id, customerId, amount, typeId, description);
        Result(accounting);
    }

    public void Delete(int id)
    {
        var accounting = _transactionService.Delete(id);
        Result(accounting);
    }

    public void Search(DateTime fromDate, DateTime toDate)
    {
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
                              accounting.Type.TypeTitle +
                              " - " +
                              accounting.Description);
        }
    }

    private void Result(bool isSuccessful)
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