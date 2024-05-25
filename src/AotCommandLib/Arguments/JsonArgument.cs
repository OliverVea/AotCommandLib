using System.Text.Json;

namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a JSON object.
/// </summary>
/// <typeparam name="T"></typeparam>
public class JsonArgument<T> : ValueArgument<T>
{
    /// <inheritdoc />
    protected override OneOf<T, ErrorMessage> TryParseValue(string value)
    {
        try
        {
            var obj = JsonSerializer.Deserialize<T>(value);
            if (obj == null) return new ErrorMessage($"Invalid JSON: {value}");
            
            return obj;
        }
        catch (JsonException ex)
        {
            return new ErrorMessage($"Invalid JSON: {ex.Message}");
        }
    }
}