namespace AotCommandLib.Arguments;

/// <summary>
/// Represents an argument.
/// </summary>
public abstract class Argument
{
    /// <summary>
    /// The name of the argument. This is the name that will be used to refer to the argument.
    /// </summary>
    /// <example>output</example>
    /// <remarks>'output' will mean that the argument can be referred to as '--output'.</remarks>
    public required string Name { get; init; }
    
    /// <summary>
    /// The short name of the argument. This is the name that will be used to refer to the argument in a short form.
    /// </summary>
    /// <example>o</example>
    /// <remarks>'o' will mean that the argument can be referred to as '-o'.</remarks>
    public string? ShortName { get; init; }
    
    /// <summary>
    /// The description of the argument.
    /// </summary>
    /// <example>The output file.</example>
    /// <remarks>This will be displayed in the help message.</remarks>
    public required string Description { get; init; }
    
    /// <summary>
    /// Whether the argument is required.
    /// </summary>
    /// <example>true</example>
    /// <remarks>If <see langword="true"/>, the argument must be provided, otherwise an error will be displayed.</remarks>
    public bool IsRequired { get; init; }
    
    internal bool IsSet { get; private set; }

    internal OneOf<Success, Error<string>> TryParse(string? value)
    {
        if (IsSet) return new Error<string>("The argument is already set.");
        
        var result = TryParseInternal(value);
        if (result.TryPickT1(out var errorMessage, out _)) return errorMessage;
        
        IsSet = true;
        return new Success();
    }
    
    internal abstract OneOf<Success, Error<string>> TryParseInternal(string? value);
}