using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class CreateGroupParam
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public CreateGroupParam(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        public CreateGroupParam()
        {

        }

        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = createGroupDocument,
                OperationName = "createGroup",
                Variables = this
            };
        }

        public string createGroupDocument = @"
        mutation createGroup($code: String!, $name: String!, $description: String) {
            createGroup(code: $code, name: $name, description: $description) {
                code
                name
                description
                createdAt
                updatedAt
            }
        }
        ";
    }
}
