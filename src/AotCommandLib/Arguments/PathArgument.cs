namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a path.
/// </summary>
public class PathArgument : ValueArgument<string>
{
    /// <inheritdoc />
    protected override OneOf<string, ErrorMessage> TryParseValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new ErrorMessage("Path cannot be empty.");
        }
        
        try
        {
            var fullPath = Path.GetFullPath(value);
            
            if (Path.IsPathFullyQualified(fullPath))
            {
                return fullPath;
            }

            return new ErrorMessage("Path is not fully qualified.");
        }
        catch (Exception ex)
        {
            return new ErrorMessage($"Invalid path: {ex.Message}");
        }
    }
}