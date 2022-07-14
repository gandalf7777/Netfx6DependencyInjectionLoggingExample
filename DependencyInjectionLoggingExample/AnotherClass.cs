namespace RedBeans.Examples.Console.DependencyInjectionLoggingExample;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

internal class AnotherClass
{
    private readonly ILogger _logger;
    private readonly IOptions<ConfigSection1Settings> _configSection1;
    public AnotherClass(IOptions<ConfigSection1Settings> configSection1, ILogger<AnotherClass> logger)
    {
        _logger = logger;
        _configSection1 = configSection1;
    }
    public void AnotherFunction()
    {
        _logger.LogInformation($"I can tell you the value of configSection1.Config1 is {_configSection1.Value.Config1}");
    }
}

