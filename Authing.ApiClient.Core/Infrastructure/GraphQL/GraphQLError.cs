using System.Runtime.Serialization;

namespace Authing.ApiClient.Core.Infrastructure.GraphQL
{
    /// <summary>
    /// Represents a GraphQL Error of a GraphQL Query
    /// </summary>
    public class GraphQLError
    {
        /// <summary>
        /// The message object of the error
        /// </summary>
        [DataMember(Name = "message")]
        public GraphQLErrorMessage Message { get; set; }
    }
}
