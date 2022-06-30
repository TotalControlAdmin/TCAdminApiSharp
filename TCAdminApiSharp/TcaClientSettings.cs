using Serilog.Events;

namespace TCAdminApiSharp;

public class TcaClientSettings
{
    public LogEventLevel MinimumLogLevel { get; set; } = LogEventLevel.Information;

    public bool ThrowOnApiResponseStatusNonComplete { get; set; }

    public static TcaClientSettings Default => new();

    public static TcaClientSettings Debug => new()
    {
        MinimumLogLevel = LogEventLevel.Debug,
        ThrowOnApiResponseStatusNonComplete = true
    };
}