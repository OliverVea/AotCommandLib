namespace Demo.Commands;

public class EchoCommand(CommandRunnerOptions options) : Command
{
    public override string Verb => "echo";
    public override string Description => "Prints the arguments back to the console.";

    private readonly StringArgument _textArgument = new()
    {
        Name = "text",
        ShortName = "t",
        Description = "The text to print.",
        IsRequired = true
    };

    public override IReadOnlyCollection<Argument> Arguments => [_textArgument];
    public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine(_textArgument.Value);
        return Task.FromResult(options.SuccessExitCode);
    }
}