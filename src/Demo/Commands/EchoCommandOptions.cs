using Microsoft.Extensions.DependencyInjection;

namespace Demo.Commands;

public class EchoCommandOptions : CommandOptions
{
    public override string Verb => "echo";
    public override string Description => "Prints the arguments back to the console.";

    internal static readonly StringArgument TextArgument = new()
    {
        Name = "text",
        ShortName = "t",
        Description = "The text to print.",
        IsRequired = true
    };

    public override IReadOnlyCollection<Argument> Arguments => [TextArgument];
    public override Command BuildCommand(IServiceScope serviceScope)
    {
        return serviceScope.ServiceProvider.GetRequiredService<EchoCommand>();
    }

}