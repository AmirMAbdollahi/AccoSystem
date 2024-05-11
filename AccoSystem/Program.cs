using System.Collections.Frozen;
using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystem.DataLayer.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;

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

var db = new UnitOfWork();

var customers = db.CustomerRepository.GetAllCustomers();

foreach (var customer in customers)
{
    Console.WriteLine(customer.FullName+" "+customer.Addrese+" "+customer.Email+" "+customer.Mobile);
}

db.Dispose();

