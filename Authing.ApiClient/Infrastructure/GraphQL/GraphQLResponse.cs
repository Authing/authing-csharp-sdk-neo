using System.Runtime.Serialization;

namespace Authing.ApiClient.Infrastructure.GraphQL
{
    [DataContract]
    public class GraphQLResponse<T>
    {
        [DataMember(Name = "data")]
        public T Data { get; set; }

        [DataMember(Name = "errors")]
        public GraphQLError[] Errors { get; set; }

        [DataMember(Name ="code")]
        public int Code { get; set; }

        [DataMember(Name ="message")]
        public string Message { get; set; }
    }
}
