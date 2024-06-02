using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;

namespace AccoSystem.Services;

public class CustomerService : ICustomerService
{
    public List<Customer> Get()
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        return unit.CustomerRepository.GetAllCustomers();
    }

    public bool Add(string fullName, string mobile, string addrese, string email)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.InsertCustomer(new Customer()
        {
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        unit.Save();
        
        return isSuccessful;
    }

    public bool Edit(int id, string fullName, string mobile, string addrese, string email)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.UpdateCustomer(new Customer()
        {
            CustomerId = id,
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        unit.Save();

        return isSuccessful;
    }

    public bool Delete(int id)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.DeleteCustomerById(id);
        unit.Save();

        return isSuccessful;
    }

    public List<Customer> Search(string query)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        return unit.CustomerRepository.GetCustomerByFilter(query).ToList();
    }
}