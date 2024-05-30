namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a Uri.
/// </summary>
public class UriArgument : ValueArgument<Uri>
{
    /// <inheritdoc />
    protected override OneOf<Uri, Error<string>> TryParseValue(string value)
    {
        if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            return uri;
        }

        return new Error<string>($"Invalid Uri: {value}");
    }
}