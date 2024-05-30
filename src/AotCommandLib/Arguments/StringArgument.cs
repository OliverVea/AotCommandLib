namespace AotCommandLib.Arguments;

/// <summary>
/// Represents a string argument.
/// </summary>
public sealed class StringArgument : ValueArgument<string>
{
    /// <summary>
    /// Parses the value of the argument.
    /// </summary>
    protected override OneOf<string, Error<string>> TryParseValue(string value)
    {
        return value;
    }
}