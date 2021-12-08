using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class RemoveUdfParam
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
        [JsonProperty("key")]
        public string Key { get; set; }

        public RemoveUdfParam(UdfTargetType targetType, string key)
        {
            this.TargetType = targetType;
            this.Key = key;
        }
        /// <summary>
        /// RemoveUdfParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), key=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = RemoveUdfDocument,
                OperationName = "removeUdf",
                Variables = this
            };
        }


        public static string RemoveUdfDocument = @"
        mutation removeUdf($targetType: UDFTargetType!, $key: String!) {
          removeUdf(targetType: $targetType, key: $key) {
            message
            code
          }
        }
        ";
    }
}
