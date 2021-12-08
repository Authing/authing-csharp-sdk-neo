using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class SearchNodesParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        public SearchNodesParam(string keyword)
        {
            this.Keyword = keyword;
        }
        /// <summary>
        /// SearchNodesParam.Request 
        /// <para>Required variables:<br/> { keyword=(string) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = SearchNodesDocument,
                OperationName = "searchNodes",
                Variables = this
            };
        }


        public static string SearchNodesDocument = @"
        query searchNodes($keyword: String!) {
          searchNodes(keyword: $keyword) {
            id
            orgId
            name
            nameI18n
            description
            descriptionI18n
            order
            code
            root
            depth
            path
            codePath
            namePath
            createdAt
            updatedAt
            children
          }
        }
        ";
    }
}
