using AccoSystem.DataLayer;

namespace AccoSystem.Services;

public interface ICustomerService
{
    public List<Customer> Get(string? query = null);
    public bool Add(string fullName, string mobile, string addrese, string email);

    public bool Edit(int id, string fullName, string mobile, string addrese, string email);

    public bool Delete(int id);

    //public List<Customer> Search(string query);
}