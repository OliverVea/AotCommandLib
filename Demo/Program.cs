using Demo.Commands;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddCommands(o =>
{
    o.FallbackCommand = "help";
});

serviceCollection.AddSingleton<Command, EchoCommand>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var commandRunner = serviceProvider.GetRequiredService<CommandRunner>();

return await commandRunner.RunAsync(args);