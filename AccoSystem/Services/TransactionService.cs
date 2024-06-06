using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace AccoSystem.Services;

public class TransactionService : ITransactionService
{
    public List<Accounting> Get(int typeId = 0)
    {
        var context = new AccoSystemDbContext();
        using var unit = new UnitOfWork(context);
        var accountings = context.Accountings.Include(a => a.Customer)
            .Include(a => a.Type)
            .ToList();
        return typeId is 1 or 2
            ? accountings.Where(a => a.TypeId == typeId).ToList()
            : accountings;
    }

    public bool Add(int customerId, int amount, int typeId, string description)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.AccountingRepository.Insert(new Accounting()
        {
            CustomerId = customerId,
            Amount = amount,
            TypeId = typeId,
            DateTime = DateTime.Now,
            Description = description,
        });
        unit.Save();

        return isSuccessful;
    }

    public bool Edit(int id, int customerId, int amount, int typeId, string description)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.AccountingRepository.Update(new Accounting()
        {
            Id = id,
            DateTime = DateTime.Now,
            Amount = amount,
            Description = description,
            CustomerId = customerId,
            TypeId = typeId
        });
        unit.Save();

        return isSuccessful;
    }

    public bool Delete(int id)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.AccountingRepository.Delete(id);
        unit.Save();
        
        return isSuccessful;
    }

    public List<Accounting> Search(DateTime fromDate, DateTime toDate)
    {
        var result = Get();
        result = result.Where(r => r.DateTime >= fromDate && r.DateTime <= toDate).ToList();

        return result;
    }
}