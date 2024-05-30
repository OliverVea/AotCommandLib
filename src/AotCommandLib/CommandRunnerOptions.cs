namespace AotCommandLib;

/// <summary>
/// Represents the options for the command runner.
/// </summary>
public class CommandRunnerOptions
{
    private const int DefaultSuccessExitCode = 0;
    private const int DefaultFailureExitCode = -1;
    
    /// <summary>
    /// The indent to use when printing help.
    /// </summary>
    public string Indent { get; set; } = "  ";

    /// <summary>
    /// The command to run if no command is provided.
    /// </summary>
    public string? FallbackCommand { get; set; } = "help";
    
    /// <summary>
    /// The exit code to return if the command is successful.
    /// </summary>
    public int SuccessExitCode { get; set; } = DefaultSuccessExitCode;
    
    /// <summary>
    /// The exit code to return if the command fails.
    /// </summary>
    public int FailureExitCode { get; set; } = DefaultFailureExitCode;
}