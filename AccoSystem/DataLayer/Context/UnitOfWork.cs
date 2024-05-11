using AccoSystem.DataLayer.Repositories;
using AccoSystem.DataLayer.Services;

namespace AccoSystem.DataLayer.Context;

public class UnitOfWork:IDisposable
{
    private AccoSystemDbContext context = new AccoSystemDbContext();

    private ICustomerRepository _customerRepository;

    public ICustomerRepository CustomerRepository
    {
        get
        {
            if (_customerRepository == null)
            {
                _customerRepository = new CustomerRepository(context);
            }
            return _customerRepository;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }
}