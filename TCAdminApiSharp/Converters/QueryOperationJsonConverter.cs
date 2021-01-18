using System;
using Newtonsoft.Json;
using TCAdminApiSharp.Querying;

namespace TCAdminApiSharp.Converters
{
    public class QueryOperationJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value != null && value.GetType().IsAssignableTo(typeof(IQueryOperation)))
            {
                writer.WriteValue(((IQueryOperation)value).GenerateQuery());
            }
            else
            {
                throw new Exception($"Cannot convert {value?.GetType()} to {typeof(IQueryOperation)}");
            }
        }

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}