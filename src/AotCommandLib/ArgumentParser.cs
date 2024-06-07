using AotCommandLib.Arguments;
using AotCommandLib.Commands;

namespace AotCommandLib;

internal sealed class ArgumentParser
{
    internal OneOf<Success, Error<string>> AssignArguments(string[] args, CommandOptions commandOptions)
    {
        var nameLookup = commandOptions.Arguments.ToDictionary(a => a.Name, a => a);
        var shortNameLookup = commandOptions.Arguments.Where(a => a.ShortName != null).ToDictionary(a => a.ShortName!, a => a);

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
        
        var requiredArguments = commandOptions.Arguments.Where(a => a is { IsRequired: true, IsSet: false }).ToList();
        if (requiredArguments.Any())
        {
            var missingArguments = string.Join(", ", requiredArguments.Select(GetRequiredArgumentString));
            return new Error<string>($"Missing required arguments: {missingArguments}.");
        }
        
        var missingValues = commandOptions.Arguments.Where(a => a is { IsSet: false }).ToList();
        foreach (var missingValue in missingValues)
        {
            var parseResult = missingValue.TryParse(null);
            if (parseResult.TryPickT1(out var errorMessage, out _)) return errorMessage;
        }
        
        return new Success();
    }

    private string GetRequiredArgumentString(Argument argument)
    {
        if (argument.ShortName is not null) return $"[-{argument.ShortName} | --{argument.Name}]";
        return $"--{argument.Name}";
    }

    private OneOf<Argument, Error<string>> GetArgument(string s, Dictionary<string, Argument> nameLookup, Dictionary<string, Argument> shortNameLookup)
    {
        if (s.StartsWith("--"))
        {
            var name = s[2..];
            if (nameLookup.TryGetValue(name, out var argument)) return argument;
            return new Error<string>($"Unknown argument '{name}'.");
        }

        if (s.StartsWith("-"))
        {
            var shortName = s[1..];
            if (shortNameLookup.TryGetValue(shortName, out var argument)) return argument;
            return new Error<string>($"Unknown argument '{shortName}'.");
        }

        return new Error<string>($"Arguments must start with '-' or '--'. Got '{s}'.");
    }
}