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
        return context.Customers.Find(customerId);
    }

    public bool InsertCustomer(Customer customer)
    {
        try
        {
            context.Customers.Add(customer);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateCustomer(Customer customer)
    {
        try
        {
            context.Entry(customer).State = EntityState.Modified;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteCustomer(Customer customer)
    {
        try
        {
            context.Entry(customer).State = EntityState.Deleted;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteCustomerById(int customerId)
    {
        try
        {
            DeleteCustomer(GetCustomerById(customerId));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Save()
    {
        context.SaveChanges();
    }
}