using System.Linq.Expressions;
using AccoSystem.DataLayer;

namespace AccoSystem.Commands;

public abstract class Command
{
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
}