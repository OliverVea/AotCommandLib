using System.Text;
using AotCommandLib.Arguments;
using AotCommandLib.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib;

internal class HelpHelper(IServiceProvider services)
{
    internal OneOf<Success, Error<string>> PrintHelp(string? verb)
    {
        var commandOptions = services.GetServices<CommandOptions>();

        if (verb == null)
        {
            PrintAllCommands(commandOptions);
            return new Success();
        }
        
        var command = commandOptions.FirstOrDefault(c => c.Verb == verb);
        if (command == null) return new Error<string>($"Command '{verb}' not found.");
        
        PrintCommand(command);
        return new Success();
    }

    private static void PrintAllCommands(IEnumerable<CommandOptions> commandOptions)
    {
        Console.WriteLine("Available Commands:");

        foreach (var c in commandOptions)
        {
            PrintCommand(c, c.PrintArgumentsInList);
        }
    }
    
    private static void PrintCommand(CommandOptions commandOptions, bool printArguments = true)
    {
        var sb = new StringBuilder();
        
        sb.Append(commandOptions.Verb);
        sb.Append($": {commandOptions.Description}");
        
        Console.WriteLine(sb.ToString());
        
        if (!printArguments) return;
        
        foreach (var arg in commandOptions.Arguments) PrintArgument(arg);
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