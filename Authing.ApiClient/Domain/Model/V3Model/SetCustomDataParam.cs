using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.Library.Domain.Model.V3Model
{
    public class SetCustomDataParam
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TargetType targetType { get; set; }
        public string targetIdentifier { get; set; }
        [JsonProperty("namespace")]
        public string _namespace { get; set; }
        public List<Dic> list { get; set; }
    }

    public class Dic
    {
        public string key { get; set; }
        public object value { get; set; }
    }

    public enum TargetType
    {
        USER,
        ROLE,
        GROUP,
        DEPARTMENT
    }

}
