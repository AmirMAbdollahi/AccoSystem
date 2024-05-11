namespace AccoSystem.DataLayer.Repositories;

public interface ICustomerRepository
{
    List<Customer> GetAllCustomers();

    Customer GetCustomerById(int customerId);

    bool InsertCustomer(Customer customer);

    bool UpdateCustomer(Customer customer);

    bool DeleteCustomer(Customer customer);

    bool DeleteCustomerById(int customerId);

    void Save();
}