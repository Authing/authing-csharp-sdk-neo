using Authing.ApiClient.Core.Model;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Udf
{
    public class SetUdfParam
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

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("dataType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UdfDataType DataType { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        [JsonProperty("options")]
        public string Options { get; set; }

        public SetUdfParam(UdfTargetType targetType, string key, UdfDataType dataType, string label)
        {
            this.TargetType = targetType;
            this.Key = key;
            this.DataType = dataType;
            this.Label = label;
        }
        /// <summary>
        /// SetUdfParam.Request 
        /// <para>Required variables:<br/> { targetType=(UDFTargetType), key=(string), dataType=(UDFDataType), label=(string) }</para>
        /// <para>Optional variables:<br/> { options=(string) }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SetUdfDocument,
                OperationName = "setUdf",
                Variables = this
            };
        }


        public static string SetUdfDocument = @"
        mutation setUdf($targetType: UDFTargetType!, $key: String!, $dataType: UDFDataType!, $label: String!, $options: String) {
          setUdf(targetType: $targetType, key: $key, dataType: $dataType, label: $label, options: $options) {
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
