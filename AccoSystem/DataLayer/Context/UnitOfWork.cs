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

    private AllRepo<Accounting> _accountingRepository;

    public AllRepo<Accounting> AccountingRepository
    {
        get {
            if (_accountingRepository==null)
            {
                _accountingRepository = new AllRepo<Accounting>(_context);
            }

            return _accountingRepository;
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