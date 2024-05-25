namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a float.
/// </summary>
public class FloatArgument : ValueArgument<float>
{
    /// <inheritdoc />
    protected override OneOf<float, ErrorMessage> TryParseValue(string value)
    {
        if (float.TryParse(value, out var floatValue))
        {
            return floatValue;
        }

        return new ErrorMessage($"Invalid float: {value}");
    }
}