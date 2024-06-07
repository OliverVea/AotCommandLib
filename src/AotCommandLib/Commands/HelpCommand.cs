namespace AotCommandLib.Commands;

internal class HelpCommand(HelpHelper helpHelper, CommandRunnerOptions commandRunnerOptions) : Command
{
    public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = helpHelper.PrintHelp(HelpCommandOptions.VerbArgument.Value);
        
        return result.Match(
            _ => Task.FromResult(commandRunnerOptions.SuccessExitCode),
            _ => Task.FromResult(commandRunnerOptions.FailureExitCode)
        );
    }
}