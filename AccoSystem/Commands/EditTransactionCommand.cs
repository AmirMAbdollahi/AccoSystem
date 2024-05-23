namespace AccoSystem.Commands;

public class EditTransactionCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to edite a transaction ...");
    }
    
    public void TransactionIndex()
    {
        Console.WriteLine("Please enter the id of the transaction you want to edit ...");
    }
    
    public void NewAmount()
    {
        Console.WriteLine("Do you want to edite transaction's amount ? if not press enter.");
    }
    public void NewDescription()
    {
        Console.WriteLine("Do you want to edite transaction's Description ? if not press enter.");
    }
    
}