using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystem.Utility;

namespace AccoSystem.Services;

public class TransactionService : ITransactionService
{
    public List<Accounting> Get(int typeId = 0)
    {
        using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
        {
            if (typeId == 1 || typeId == 2)
            {
                return unit.AccountingRepository.Get(a => a.TypeId == typeId).ToList();
            }

            return unit.AccountingRepository.Get().ToList();
        }
    }

    public bool Add(int customerId, int amount, int typeId, string description)
    {
        bool isSuccessful = false;
        try
        {
            using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
            {
                isSuccessful = unit.AccountingRepository.Insert(new Accounting()
                {
                    CustomerId = customerId,
                    Amount = amount,
                    TypeId = typeId,
                    DateTime = DateTime.Now,
                    Description = description,
                });
                unit.Save();
            }

            return isSuccessful;
        }
        catch
        {
            return isSuccessful;
        }
    }

    public bool Edit(int id, int customerId, int amount, int typeId, string description)
    {
        bool isSuccessful = false;
        try
        {
            using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
            {
                isSuccessful = unit.AccountingRepository.Update(new Accounting()
                {
                    Id = id,
                    DateTime = DateTime.Now,
                    Amount = amount,
                    Description = description,
                    CustomerId = customerId,
                    TypeId = typeId
                });
                unit.Save();
            }

            return isSuccessful;
        }
        catch
        {
            return isSuccessful;
        }
    }

    public bool Delete(int id)
    {
        bool isSuccessful = false;
        try
        {
            using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
            {
                isSuccessful = unit.AccountingRepository.Delete(id);
                unit.Save();
            }

            return isSuccessful;
        }
        catch
        {
            return isSuccessful;
        }
    }

    public List<Accounting> Search(DateTime fromDate, DateTime toDate)
    {
        List<Accounting> result = new List<Accounting>();
        result = Get();
        result = result.Where(r => r.DateTime >= fromDate && r.DateTime <= toDate).ToList();

        return result;
    }
    
}