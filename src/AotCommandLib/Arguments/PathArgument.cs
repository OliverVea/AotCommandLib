namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a path.
/// </summary>
public class PathArgument : ValueArgument<string>
{
    /// <inheritdoc />
    protected override OneOf<string, Error<string>> TryParseValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new Error<string>("Path cannot be empty.");
        }
        
        try
        {
            var fullPath = Path.GetFullPath(value);
            
            if (Path.IsPathFullyQualified(fullPath))
            {
                return fullPath;
            }

            return new Error<string>("Path is not fully qualified.");
        }
        catch (Exception ex)
        {
            return new Error<string>($"Invalid path: {ex.Message}");
        }
    }
    
    /// <inheritdoc />
    protected override string PostParse(string value)
    {
        return Path.GetFullPath(value);
    }
}