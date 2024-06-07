namespace Demo.Commands;

public class TestCommand : Command
{
    public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Float: {TestCommandOptions.FloatArgument.Value}");
        Console.WriteLine($"Int: {TestCommandOptions.IntArgument.Value}");
        Console.WriteLine($"Path: {TestCommandOptions.PathArgument.Value}");
        Console.WriteLine($"Strict ID: {TestCommandOptions.StrictIdArgument.Value}");
        Console.WriteLine($"String: {TestCommandOptions.StringArgument.Value}");
        Console.WriteLine($"Uri: {TestCommandOptions.UriArgument.Value}");

        return Task.FromResult(0);
    }
}