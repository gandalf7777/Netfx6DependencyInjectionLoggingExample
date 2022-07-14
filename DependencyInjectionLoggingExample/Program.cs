
//namespace RedBeans.Examples.Console.DependencyInjectionLoggingExample;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RedBeans.Examples.Console.DependencyInjectionLoggingExample;

Console.WriteLine("Hello, World!");
await Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<MyApplication>()
        .AddTransient<AnotherClass>()
        .AddOptions<ConfigSection1Settings>().Bind(context.Configuration.GetSection("ConfigSection1"));
    })
    .ConfigureLogging((context, logging) =>
    {
        logging.ClearProviders();
        logging.AddConfiguration(context.Configuration.GetSection("NLog"));
        logging.AddNLog();
    })
    .RunConsoleAsync();