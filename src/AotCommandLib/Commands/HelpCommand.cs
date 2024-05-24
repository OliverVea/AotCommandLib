using AotCommandLib.Arguments;
using AotCommandLib.Helpers;

namespace AotCommandLib.Commands;

internal class HelpCommand : Command
{
    private readonly HelpHelper _helpHelper;
    private readonly CommandRunnerOptions _commandRunnerOptions;
    
    internal HelpCommand(HelpHelper helpHelper, CommandRunnerOptions commandRunnerOptions)
    {
        _helpHelper = helpHelper;
        _commandRunnerOptions = commandRunnerOptions;
    }
    
    public override string Verb => "help";
    public override string Description => "Prints help for a command.";
    public override bool PrintArgumentsInList => true;

    private readonly StringArgument _verbArgument = new()
    {
        Name = "verb",
        ShortName = "v",
        Description = "The command to print help for."
    };

    public override IReadOnlyCollection<Argument> Arguments => [ _verbArgument ];

    public override Task<int> ExecuteAsync()
    {
        var result = _helpHelper.PrintHelp(_verbArgument.Value);
        
        return result.Match(
            success => Task.FromResult(_commandRunnerOptions.SuccessExitCode),
            error => Task.FromResult(_commandRunnerOptions.FailureExitCode)
        );
    }
}