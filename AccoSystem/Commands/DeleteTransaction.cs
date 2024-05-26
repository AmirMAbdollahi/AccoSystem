namespace AccoSystem.Commands;

public class DeleteTransaction:Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to delete a transaction ...");
    }
    public void TransactionIndex()
    {
        Console.WriteLine("Please enter the id of the transaction you want to delete ...");
    }
    
    public void Finish()
    {
        Console.WriteLine("The transaction you wanted was deleted.");
    }
}