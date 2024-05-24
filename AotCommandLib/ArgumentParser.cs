using AotCommandLib.Arguments;
using AotCommandLib.Commands;

namespace AotCommandLib;

internal sealed class ArgumentParser
{
    internal OneOf<Success, ErrorMessage> AssignArguments(string[] args, Command command)
    {
        var nameLookup = command.Arguments.ToDictionary(a => a.Name, a => a);
        var shortNameLookup = command.Arguments.Where(a => a.ShortName != null).ToDictionary(a => a.ShortName!, a => a);

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            var result = GetArgument(arg, nameLookup, shortNameLookup);
            if (result.TryPickT1(out var errorMessage, out var argument)) return errorMessage;

            var value = i + 1 < args.Length ? args[i + 1] : null;
            if (value is null || value.StartsWith("-") || value.StartsWith("--"))
            {
                var parseResult = argument.TryParse(null);
                if (parseResult.TryPickT1(out errorMessage, out _)) return errorMessage;
            }
            else
            {
                var parseResult = argument.TryParse(value);
                if (parseResult.TryPickT1(out errorMessage, out _)) return errorMessage;
                i++;
            }
        }
        
        var requiredArguments = command.Arguments.Where(a => a is { IsRequired: true, IsSet: false }).ToList();
        if (requiredArguments.Any())
        {
            var missingArguments = string.Join(", ", requiredArguments.Select(a => a.Name));
            return new ErrorMessage($"Missing required arguments: {missingArguments}.");
        }
        
        return new Success();
    }

    private OneOf<Argument, ErrorMessage> GetArgument(string s, Dictionary<string, Argument> nameLookup, Dictionary<string, Argument> shortNameLookup)
    {
        if (s.StartsWith("--"))
        {
            var name = s[2..];
            if (nameLookup.TryGetValue(name, out var argument)) return argument;
            return new ErrorMessage($"Unknown argument '{name}'.");
        }

        if (s.StartsWith("-"))
        {
            var shortName = s[1..];
            if (shortNameLookup.TryGetValue(shortName, out var argument)) return argument;
            return new ErrorMessage($"Unknown argument '{shortName}'.");
        }

        return new ErrorMessage($"Arguments must start with '-' or '--'. Got '{s}'.");
    }
}