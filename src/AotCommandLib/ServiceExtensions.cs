using AotCommandLib.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class ServiceExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
{
    /// <summary>
    /// Adds the command runner and related services to the service collection.
    /// </summary>
    /// <param name="configureOptions">Optionally configures the command runner options.</param>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<CommandRunnerOptions>? configureOptions = null)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
    {
        var options = new CommandRunnerOptions();
        configureOptions?.Invoke(options);
        services.AddSingleton(options);
        
        services.AddSingleton<ArgumentParser>();
        
        services.AddSingleton(BuildCommandRunner);
        services.AddSingleton(BuildHelpHelper);
        services.AddSingleton(BuildHelpCommand);
        
        services.AddSingleton<Command, HelpCommand>(c => c.GetRequiredService<HelpCommand>());
        
        return services;
    }

    private static CommandRunner BuildCommandRunner(IServiceProvider serviceProvider)
    {
        var commands = serviceProvider.GetServices<Command>();
        var argumentParser = serviceProvider.GetRequiredService<ArgumentParser>();
        var options = serviceProvider.GetRequiredService<CommandRunnerOptions>();
        
        return new CommandRunner(commands, argumentParser, options);
    }
    
    private static HelpHelper BuildHelpHelper(IServiceProvider serviceProvider)
    {
        return new HelpHelper(serviceProvider);
    }
    
    private static HelpCommand BuildHelpCommand(IServiceProvider serviceProvider)
    {
        var helpHelper = serviceProvider.GetRequiredService<HelpHelper>();
        var options = serviceProvider.GetRequiredService<CommandRunnerOptions>();
        
        return new HelpCommand(helpHelper, options);
    }
    
}