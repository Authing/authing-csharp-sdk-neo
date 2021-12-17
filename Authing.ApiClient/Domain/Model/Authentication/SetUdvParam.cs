using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class SetUdvParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UdfTargetType TargetType { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        public SetUdvParam(UdfTargetType targetType, string targetId, string key, string value)
        {
            this.TargetType = targetType;
            this.TargetId = targetId;
            this.Key = key;
            this.Value = value;
        }
        /// <summary>
        /// SetUdvParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), targetId=(string), key=(string), value=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SetUdvDocument,
                OperationName = "setUdv",
                Variables = this
            };
        }


        public static string SetUdvDocument = @"
        mutation setUdv($targetType: UDFTargetType!, $targetId: String!, $key: String!, $value: String!) {
          setUdv(targetType: $targetType, targetId: $targetId, key: $key, value: $value) {
            key
            dataType
            value
            label
          }
        }
        ";
    }
}
