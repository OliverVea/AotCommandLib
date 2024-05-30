namespace Demo.Commands;

public class TestCommand : Command
{
    public override string Verb => "test";
    public override string Description => "A test command.";

    private readonly FloatArgument _floatArgument = new()
    {
        Name = "float",
        ShortName = "f",
        Description = "A float argument.",
    };

    private readonly IntArgument _intArgument = new()
    {
        Name = "int",
        ShortName = "i",
        Description = "An integer argument.",
    };

    private readonly JsonArgument<Dictionary<string, string>> _jsonArgument = new()
    {
        Name = "json",
        ShortName = "j",
        Description = "A JSON argument.",
    };
    
    private readonly PathArgument _pathArgument = new()
    {
        Name = "path",
        ShortName = "p",
        Description = "A path argument.",
    };

    private readonly StrictIdArgument<TestCommand> _strictIdArgument = new()
    {
        Name = "id",
        ShortName = "d",
        Description = "A strict ID argument.",
    };

    private readonly StringArgument _stringArgument = new()
    {
        Name = "string",
        ShortName = "s",
        Description = "A string argument.",
    };

    private readonly UriArgument _uriArgument = new()
    {
        Name = "uri",
        ShortName = "u",
        Description = "A Uri argument.",
    };


    public override IReadOnlyCollection<Argument> Arguments => 
    [
        _floatArgument,
        _intArgument,
        _jsonArgument,
        _pathArgument,
        _strictIdArgument,
        _stringArgument,
        _uriArgument,
    ];

    public override Task<int> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Float: {_floatArgument.Value}");
        Console.WriteLine($"Int: {_intArgument.Value}");
        Console.WriteLine($"Json: {_jsonArgument.Value}");
        Console.WriteLine($"Path: {_pathArgument.Value}");
        Console.WriteLine($"Strict ID: {_strictIdArgument.Value}");
        Console.WriteLine($"String: {_stringArgument.Value}");
        Console.WriteLine($"Uri: {_uriArgument.Value}");

        return Task.FromResult(0);
    }
}