using AotCommandLib.Commands;

namespace AotCommandLib;

/// <summary>
/// The command runner. This is the main entry point for running commands.
/// </summary>
public sealed class CommandRunner
{
    private readonly IEnumerable<Command> _commands;
    private readonly ArgumentParser _argumentParser;
    private readonly CommandRunnerOptions _options;
    
    internal CommandRunner(IEnumerable<Command> commands, ArgumentParser argumentParser, CommandRunnerOptions options)
    {
        _commands = commands;
        _argumentParser = argumentParser;
        _options = options;
    }
    
    /// <summary>
    /// Runs the command runner with the specified arguments.
    /// </summary>
    /// <param name="args">The arguments to run the command runner with.</param>
    /// <returns>The exit code of the command runner.</returns>
    public Task<int> RunAsync(string[] args)
    {
        if (args.Length == 0) 
        {
            if (_options.FallbackCommand is null)
            {
                Console.WriteLine("No command provided.");
                return Task.FromResult(_options.FailureExitCode);
            }

            args = [_options.FallbackCommand];
        }

        var verb = args[0];

        var command = _commands.FirstOrDefault(c => c.Verb == verb);
        
        if (command is null)
        {
            Console.WriteLine($"Command '{verb}' not found. Use 'help' to see available commands.");
            return Task.FromResult(_options.FailureExitCode);
        }

        var commandArgs = args.Skip(1).ToArray();
        
        var result = _argumentParser.AssignArguments(commandArgs, command);
        
        return result.Match(success => command.ExecuteAsync(),
            error =>
            {
                Console.WriteLine(error.Value);
                return Task.FromResult(_options.FailureExitCode);
            });
    }
}