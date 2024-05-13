using AccoSystem.DataLayer.Repositories;
using AccoSystem.DataLayer.Services;

namespace AccoSystem.DataLayer.Context;

public class UnitOfWork:IDisposable
{
    private AccoSystemDbContext _context;

    public UnitOfWork(AccoSystemDbContext context)
    {
        _context = context;
    }

    private ICustomerRepository _customerRepository;

    public ICustomerRepository CustomerRepository
    {
        get
        {
            if (_customerRepository == null)
            {
                _customerRepository = new CustomerRepository(_context);
            }
            return _customerRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}