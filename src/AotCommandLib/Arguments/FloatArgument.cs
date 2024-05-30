namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a float.
/// </summary>
public class FloatArgument : ValueArgument<float>
{
    /// <inheritdoc />
    protected override OneOf<float, Error<string>> TryParseValue(string value)
    {
        if (float.TryParse(value, out var floatValue))
        {
            return floatValue;
        }

        return new Error<string>($"Invalid float: {value}");
    }
}