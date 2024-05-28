using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystem.Services;

namespace AccoSystem.Commands;

public class CustomerCommand : Command
{
    private ICustomerService _customerService;

    public CustomerCommand(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public void Get()
    {
        var customers = _customerService.Get();
        Print(customers);
    }

    public void Add(string fullName, string mobile, string addrese, string email)
    {
        var customer = _customerService.Add(fullName, mobile, addrese, email);
        Result(customer);
    }

    public void Edit(int id, string fullName, string mobile, string addrese, string email)
    {
        var customer = _customerService.Edit(id, fullName, mobile, addrese, email);
        Result(customer);
    }

    public void Delete(int id)
    {
        var customer = _customerService.Delete(id);
        Result(customer);
    }

    public void Search(string query)
    {
        var customers = _customerService.Search(query);
        Print(customers);
    }

    private void Print(List<Customer> customers)
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