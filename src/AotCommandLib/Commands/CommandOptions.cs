using AotCommandLib.Arguments;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib.Commands;

/// <summary>
/// Represents options for a command.
/// </summary>
public abstract class CommandOptions
{
    /// <summary>
    /// The verb of the command.
    /// </summary>
    /// <example>help</example>
    public abstract string Verb { get; }
    
    /// <summary>
    /// The description of the command.
    /// </summary>
    /// <example>Displays help information about the available commands.</example>
    public abstract string Description { get; }
    
    /// <summary>
    /// Whether to print the arguments in the list of available commands.
    /// If <see langword="true"/>, the arguments will be printed in the list of available commands.
    /// If <see langword="false"/>, the arguments will only be printed when the command is printed individually.
    /// </summary>
    /// <example>false</example>
    public virtual bool PrintArgumentsInList => false;
    
    /// <summary>
    /// The arguments of the command.
    /// </summary>
    public abstract IReadOnlyCollection<Argument> Arguments { get; }

    /// <summary>
    /// Builds the command from the command option.
    /// </summary>
    /// <param name="serviceScope">The service scope to build the command with.</param>
    /// <returns>The built command.</returns>
    public abstract Command BuildCommand(IServiceScope serviceScope);
}