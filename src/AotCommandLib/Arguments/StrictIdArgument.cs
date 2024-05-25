using StrictId;

namespace AotCommandLib.Arguments;

/// <summary>
/// Argument for a StrictId Id.
/// </summary>
/// <typeparam name="T"></typeparam>
public class StrictIdArgument<T> : ValueArgument<Id<T>>
{
    /// <inheritdoc />
    protected override OneOf<Id<T>, ErrorMessage> TryParseValue(string value)
    {
        if (Id<T>.TryParse(value, out var id))
        {
            return id;
        }

        return new ErrorMessage($"Invalid Id: {value}");
    }
}