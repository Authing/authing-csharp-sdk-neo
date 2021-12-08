using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class SetUdvBatchParam
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
        /// Optional
        /// </summary>
        [JsonProperty("udvList")]
        public IEnumerable<UserDefinedDataInput> UdvList { get; set; }

        public SetUdvBatchParam(UdfTargetType targetType, string targetId)
        {
            this.TargetType = targetType;
            this.TargetId = targetId;
        }
        /// <summary>
        /// SetUdvBatchParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), targetId=(string) }</para>
        /// <para>Optional variables:<br/> { udvList=(UserDefinedDataInput[]) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SetUdvBatchDocument,
                OperationName = "setUdvBatch",
                Variables = this
            };
        }


        public static string SetUdvBatchDocument = @"
        mutation setUdvBatch($targetType: UDFTargetType!, $targetId: String!, $udvList: [UserDefinedDataInput!]) {
          setUdvBatch(targetType: $targetType, targetId: $targetId, udvList: $udvList) {
            key
            dataType
            value
            label
          }
        }
        ";
    }
}
