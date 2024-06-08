using AccoSystem.DataLayer;

namespace AccoSystem.Services;

public interface ITransactionService
{
    public List<Accounting> Get(TransactionType type = default);


    public bool Add(int customerId, int amount, TransactionType typeId, string description);


    public bool Edit(int id, int customerId, int amount, TransactionType typeId, string description);

    public bool Delete(int id);

    public List<Accounting> Search(DateTime fromDate, DateTime toDate);
}