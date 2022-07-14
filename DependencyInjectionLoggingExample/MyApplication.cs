namespace RedBeans.Examples.Console.DependencyInjectionLoggingExample;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
internal class MyApplication : IHostedService
{
    private int? _exitCode;
    private readonly ILogger<MyApplication> _logger;
    private readonly AnotherClass _another;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public MyApplication(IHostApplicationLifetime applicationLifetime, AnotherClass another, ILogger<MyApplication> logger)
    {
        _applicationLifetime = applicationLifetime;
        _another = another;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting Application with arguments: {string.Join(";", Environment.GetCommandLineArgs())}");
        _applicationLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("My Application Business logic starts here!!!");
                    _another.AnotherFunction();
                    await Task.Delay(1000, cancellationToken);
                    _exitCode = 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception");
                    _exitCode = 1;
                }
                finally
                {
                    _applicationLifetime.StopApplication();
                }
            });
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Exiting with return code: {_exitCode}");
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        return Task.CompletedTask;
    }
}