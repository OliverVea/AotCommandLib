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
    
    /// <summary>
    /// The default value of the argument.
    /// </summary>
    public T DefaultValue { get; init; } = default!;

    internal override OneOf<Success, Error<string>> TryParseInternal(string? value)
    {
        if (value is null)
        {
            if (IsRequired) return new Error<string>("The argument is required.");

            Value = PostParse(DefaultValue);
            return new Success();
        }

        var result = TryParseValue(value);
        if (result.TryPickT1(out var errorMessage, out var parsedValue)) return errorMessage;
        
        Value = PostParse(parsedValue);
        return new Success();
    }
    
    /// <summary>
    /// Parses the value of the argument.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>The parsed value or an error message.</returns>
    protected abstract OneOf<T, Error<string>> TryParseValue(string value);
    
    /// <summary>
    /// Post-parses the value of the argument.
    /// </summary>
    /// <param name="value">The value to post-parse.</param>
    /// <returns>The post-parsed value.</returns>
    protected virtual T PostParse(T value) => value;
}