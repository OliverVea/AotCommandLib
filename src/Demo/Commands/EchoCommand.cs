namespace Demo.Commands;

public class EchoCommand(CommandRunnerOptions options) : Command
{
    public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine(EchoCommandOptions.TextArgument.Value);
        return Task.FromResult(options.SuccessExitCode);
    }
}