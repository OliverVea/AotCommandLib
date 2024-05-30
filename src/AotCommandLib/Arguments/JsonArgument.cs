using System.Text.Json;

namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a JSON object.
/// </summary>
/// <typeparam name="T"></typeparam>
public class JsonArgument<T> : ValueArgument<T>
{
    /// <inheritdoc />
    protected override OneOf<T, Error<string>> TryParseValue(string value)
    {
        try
        {
            var obj = JsonSerializer.Deserialize<T>(value);
            if (obj == null) return new Error<string>($"Invalid JSON: {value}");
            
            return obj;
        }
        catch (JsonException ex)
        {
            return new Error<string>($"Invalid JSON: {ex.Message}");
        }
    }
}