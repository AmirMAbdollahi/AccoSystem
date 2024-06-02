using AccoSystem.Commands;


while (true)
{
    Console.WriteLine("Please enter what you need...");
    var input=Console.ReadLine();
    CommandFactory.GetCommand(input);
    Console.WriteLine();
}
