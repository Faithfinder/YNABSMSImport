using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace YNABConnector.YNABObjectModel
{
    public class SaveTransactionConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return false; }
        }

        public SaveTransactionConverter(Type[] types)
        {
            _types = types;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Can't read");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = JToken.FromObject(value);
            token.WriteTo(writer);
        }

        private readonly Type[] _types;
    }

    public class SaveTransactionWrapper
    {
        public SaveTransaction Transaction { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this).ToLower();
        }
    }
}