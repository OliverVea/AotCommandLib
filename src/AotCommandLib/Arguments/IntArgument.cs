namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for an integer.
/// </summary>
public class IntArgument : ValueArgument<int>
{
    /// <inheritdoc />
    protected override OneOf<int, Error<string>> TryParseValue(string value)
    {
        if (int.TryParse(value, out var intValue))
        {
            return intValue;
        }

        return new Error<string>($"Invalid integer: {value}");
    }
}