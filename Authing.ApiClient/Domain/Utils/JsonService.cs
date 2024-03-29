﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Utils
{
    public class JsonService : IJsonService
    {
        private JsonSerializerSettings settings;

        private List<JsonConverter> converters;

        private BooleanStringJsonConverter booleanStringJsonConverter;
        private StringEnumConverter stringEnumConverter;
        public JsonService()
        {
            booleanStringJsonConverter = new BooleanStringJsonConverter();
            stringEnumConverter = new StringEnumConverter();

            converters = new List<JsonConverter>();
            converters.Add(new BooleanStringJsonConverter());

            settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Converters = converters
            };
        }

        public string SerializeObject(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, stringEnumConverter);
            return result;
        }

        public T DeserializeObject<T>(string jsonStr)
        {
            T obj = JsonConvert.DeserializeObject<T>(jsonStr, settings);
            return obj;
        }
    }
}
