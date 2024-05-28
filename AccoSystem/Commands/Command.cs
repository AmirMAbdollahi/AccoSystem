using System.Linq.Expressions;
using System.Text;
using AccoSystem.DataLayer;

namespace AccoSystem.Commands;

public abstract class Command
{
    // private Dictionary<string, string> _commandList;
    //
    // public Dictionary<string, string> CommandList
    // {
    //     get
    //     {
    //         return 
    //     }
    //     set
    //     {
    //         _commandList = CommandsValue();
    //     }
    // }

    public Dictionary<string, string> CommandsValue(string path)
    {
        Dictionary<string, string> commands = new Dictionary<string, string>();
        
        switch (GetLastPath(path))
        {
            case "ConsoleApp":
                commands.Add("AccoSystem", "1");
                break;
            case "AccoSystem":
                commands.Add("Customer", "1");
                commands.Add("Transaction", "1");
                commands.Add("Back", "1");
                break;
            case "Customer":
                commands.Add("Get", "1");
                commands.Add("Add", "1");
                commands.Add("Edit", "1");
                commands.Add("Delete", "1");
                commands.Add("Search", "1");
                commands.Add("Back", "1");
                break;
            case "Transaction":
                commands.Add("Get", "1");
                commands.Add("Add", "1");
                commands.Add("Edit", "1");
                commands.Add("Delete", "1");
                commands.Add("Search", "1");
                commands.Add("Report", "1");
                commands.Add("Back", "1");
                break;
            case "Report":
                commands.Add("Income", "1");
                commands.Add("Cost", "1");
                commands.Add("Back", "1");
                break;
        }
        commands.Add("Help", "1");
        return commands;
    }

    public Dictionary<string, string> GetPropertyValueDictionary<T>(
        params Expression<Func<T, object>>[] ignoredProperties)
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        //var propertyNameList = properties.Select(p => p.Name).ToList();
        var ignorePropertyNameList = new List<string>();
        foreach (var ignoredProperty in ignoredProperties)
        {
            switch (ignoredProperty.Body)
            {
                case UnaryExpression { Operand: MemberExpression memberExpression }:
                    ignorePropertyNameList.Add(memberExpression.Member.Name);
                    break;
                case MemberExpression memberExp:
                    ignorePropertyNameList.Add(memberExp.Member.Name);
                    break;
            }
        }

        Dictionary<string, string> customerDictionary = new();
        foreach (var property in properties)
        {
            if (ignorePropertyNameList.Contains(property.Name))
                continue;
            start:
            Console.Write($"Please enter {property.Name}: ");
            var propertyValue = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                Console.WriteLine("Please enter valid value");
                goto start;
            }

            customerDictionary.Add(property.Name, propertyValue);
        }

        return customerDictionary;
    }

    public void CommandAnalysis(string command)
    {
        // foreach (var value in CommandsValue())
        // {
        //     if (command == value.Key)
        //     {
        //     }
        // }
    }

    private void Path(string command)
    {
    }

    private string GetLastPath(string path)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in path)
        {
            sb.Append(c);
            if (c.ToString().Contains("/"))
            {
                sb.Clear();
            }
        }

        return sb.ToString();
    }
}