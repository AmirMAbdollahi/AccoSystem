using AccoSystem.DataLayer;
using AccoSystem.Services;

namespace AccoSystem.Commands;

public class CustomerCommand : Command
{
    private ICustomerService _customerService;

    public CustomerCommand(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override void Get()
    {
        var customers = _customerService.Get();
        Print(customers);
    }

    public override void Add()
    {
        var addDictionary = GetPropertyValueDictionary<Customer>(
            a => a.CustomerId,
            a => a.Accountings);
        var fullName = addDictionary["FullName"];
        var mobile = addDictionary["Mobile"];
        var addrese = addDictionary["Addrese"];
        var email = addDictionary["Email"];
        var customer = _customerService.Add(fullName, mobile, addrese, email);
        ShowResultMessage(customer);
    }

    public override void Edit()
    {
        var id = GetId();
        var editDictionary = GetPropertyValueDictionary<Customer>(
            a => a.CustomerId,
            a => a.Accountings);
        var fullName = editDictionary["FullName"];
        var mobile = editDictionary["Mobile"];
        var addrese = editDictionary["Addrese"];
        var email = editDictionary["Email"];
        var customer = _customerService.Edit(id, fullName, mobile, addrese, email);
        ShowResultMessage(customer);
    }

    public override void Delete()
    {
        var id = GetId();
        var customer = _customerService.Delete(id);
        ShowResultMessage(customer);
    }

    public override void Search()
    {
        Console.WriteLine("Do you want to search customer? Please enter its Name");
        var query = Console.ReadLine();
        var customers = _customerService.Get(query);
        Print(customers);
    }

    private static void Print(List<Customer> customers)
    {
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.FullName +
                              " - " +
                              customer.Mobile +
                              " - " +
                              customer.Addrese +
                              " - " +
                              customer.Email);
        }
    }

    private static void PrintById(List<Customer> customers)
    {
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.CustomerId +
                              " - "
                              + customer.FullName +
                              " - " +
                              customer.Mobile +
                              " - " +
                              customer.Addrese +
                              " - " +
                              customer.Email);
        }
    }

    private int GetId()
    {
        var customers = _customerService.Get(null);
        PrintById(customers);
        Console.WriteLine("Which customer do you want ? Please enter its ID");
        var id = Convert.ToInt32(Console.ReadLine());
        return id;
    }

    private static void ShowResultMessage(bool isSuccessful)
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