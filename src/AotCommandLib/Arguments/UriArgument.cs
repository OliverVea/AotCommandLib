namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a Uri.
/// </summary>
public class UriArgument : ValueArgument<Uri>
{
    /// <inheritdoc />
    protected override OneOf<Uri, ErrorMessage> TryParseValue(string value)
    {
        if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            return uri;
        }

        return new ErrorMessage($"Invalid Uri: {value}");
    }
}