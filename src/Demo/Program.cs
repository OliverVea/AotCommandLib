using Demo.Commands;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddCommands(o =>
{
    o.FallbackCommand = "help";
});

serviceCollection.AddScoped<EchoCommand>();
serviceCollection.AddSingleton<CommandOptions, EchoCommandOptions>();
serviceCollection.AddScoped<TestCommand>();
serviceCollection.AddSingleton<CommandOptions, TestCommandOptions>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var commandRunner = serviceProvider.GetRequiredService<ICommandRunner>();

return await commandRunner.RunAsync(args);