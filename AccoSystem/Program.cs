using System.Collections.Frozen;
using AccoSystem.Commands;
using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystem.DataLayer.Services;
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
                EditeCustomer();
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
    
    newCus.newFullName();
    var fullName = Console.ReadLine();
    
    newCus.newMobile();
    var mobile = Console.ReadLine();
    
    newCus.newAddrese();
    var addrese = Console.ReadLine();
    
    newCus.newEmail();
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
    newCus.finish();
}

void EditeCustomer()
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
                
                editeCustomer.newFullName();
                var newFullName = Console.ReadLine();
                if (newFullName.IsNullOrEmpty())
                {
                    newFullName=customers[i].FullName;
                }
                
                editeCustomer.newMobile();
                var newMobile = Console.ReadLine();
                if (newMobile.IsNullOrEmpty())
                {
                    newMobile = customers[i].Mobile;
                }

                editeCustomer.newAddrese();
                var newAddrese = Console.ReadLine();
                if (newAddrese.IsNullOrEmpty())
                {
                    newAddrese = customers[i].Addrese;
                }
                
                editeCustomer.newEmail();
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
                editeCustomer.finish();
            }
        }
    }
}

void DeleteCustomer()
{
    using (UnitOfWork unit=new UnitOfWork(new AccoSystemDbContext()))
    {
        var deleteCustomer = new DeleteCustomerCommand();
        deleteCustomer.Execute();
        var customers = unit.CustomerRepository.GetAllCustomers();
        for (int i = 0 ; i < customers.Count; i++)
        {
            Console.WriteLine($"{i}: "+customers[i].FullName+" - "+customers[i].Mobile+" - "+customers[i].Addrese+" - "+customers[i].Email);
        }
        deleteCustomer.CustomerIndex();
        var customerIndex = Convert.ToInt32(Console.ReadLine());
        for (int y = 0; y < customers.Count ; y++)
        {
            if (customerIndex == y)
            {
                var customerDeleted = unit.CustomerRepository.DeleteCustomer(customers[y]);
                unit.Save();
                deleteCustomer.finish();
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
        var customerName = unit.CustomerRepository.GetCustomerByFilter(name);
        Console.WriteLine("The customers you want ....");
        foreach (var customer in customerName)
        {
            Console.WriteLine(customer.FullName+" - "+customer.Mobile+" - "+customer.Addrese+" - "+customer.Email);
        }
    }
}
