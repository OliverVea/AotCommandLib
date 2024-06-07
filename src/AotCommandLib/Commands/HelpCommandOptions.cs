using AotCommandLib.Arguments;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib.Commands;

/// <summary>
/// Represents the help command option.
/// </summary>
internal class HelpCommandOptions : CommandOptions
{
    /// <inheritdoc />
    public override string Verb => "help";

    /// <inheritdoc />
    public override string Description => "Prints help for a command.";

    /// <inheritdoc />
    public override bool PrintArgumentsInList => true;
    
    public static readonly StringArgument VerbArgument = new()
    {
        Name = "verb",
        ShortName = "v",
        Description = "The command to print help for."
    };
    
    public override IReadOnlyCollection<Argument> Arguments => [ VerbArgument ];
    
    public override Command BuildCommand(IServiceScope serviceScope) => serviceScope.ServiceProvider.GetRequiredService<HelpCommand>();
}