namespace AotCommandLib.Commands;

/// <summary>
/// Represents a command.
/// </summary>
public abstract class Command
{
    /// <summary>
    /// Executes the command.
    /// </summary>
    public abstract Task<int> ExecuteAsync(CancellationToken cancellationToken = default);
}