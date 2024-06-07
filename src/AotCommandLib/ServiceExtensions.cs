using AotCommandLib.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AotCommandLib;

/// <summary>
/// Contains extension methods for configuring the command runner and related services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds the command runner and related services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Optionally configures the command runner options.</param>
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<CommandRunnerOptions>? configureOptions = null)
    {
        var options = new CommandRunnerOptions();
        configureOptions?.Invoke(options);
        services.AddSingleton(options);
        
        services.AddSingleton<ArgumentParser>();
        services.AddSingleton<ICommandRunner, CommandRunner>();
        services.AddSingleton<HelpHelper>();
        
        services.AddSingleton<CommandOptions, HelpCommandOptions>();
        services.AddScoped<HelpCommand>();
        
        return services;
    }
}