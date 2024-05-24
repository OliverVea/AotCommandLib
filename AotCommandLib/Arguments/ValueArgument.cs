namespace AotCommandLib.Arguments;

/// <summary>
/// Represents an argument with a value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public abstract class ValueArgument<T> : Argument
{
    /// <summary>
    /// The value of the argument.
    /// </summary>
    /// <remarks>Is set automatically  </remarks>
    public T Value { get; private set; } = default!;
    private T? DefaultValue { get; init; }

    internal override OneOf<Success, ErrorMessage> TryParseInternal(string? value)
    {
        if (value is null)
        {
            if (IsRequired) return new ErrorMessage("The argument is required.");
            if (DefaultValue is null) return new ErrorMessage("The argument is required.");

            Value = DefaultValue;
            return new Success();
        }

        var result = TryParseValue(value);
        if (result.TryPickT1(out var errorMessage, out var parsedValue)) return errorMessage;
        
        Value = parsedValue;
        return new Success();
    }
    
    /// <summary>
    /// Parses the value of the argument.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>The parsed value or an error message.</returns>
    protected abstract OneOf<T, ErrorMessage> TryParseValue(string value);
}