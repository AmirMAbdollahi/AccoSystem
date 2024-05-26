using System.Collections.Frozen;
using System.ComponentModel;
using System.Threading.Channels;
using AccoSystem.Commands;
using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystem.DataLayer.Services;
using AccoSystem.Utility;
using AccoSystem.ViewModels;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;

var welcome = new WelcomeCommand();
welcome.Execute();

var main = new MainCommand();
main.Execute();

var continueCom = new ContinueCommand();
continueCom.Execute();

var continueNum = Convert.ToInt32(Console.ReadLine());

switch (continueNum)
{
    case 1:
        Console.WriteLine("Your customers are :");
        GetCustomerList();
        var cusList = new CustomerListCommand();
        cusList.Execute();
        var num = Convert.ToInt32(Console.ReadLine());
        switch (num)
        {
            case 1:
                NewCustomer();
                break;
            case 2:
                EditCustomer();
                break;
            case 3:
                DeleteCustomer();
                break;
            case 4:
                SearchCustomer();
                break;
            default:
                break;
        }

        break;
    case 2:
        Console.WriteLine("Your transactions are :");
        GetTransaction();
        var trnList = new TransactionsListCommand();
        trnList.Execute();
        var number = Convert.ToInt32(Console.ReadLine());
        switch (number)
        {
            case 1:
                Transactions();
                break;
            case 2:
                EditTransaction();
                break;
            case 3:
                DeleteTransaction();
                break;
            case 4:
                SearchTransaction();
                break;
            default:
                break;
        }

        break;
    case 3:
        InComeReport();
        break;
    case 4:
        CostReport();
        break;
    default:
        var wrongNum = new WrongNumberCommand();
        wrongNum.Execute();
        break;
}

void EditTransaction()
{
    var editTran = new EditTransactionCommand();
    editTran.Execute();
    editTran.TransactionIndex();
    var index = FindTransaction();
    var transactionDictionary= editTran.GetPropertyValueDictionary<Accounting>(
        a => a.CustomerId,
        a => a.TypeId,
        a => a.DateTime,
        a => a.Customer,
        a => a.Id,
        a => a.Type);
    int typeIndex = 0;
    int CustomerId = 0;

    /// two using
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var accountings = unit.AccountingRepository.Get().ToList();
        foreach (var accounting in accountings)
        {
            if (accounting.Id == index)
            {
                typeIndex = accounting.TypeId;
                CustomerId = accounting.CustomerId;
            }
        }
    }

    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        unit.AccountingRepository.Update(new Accounting()
        {
            Id = index,
            DateTime = DateTime.Now,
            Amount = Convert.ToInt32(transactionDictionary["Amount"]),
            Description = transactionDictionary["Description"],
            CustomerId = CustomerId,
            TypeId = typeIndex
        });
        unit.Save();
        editTran.Finish();
    }
}

int FindTransaction()
{
    DisplayTransactionWithId();
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var transactions = unit.AccountingRepository.Get().ToList();
        int index = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < transactions.Count; i++)
        {
            if (index == transactions[i].Id)
            {
                return index;
            }
        }
    }

    return -1;
}

void DeleteTransaction()
{
    var deleteTran = new DeleteTransaction();
    deleteTran.Execute();
    DisplayTransactionWithId();
    deleteTran.TransactionIndex();
    int index = Convert.ToInt32(Console.ReadLine());
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        unit.AccountingRepository.Delete(index);
        unit.Save();
    }

    deleteTran.Finish();
}

void DisplayTransactionWithId()
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var transactions = unit.AccountingRepository.Get().ToList();
        foreach (var transaction in transactions)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(transaction.CustomerId);
            Console.WriteLine(transaction.Id + " - " + name + " - " + transaction.Amount + " - " +
                              transaction.DateTime.ToShamsi() + " - " + transaction.Description);
        }
    }
}

void SearchTransaction()
{
    var searchTran = new SearchTransaction();
    searchTran.Execute();
    searchTran.FromDate();
    var startDate = Console.ReadLine();
    searchTran.ToDate();
    var endDate = Console.ReadLine();
    DateTime? fromDate = DateConvertor.ToMiladi(Convert.ToDateTime(startDate));
    DateTime? toDate = DateConvertor.ToMiladi(Convert.ToDateTime(endDate));
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        List<Accounting> result = new List<Accounting>();
        result = unit.AccountingRepository.Get().ToList();
        result = result.Where(r => r.DateTime >= fromDate && r.DateTime <= toDate).ToList();
        DisplaySearchedTransaction(result);
    }
}

void DisplaySearchedTransaction(List<Accounting> results)
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        foreach (var result in results)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(result.CustomerId);
            Console.WriteLine(result.Id + " - " + name + " - " + result.Amount + " - " +
                              result.DateTime.ToShamsi() + " - " + result.Description);
        }
    }
}

void InComeReport()
{
    GetReport(1);
}

void CostReport()
{
    GetReport(2);
}

void GetReport(int type)
{
    var report = new ReportCommand();
    report.Execute();
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var accountings = unit.AccountingRepository.Get(a => a.TypeId == type).ToList();
        foreach (var accounting in accountings)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(accounting.CustomerId);
            var description = accounting.Description;
            if (accounting.Description.IsNullOrEmpty())
            {
                description = "There is no explanation.";
            }

            Console.WriteLine("Name is : " + name + " - Amount is : " + accounting.Amount + " - Date is : " +
                              accounting.DateTime.ToShamsi() + " - Description is : " + description);
        }
    }
}

void GetTransaction()
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var transactions = unit.AccountingRepository.Get().ToList();
        foreach (var transaction in transactions)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(transaction.CustomerId);
            Console.WriteLine(name + " - " + transaction.Amount + " - " +
                              transaction.DateTime.ToShamsi() + " - " + transaction.Description);
        }
    }
}

void Transactions()
{
    var transaction = new NewTransaction();
    transaction.Execute();
    var customerSelected = GetCustomerId();

    transaction.NewTypeId();
    var typeId = Convert.ToInt32(Console.ReadLine());
    
    var transactionDictionary = transaction.GetPropertyValueDictionary<Accounting>(
        a=>a.Id,
        a => a.CustomerId,
        a => a.DateTime,
        a=>a.Customer,
        a=>a.Type,
        a=>a.TypeId);

    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        unit.AccountingRepository.Insert(new Accounting()
        {
            CustomerId = customerSelected,
            Amount =Convert.ToInt32(transactionDictionary["Amount"]),
            TypeId =typeId,
            DateTime = DateTime.Now,
            Description = transactionDictionary["Description"],
        });

        if (customerSelected == -1 || typeId != 1 && typeId != 2)
        {
            Console.WriteLine("You doesn't select a customer and Correct typing. your transaction was not registered.");
        }
        else
        {
            unit.Save();
            transaction.Finish();
        }
    }
}

int GetCustomerId()
{
    GetCustomersName();
    Console.WriteLine("Please select a customer id for new transaction... ");
    var customersId = GetCustomersId();
    int customerSelected = Convert.ToInt32(Console.ReadLine());
    for (int i = 0; i < customersId.Count; i++)
    {
        if (customerSelected == customersId[i])
        {
            return customersId[i];
        }
    }

    return -1;
}

void GetCustomerList()
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var customers = unit.CustomerRepository.GetAllCustomers();
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.FullName + " - " + customer.Mobile + " - " + customer.Addrese + " - " +
                              customer.Email);
        }
    }
}

void NewCustomer()
{
    var newCus = new NewCustomerCommand();
    newCus.Execute();

    var customerDictionary = newCus.GetPropertyValueDictionary<Customer>(
        a => a.CustomerId,
        a => a.Accountings);

    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var customer = unit.CustomerRepository.InsertCustomer(new Customer()
        {
            FullName = customerDictionary["FullName"],
            Mobile = customerDictionary["Mobile"],
            Addrese = customerDictionary["Addrese"],
            Email = customerDictionary["Email"]
        });
        unit.Save();
    }
    newCus.Finish();
}

void EditCustomer()
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var editeCustomer = new EditeCustomerCommand();
        editeCustomer.Execute();
        //Duplicate code
        var customers = unit.CustomerRepository.GetAllCustomers();
        for (int i = 0; i < customers.Count; i++)
        {
            Console.WriteLine($"{i}: " + customers[i].FullName + " - " + customers[i].Mobile + " - " +
                              customers[i].Addrese + " - " + customers[i].Email);
        }

        editeCustomer.CustomerIndex();
        var customerIndex = Convert.ToInt32(Console.ReadLine());
        var customerDictionary =
            editeCustomer.GetPropertyValueDictionary<Customer>(a => a.CustomerId, a => a.Accountings);
        for (int i = 0; i < customers.Count(); i++)
        {
            if (customerIndex == i)
            {
                var customerEdited = unit.CustomerRepository.UpdateCustomer(new Customer()
                {
                    CustomerId = customers[i].CustomerId,
                    FullName = customerDictionary["FullName"],
                    Mobile = customerDictionary["Mobile"],
                    Addrese = customerDictionary["Addrese"],
                    Email = customerDictionary["Email"]
                });
                unit.Save();
                editeCustomer.Finish();
            }
        }
    }
}

void DisplayCustomers(List<Customer> customers)
{
    for (int i = 0; i < customers.Count; i++)
    {
        Console.WriteLine($"{i}: " + customers[i].FullName + " - " + customers[i].Mobile + " - " +
                          customers[i].Addrese + " - " + customers[i].Email);
    }
}

void DeleteCustomer()
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var deleteCustomer = new DeleteCustomerCommand();
        deleteCustomer.Execute();
        var customers = unit.CustomerRepository.GetAllCustomers();
        DisplayCustomers(customers);
        deleteCustomer.CustomerIndex();
        var customerIndex = Convert.ToInt32(Console.ReadLine());
        for (int y = 0; y < customers.Count; y++)
        {
            if (customerIndex == y)
            {
                var customerDeleted = unit.CustomerRepository.DeleteCustomer(customers[y]);
                unit.Save();
                deleteCustomer.Finish();
            }
        }
    }
}

void SearchCustomer()
{
    var searchCommand = new SearchCustomerCommand();
    searchCommand.Execute();
    var name = Console.ReadLine();
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var customerName = unit.CustomerRepository.GetCustomerByFilter(name).ToList();
        Console.WriteLine("The customers you want ....");
        foreach (var customer in customerName)
        {
            Console.WriteLine(customer.FullName + " - " + customer.Mobile + " - " + customer.Addrese + " - " +
                              customer.Email);
        }
    }
}

void GetCustomersName()
{
    Console.WriteLine("Your customer's Name are :");
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var customersNameViewModel = unit.CustomerRepository.GetCustomerForTransaction();
        foreach (var customerName in customersNameViewModel)
        {
            Console.WriteLine(customerName.Id + "- " + customerName.FullName);
        }
    }
}

List<int> GetCustomersId()
{
    using (UnitOfWork unit = new UnitOfWork(new AccoSystemDbContext()))
    {
        var customersNameViewModel = unit.CustomerRepository.GetCustomerForTransaction();
        List<int> customersId = new List<int>();
        for (int i = 0; i < customersNameViewModel.Count; i++)
        {
            customersId.Add(customersNameViewModel[i].Id);
        }

        return customersId;
    }
}