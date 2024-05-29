using AccoSystem.DataLayer;

namespace AccoSystem.Services;

public interface ITransactionService
{
    public List<Accounting> Get(int typeId=0);


    public bool Add(int customerId, int amount, int typeId, string description);


    public bool Edit(int id, int customerId, int amount, int typeId, string description);

    public bool Delete(int id);

    public List<Accounting> Search(DateTime fromDate, DateTime toDate);

}