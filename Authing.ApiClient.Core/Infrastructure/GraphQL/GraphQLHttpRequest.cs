using Newtonsoft.Json;

namespace Authing.ApiClient.Core.Infrastructure.GraphQL
{
    public class GraphQLHttpRequest : GraphQLRequest
    {
        public GraphQLHttpRequest()
        {
        }

        public GraphQLHttpRequest(string query, object variables = null, string operationName = null) : base(query,
            variables, operationName)
        {
        }

        public GraphQLHttpRequest(GraphQLRequest other) : base(other.Query, other.Variables, other.OperationName)
        {
        }

        public virtual string ToHttpRequestBody()
        {
            return JsonConvert.SerializeObject(
                this,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}