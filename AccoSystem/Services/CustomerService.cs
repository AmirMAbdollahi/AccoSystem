using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using Microsoft.IdentityModel.Tokens;

namespace AccoSystem.Services;

public class CustomerService : ICustomerService
{
    public List<Customer> Get(string? query = null)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        if (query.IsNullOrEmpty())
        {
            return unit.CustomerRepository.Get().ToList();
        }

        return unit.CustomerRepository.Get(a => a.FullName == query).ToList();
    }

    public bool Add(string fullName, string mobile, string addrese, string email)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.Insert(new Customer()
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
        var isSuccessful = unit.CustomerRepository.Update(new Customer()
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
        var isSuccessful = unit.CustomerRepository.Delete(id);
        unit.Save();

        return isSuccessful;
    }

    // public List<Customer> Search(string query)
    // {
    //     using var unit = new UnitOfWork(new AccoSystemDbContext());
    //     return unit.CustomerRepository.Get(query).ToList();
    // }
}