using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class RemoveUdvParam
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

        public RemoveUdvParam(UdfTargetType targetType, string targetId, string key)
        {
            this.TargetType = targetType;
            this.TargetId = targetId;
            this.Key = key;
        }
        /// <summary>
        /// RemoveUdvParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), targetId=(string), key=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RemoveUdvDocument,
                OperationName = "removeUdv",
                Variables = this
            };
        }


        public static string RemoveUdvDocument = @"
        mutation removeUdv($targetType: UDFTargetType!, $targetId: String!, $key: String!) {
          removeUdv(targetType: $targetType, targetId: $targetId, key: $key) {
            key
            dataType
            value
            label
          }
        }
        ";
    }
}
