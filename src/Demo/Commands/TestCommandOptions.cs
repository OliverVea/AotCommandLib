using Microsoft.Extensions.DependencyInjection;

namespace Demo.Commands;

public class TestCommandOptions : CommandOptions
{
    public override string Verb => "test";
    public override string Description => "A test command.";

    public static readonly FloatArgument FloatArgument = new()
    {
        Name = "float",
        ShortName = "f",
        Description = "A float argument.",
    };

    public static readonly IntArgument IntArgument = new()
    {
        Name = "int",
        ShortName = "i",
        Description = "An integer argument.",
    };
    
    public static readonly PathArgument PathArgument = new()
    {
        Name = "path",
        ShortName = "p",
        Description = "A path argument.",
    };

    public static readonly StrictIdArgument<TestCommand> StrictIdArgument = new()
    {
        Name = "id",
        ShortName = "d",
        Description = "A strict ID argument.",
    };

    public static readonly StringArgument StringArgument = new()
    {
        Name = "string",
        ShortName = "s",
        Description = "A string argument.",
    };

    public static readonly UriArgument UriArgument = new()
    {
        Name = "uri",
        ShortName = "u",
        Description = "A Uri argument.",
    };


    public override IReadOnlyCollection<Argument> Arguments => 
    [
        FloatArgument,
        IntArgument,
        PathArgument,
        StrictIdArgument,
        StringArgument,
        UriArgument,
    ];

    public override Command BuildCommand(IServiceScope serviceScope)
    {
        return serviceScope.ServiceProvider.GetRequiredService<TestCommand>();
    }

}