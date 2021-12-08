using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Authing.ApiClient.Domain.Model.Management.Roles
{
    public class UdfValueBatchParam
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
        [JsonProperty("targetIds")]
        public IEnumerable<string> TargetIds { get; set; }

        public UdfValueBatchParam(UdfTargetType targetType, IEnumerable<string> targetIds)
        {
            this.TargetType = targetType;
            this.TargetIds = targetIds;
        }
        /// <summary>
        /// UdfValueBatchParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), targetIds=(string[]) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UdfValueBatchDocument,
                OperationName = "udfValueBatch",
                Variables = this
            };
        }


        public static string UdfValueBatchDocument = @"
        query udfValueBatch($targetType: UDFTargetType!, $targetIds: [String!]!) {
          udfValueBatch(targetType: $targetType, targetIds: $targetIds) {
            targetId
            data {
              key
              dataType
              value
              label
            }
          }
        }
        ";
    }
}
