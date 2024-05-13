namespace AccoSystem.Commands;

public class EditeCustomerCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to edite a customer.");
    }
    
    //Duplicate code
    public void CustomerIndex()
    {
        Console.WriteLine("Please enter the number of the customer you want to edit ...");
    }
    
    public void NewFullName()
    {
        Console.WriteLine("Do you want to edite customer's name ? if not press enter.");
    }
    public void NewEmail()
    {
        Console.WriteLine("Do you want to edite customer's email ? if not press enter.");
    }
    public void NewMobile()
    {
        Console.WriteLine("Do you want to edite customer's mobile ? if not press enter.");
    }
    public void NewAddrese()
    {
        Console.WriteLine("Do you want to edite customer's addrese ? if not press enter.");
    }
    public void Finish()
    {
        Console.WriteLine("Your customer edited...");
    }
    
    /*void WritePropertyCommand<T>()
    {
        var type = typeof(T);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            Console.Write($"Please enter {property.Name}: ");
            Console.ReadLine();
        }
    }*/
}