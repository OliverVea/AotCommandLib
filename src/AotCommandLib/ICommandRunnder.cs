namespace AotCommandLib;

/// <summary>
/// The command runner. This is the main entry point for running commands.
/// </summary>
public interface ICommandRunner
{
    /// <summary>
    /// Runs the command runner with the specified arguments.
    /// </summary>
    /// <param name="args">The arguments to run the command runner with.</param>
    /// <returns>The exit code of the command runner.</returns>
    Task<int> RunAsync(string[] args);
}