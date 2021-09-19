using Serilog.Events;

namespace TCAdminApiSharp
{
    public class TcaClientSettings
    {
        public LogEventLevel MinimumLogLevel { get; set; } = LogEventLevel.Error;
        public bool ThrowOnApiSuccessFailure { get; set; } = true;

        public bool ThrowOnApiResponseStatusNonComplete { get; set; } = true;
        public bool ThrowOnApiStatusCodeNonOk { get; set; } = true;

        public static TcaClientSettings Default => new();

        public static TcaClientSettings Debug => new()
        {
            MinimumLogLevel = LogEventLevel.Debug,
            ThrowOnApiSuccessFailure = true
        };
    }
}