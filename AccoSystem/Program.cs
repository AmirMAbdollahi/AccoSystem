using System.Collections.Frozen;
using System.ComponentModel;
using System.Threading.Channels;
using AccoSystem.Commands;
using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystem.DataLayer.Services;
using AccoSystem.ViewModels;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;

//var db=new CustomerRepository(new MyDbContext());
//var customers = db.Customers.ToList();

// foreach (var customer in customers)
// {
//     Console.WriteLine(customer.FullName +" "+ customer.Addrese);
// }

/*db.Customers.Add(new Customer()
{
    Addrese = "lator",
    Email = "sasf@gmail.com",
    FullName = "sadfsfsdf",
    Mobile = "46545454",
});

db.SaveChanges();*/

/*var customer=new Customer()
{
    FullName = "amir",
    Addrese = "kashan",
    Email = "amir@gmail.com",
    Mobile = "09130304809"
};*/

/*db.InsertCustomer(new Customer()
{
    FullName = "sobhan",
    Addrese = "tabrez",
    Email = "sobhan@gmail.com",
    Mobile = "09130456789"
});

db.Save();*/

/*var acsDB = new AccoSystemDbContext();
var db = new UnitOfWork(acsDB);
var db2 = new UnitOfWork(acsDB);

var customers = db.CustomerRepository.GetAllCustomers();

foreach (var customer in customers)
{
    Console.WriteLine(customer.FullName+" "+customer.Addrese+" "+customer.Email+" "+customer.Mobile);
}

db.Dispose();*/



/*
it dosent work
var accoSystemDB = new AccoSystemDbContext();
*/

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
                UpdateCustomer();
                break;
            case 5:
                SearchCustomer();
                break;
            default:
                break;
        }
        break;
    case 2:
        Transactions();
        break;
    case 3:
        break;
    case 4:
        break;
    default:
        var wrongNum = new WrongNumberCommand();
        wrongNum.Execute();
        break;
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
    }

    if (customerSelected == -1 || typeId!=1 || typeId != 2)
    {
        Console.WriteLine("You doesn't select a customer and Correct typing. your transaction was not registered.");
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
    var customersName=GetCustomersName();
    Console.WriteLine("Your customer's Name are :");
    foreach (var customerName in customersName)
    {
        Console.WriteLine(customerName);
    }
    Console.WriteLine("Please select a customer for new transaction... ");
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

void UpdateCustomer()
{
    
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

List<string?> GetCustomersName()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var customersNameViewModel=unit.CustomerRepository.GetCustomerForTransaction();
        List<string?> customersName=new List<string?>();
        for (int i = 0; i < customersNameViewModel.Count; i++)
        {
            customersName.Add(i+"- "+customersNameViewModel[i].FullName);
        }
        return customersName;
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
