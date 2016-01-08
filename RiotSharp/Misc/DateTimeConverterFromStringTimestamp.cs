﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RiotSharp
{
    class DateTimeConverterFromStringTimestamp : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(long).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Value<string>() != null)
            {
                return long.Parse(token.Value<string>()).ToDateTimeFromMilliSeconds();
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((DateTime)value).ToLong());
        }
    }
}
