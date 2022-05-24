using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
    public class ExpnadAllRequest
    {
        public GraphQLRequest CreateRequest()
        {

            return new GraphQLRequest
            {
                Query = "",
                OperationName = "org",
                Variables = this
            };
        }
    }
}