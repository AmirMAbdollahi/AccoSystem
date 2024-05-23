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
    var index=FindTransaction();
    editTran.NewAmount();
    var amount = Convert.ToInt32(Console.ReadLine());
    editTran.NewDescription();
    var description = Console.ReadLine();
    int typeIndex = 0;
    int CustomerId = 0;

    /// two using
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
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
    
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        unit.AccountingRepository.Update(new Accounting()
        {
            Id = index,
            DateTime = DateTime.Now,
            Amount = amount,
            Description = description,
            CustomerId = CustomerId,
            TypeId = typeIndex
        });
        unit.Save();
    }
}

int FindTransaction()
{
    DisplayTransactionWithId();
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var transactions=unit.AccountingRepository.Get().ToList();
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
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        unit.AccountingRepository.Delete(index);
        unit.Save();
    }
    deleteTran.Finish();
}

void DisplayTransactionWithId()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var transactions=unit.AccountingRepository.Get().ToList();
        foreach (var transaction in transactions)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(transaction.CustomerId);
            Console.WriteLine(transaction.Id+" - "+name + " - " + transaction.Amount + " - " +
                              transaction.DateTime.ToShamsi() + " - "+transaction.Description);
        }
    }
}

void SearchTransaction()
{
    
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
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var accountings = unit.AccountingRepository.Get(a => a.TypeId==type).ToList();
        foreach (var accounting in accountings)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(accounting.CustomerId);
            var description = accounting.Description;
            if (accounting.Description.IsNullOrEmpty())
            {
                description = "There is no explanation.";
            }
            Console.WriteLine("Name is : " + name + " - Amount is : " + accounting.Amount + " - Date is : " +
                              accounting.DateTime.ToShamsi() + " - Description is : "+description);
        }
    }
}

void GetTransaction()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var transactions=unit.AccountingRepository.Get().ToList();
        foreach (var transaction in transactions)
        {
            string name = unit.CustomerRepository.GetCustomerNameById(transaction.CustomerId);
            Console.WriteLine(name + " - " + transaction.Amount + " - " +
                              transaction.DateTime.ToShamsi() + " - "+transaction.Description);
        }
    }
}

void Transactions()
{
    var transaction = new NewTransaction();
    transaction.Execute();
    var customerSelected=GetCustomerId();
    var typeId = GetTypeId();
    transaction.NewAmount();
    int amount = Convert.ToInt32(Console.ReadLine());
    transaction.NewDescription();
    string? description = Console.ReadLine();
    
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        unit.AccountingRepository.Insert(new Accounting()
        {
            CustomerId = customerSelected,
            Amount = amount,
            TypeId = typeId,
            DateTime = DateTime.Now,
            Description = description,
            
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

int GetTypeId()
{
    var transaction = new NewTransaction();
    transaction.NewTypeId();
    int typeId = Convert.ToInt32(Console.ReadLine());
    return typeId;
}

int GetCustomerId()
{
    GetCustomersName();
    Console.WriteLine("Please select a customer id for new transaction... ");
    var customersId=GetCustomersId();
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
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var customers = unit.CustomerRepository.GetAllCustomers();
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.FullName+" - "+customer.Mobile+" - "+customer.Addrese+" - "+customer.Email);
        }
    }
}

void NewCustomer()
{
    var newCus = new NewCustomerCommand();
    newCus.Execute();
    
    newCus.NewFullName();
    var fullName = Console.ReadLine();
    
    newCus.NewMobile();
    var mobile = Console.ReadLine();
    
    newCus.NewAddrese();
    var addrese = Console.ReadLine();
    
    newCus.NewEmail();
    var email = Console.ReadLine();
    
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var customer = unit.CustomerRepository.InsertCustomer(new Customer()
        {
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        unit.Save();
    }
    newCus.Finish();
}

void EditCustomer()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var editeCustomer = new EditeCustomerCommand();
        editeCustomer.Execute();
        //Duplicate code
        var customers = unit.CustomerRepository.GetAllCustomers();
        for (int i = 0; i < customers.Count; i++)
        {
            Console.WriteLine($"{i}: "+customers[i].FullName+" - "+customers[i].Mobile+" - "+customers[i].Addrese+" - "+customers[i].Email);
        }
        editeCustomer.CustomerIndex();
        var customerIndex = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < customers.Count(); i++)
        {
            if (customerIndex == i)
            {
                
                editeCustomer.NewFullName();
                var newFullName = Console.ReadLine();
                if (newFullName.IsNullOrEmpty())
                {
                    newFullName=customers[i].FullName;
                }
                
                editeCustomer.NewMobile();
                var newMobile = Console.ReadLine();
                if (newMobile.IsNullOrEmpty())
                {
                    newMobile = customers[i].Mobile;
                }

                editeCustomer.NewAddrese();
                var newAddrese = Console.ReadLine();
                if (newAddrese.IsNullOrEmpty())
                {
                    newAddrese = customers[i].Addrese;
                }
                
                editeCustomer.NewEmail();
                var newEmail = Console.ReadLine();
                if (newEmail.IsNullOrEmpty())
                {
                    newEmail = customers[i].Email;
                }
                
                var customerEdited = unit.CustomerRepository.UpdateCustomer(new Customer()
                {
                    CustomerId = customers[i].CustomerId,
                    FullName = newFullName,
                    Mobile = newMobile,
                    Addrese = newAddrese,
                    Email = newEmail
                });
                unit.Save();
                editeCustomer.Finish();
            }
        }
    }
}

void DisplayCustomers(List<Customer> customers)
{
    for (int i = 0 ; i < customers.Count; i++)
    {
        Console.WriteLine($"{i}: "+customers[i].FullName+" - "+customers[i].Mobile+" - "+customers[i].Addrese+" - "+customers[i].Email);
    }
}

void DeleteCustomer()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var deleteCustomer = new DeleteCustomerCommand();
        deleteCustomer.Execute();
        var customers = unit.CustomerRepository.GetAllCustomers();
        DisplayCustomers(customers);
        deleteCustomer.CustomerIndex();
        var customerIndex = Convert.ToInt32(Console.ReadLine());
        for (int y = 0; y < customers.Count ; y++)
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
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var customerName = unit.CustomerRepository.GetCustomerByFilter(name).ToList();
        Console.WriteLine("The customers you want ....");
        foreach (var customer in customerName)
        {
            Console.WriteLine(customer.FullName+" - "+customer.Mobile+" - "+customer.Addrese+" - "+customer.Email);
        }
    }
}

void GetCustomersName()
{
    Console.WriteLine("Your customer's Name are :");
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var customersNameViewModel=unit.CustomerRepository.GetCustomerForTransaction();
        foreach (var customerName in customersNameViewModel)
        {
            Console.WriteLine(customerName.Id+"- "+customerName.FullName);
        }
    }
}
List<int> GetCustomersId()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var customersNameViewModel=unit.CustomerRepository.GetCustomerForTransaction();
        List<int> customersId=new List<int>();
        for (int i = 0; i < customersNameViewModel.Count; i++)
        {
            customersId.Add(customersNameViewModel[i].Id);
        }
        return customersId;
    }
}
