namespace AccoSystem.Commands;

public class DeleteTransaction:Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to delete a transaction ...");
    }

    public Dictionary<string, string> GetPropertyValueDictionary<T>()
    {
        throw new NotImplementedException();
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