using System.Net;
using System.Net.Http.Headers;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client.Impl.Client
{
    public class GraphQLHttpResponse<T> : GraphQLResponse<T>
    {
        public GraphQLHttpResponse(GraphQLResponse<T> response, HttpResponseHeaders responseHeaders, HttpStatusCode statusCode)
        {
            Data = response.Data;
            Errors = response.Errors;
            ResponseHeaders = responseHeaders;
            StatusCode = statusCode;
        }

        public HttpResponseHeaders ResponseHeaders { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
