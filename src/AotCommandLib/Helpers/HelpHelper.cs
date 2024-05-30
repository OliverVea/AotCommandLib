using System.Text;
using AotCommandLib.Arguments;
using AotCommandLib.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib;

internal class HelpHelper(IServiceProvider services)
{
    internal OneOf<Success, Error<string>> PrintHelp(string? verb)
    {
        var commands = services.GetServices<Command>();

        if (verb == null)
        {
            PrintAllCommands(commands);
            return new Success();
        }
        
        var command = commands.FirstOrDefault(c => c.Verb == verb);
        if (command == null) return new Error<string>($"Command '{verb}' not found.");
        
        PrintCommand(command);
        return new Success();
    }

    private static void PrintAllCommands(IEnumerable<Command> commands)
    {
        Console.WriteLine("Available commands:");

        foreach (var c in commands)
        {
            Console.WriteLine();
            PrintCommand(c, c.PrintArgumentsInList);
        }
    }
    
    private static void PrintCommand(Command command, bool printArguments = true)
    {
        var sb = new StringBuilder();
        
        sb.Append(command.Verb);
        sb.Append($": {command.Description}");
        
        Console.WriteLine(sb.ToString());
        
        if (!printArguments) return;
        
        foreach (var arg in command.Arguments) PrintArgument(arg);
    }
    
    private static void PrintArgument(Argument arg)
    {
        var sb = new StringBuilder("\t");
        
        if (arg.IsRequired) sb.Append("[required] ");
        
        if (arg.ShortName != null) sb.Append($"-{arg.ShortName}, ");
        sb.Append($"--{arg.Name}");
        
        sb.Append($": {arg.Description}");
        
        Console.WriteLine(sb.ToString());
    }
}