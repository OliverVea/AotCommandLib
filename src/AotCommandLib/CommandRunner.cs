using AotCommandLib.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib;

internal sealed class CommandRunner(
    IEnumerable<CommandOptions> commandOptions,
    ArgumentParser argumentParser,
    CommandRunnerOptions options,
    IServiceProvider serviceProvider) : ICommandRunner
{
    public Task<int> RunAsync(string[] args)
    {
        if (args.Length == 0) 
        {
            if (options.FallbackCommand is null)
            {
                Console.WriteLine("No command provided.");
                return Task.FromResult(options.FailureExitCode);
            }

            args = [options.FallbackCommand];
        }

        var verb = args[0];

        var commandOption = commandOptions.FirstOrDefault(c => c.Verb == verb);
        
        if (commandOption is null)
        {
            Console.WriteLine($"Command '{verb}' not found. Use 'help' to see available commands.");
            return Task.FromResult(options.FailureExitCode);
        }

        var commandArgs = args.Skip(1).ToArray();
        
        var result = argumentParser.AssignArguments(commandArgs, commandOption);
        
        return result.Match(
            _ => ExecuteCommand(commandOption),
            error =>
            {
                Console.WriteLine(error.Value);
                return Task.FromResult(options.FailureExitCode);
            });
    }

    private Task<int> ExecuteCommand(CommandOptions commandOptions)
    {
        var scope = serviceProvider.CreateScope();
        var command = commandOptions.BuildCommand(scope);
        return command.ExecuteAsync();
    }
}