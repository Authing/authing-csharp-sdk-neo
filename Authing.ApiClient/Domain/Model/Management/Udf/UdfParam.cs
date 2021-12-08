using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class UdfParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UdfTargetType TargetType { get; set; }

        public UdfParam(UdfTargetType targetType)
        {
            this.TargetType = targetType;
        }
        /// <summary>
        /// UdfParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UdfDocument,
                OperationName = "udf",
                Variables = this
            };
        }


        public static string UdfDocument = @"
        query udf($targetType: UDFTargetType!) {
          udf(targetType: $targetType) {
            targetType
            dataType
            key
            label
            options
          }
        }
        ";
    }
}
