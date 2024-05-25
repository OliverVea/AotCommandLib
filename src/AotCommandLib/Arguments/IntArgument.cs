namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for an integer.
/// </summary>
public class IntArgument : ValueArgument<int>
{
    /// <inheritdoc />
    protected override OneOf<int, ErrorMessage> TryParseValue(string value)
    {
        if (int.TryParse(value, out var intValue))
        {
            return intValue;
        }

        return new ErrorMessage($"Invalid integer: {value}");
    }
}