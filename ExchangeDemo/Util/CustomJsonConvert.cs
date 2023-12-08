using System.Text.Json;
using System.Text.Json.Serialization;
using ExchangeDemo.POCO;

namespace ExchangeDemo.Util
{
    public class CustomJsonConvert : JsonConverter<List<Rate>>
    {
        public override List<Rate> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            List<Rate> rate = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return rate;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    rate.Add(new());
                    rate[rate.Count - 1].Name = reader.GetString();
                    reader.Read();
                    rate[rate.Count - 1].ActualRate = reader.GetDecimal();
                }
            }

            return rate;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(List<Rate>).IsAssignableFrom(typeToConvert);
        }

        public override void Write(Utf8JsonWriter writer, List<Rate> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
