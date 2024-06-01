using System.Reflection;
using AccoSystem.Services;

namespace AccoSystem.Commands;

public static class CommandFactory
{

    private static readonly Dictionary<string, Func<Command>> CommandRegistry = new();

    static CommandFactory()
    {
        RegisterCommand("customer", () => new CustomerCommand(new CustomerService()));
        RegisterCommand("transaction", () => new TransactionCommand(new TransactionService()));
    }

    public static void RegisterCommand(string commandType, Func<Command> constructor)
    {
        CommandRegistry[commandType.ToLower()] = constructor;
    }

    public static Command CreateCommand(string commandType)
    {
        if (CommandRegistry.TryGetValue(commandType.ToLower(), out var constructor))
        {
            return constructor();
        }
        throw new ArgumentException("Invalid command type");
    }
    
    private static List<string> GetCommandClassNames()
    {
        var assembly = Assembly.GetAssembly(typeof(Command));

        var types = assembly!.GetTypes();

        var classNameList = types
            .Where(t => t.IsSubclassOf(typeof(Command)))
            .Select(t => t.Name)
            .ToList();

        return classNameList;
    }

    public static void GetCommand(string command)
    {
        string[] commandWords = command.Split(' ');
        string firstUserCommand = commandWords[0];
        string secondUserCommand = commandWords[1];

        var classNameList = GetCommandClassNames();

        string commandPrefix = null;
        bool matchPrefixFound = classNameList.Exists(commandClass => 
        {
            commandPrefix = commandClass.Replace("Command", "");
            return commandPrefix.Equals(firstUserCommand, StringComparison.OrdinalIgnoreCase);
        });
        
        if (matchPrefixFound)
        {
            Command commandClass = CreateCommand(commandPrefix);
            switch (secondUserCommand.ToLower())
            {
                case "list":
                    commandClass.Get();
                    break;
                case "add":
                    commandClass.Add();
                    break;
                case "edit":
                    commandClass.Edit();
                    break;
                case "delete":
                    commandClass.Delete();
                    break;
                case "search":
                    commandClass.Search();
                    break;
                default:
                    Help();
                    break;
            }
        }
        else
        {
            Help();
        }

    }

    private static void Help()
    {
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine("Unfortunately, you wrote the wrong command.\nIf you do not know how to use the program, follow the instructions below.\n");
        Console.WriteLine("customer list => It gives you all the customers in the database.");
        Console.WriteLine("customer add => You can add a new customer to the database.");
        Console.WriteLine("customer edit => You can change the customer's name, mobile, phone, address and email by entering the customer's ID and save it in the database.");
        Console.WriteLine("customer delete => You can delete a customer from the database by entering the ID.");
        Console.WriteLine("customer search => You can find the customer from the database by entering its name.");
        Console.WriteLine("transaction list => It gives you all the transaction in the database.");
        Console.WriteLine("transaction add => You can add a new transaction to the database.");
        Console.WriteLine("transaction edit => You can change the transaction's amount and description by entering the transaction's ID and save it in the database.");
        Console.WriteLine("transaction delete => You can delete a transaction from the database by entering the ID.");
        Console.WriteLine("transaction search => You can find transactions between them by entering two dates.");
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine("Enjoy my app console");
        Console.WriteLine("--------------------");
    }
    
}