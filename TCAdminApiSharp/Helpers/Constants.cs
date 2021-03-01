using Newtonsoft.Json;

namespace TCAdminApiSharp.Helpers
{
    internal static class Constants
    {
        public const string JsonContentType = "application/json";

        public static readonly JsonSerializerSettings IgnoreDefaultValues = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static readonly JsonSerializerSettings IgnoreReferenceLoop = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }
}