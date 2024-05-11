using AccoSystem.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccoSystem.DataLayer.Services;

public class CustomerRepository:ICustomerRepository
{
    private MyDbContext context;
    public CustomerRepository(MyDbContext db)
    {
        this.context = db;
    }
    public List<Customer> GetAllCustomers()
    {
        return context.Customers.ToList();
    }

    public Customer GetCustomerById(int customerId)
    {
        throw new NotImplementedException();
    }

    public bool InsertCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public bool UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public bool DeleteCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public bool DeleteCustomerById(int customerId)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}