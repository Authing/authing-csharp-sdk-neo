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
    public class SetUdfValueBatchParam
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
        [JsonProperty("input")]
        public IEnumerable<SetUdfValueBatchInput> Input { get; set; }

        public SetUdfValueBatchParam(UdfTargetType targetType, IEnumerable<SetUdfValueBatchInput> input)
        {
            this.TargetType = targetType;
            this.Input = input;
        }
        /// <summary>
        /// SetUdfValueBatchParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), input=(SetUdfValueBatchInput[]) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SetUdfValueBatchDocument,
                OperationName = "setUdfValueBatch",
                Variables = this
            };
        }


        public static string SetUdfValueBatchDocument = @"
        mutation setUdfValueBatch($targetType: UDFTargetType!, $input: [SetUdfValueBatchInput!]!) {
          setUdfValueBatch(targetType: $targetType, input: $input) {
            code
            message
          }
        }
        ";
    }
}
