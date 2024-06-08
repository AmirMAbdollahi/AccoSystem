using System.Linq.Expressions;
using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;

namespace AccoSystem.Services;

public class TransactionService : ITransactionService
{
    public List<Accounting> Get(TransactionType type = default)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        return type is TransactionType.Cost or TransactionType.Income
            ? unit.AccountingRepository.Get(a => a.TransactionType == type).ToList()
            : unit.AccountingRepository.Get(null, new List<Expression<Func<Accounting, object>>>()
            {
                x => x.Customer
            }).ToList();
    }

    public bool Add(int customerId, int amount, TransactionType type, string description)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.AccountingRepository.Insert(new Accounting()
        {
            CustomerId = customerId,
            Amount = amount,
            TransactionType = type,
            DateTime = DateTime.Now,
            Description = description,
        });
        unit.Save();

        return isSuccessful;
    }

    public bool Edit(int id, int customerId, int amount, TransactionType type, string description)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.AccountingRepository.Update(new Accounting()
        {
            Id = id,
            DateTime = DateTime.Now,
            Amount = amount,
            Description = description,
            CustomerId = customerId,
            TransactionType = type
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