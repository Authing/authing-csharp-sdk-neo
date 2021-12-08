using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class UdvParam
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

        public UdvParam(UdfTargetType targetType, string targetId)
        {
            this.TargetType = targetType;
            this.TargetId = targetId;
        }
        /// <summary>
        /// UdvParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), targetId=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UdvDocument,
                OperationName = "udv",
                Variables = this
            };
        }


        public static string UdvDocument = @"
        query udv($targetType: UDFTargetType!, $targetId: String!) {
          udv(targetType: $targetType, targetId: $targetId) {
            key
            dataType
            value
            label
          }
        }
        ";
    }

}
