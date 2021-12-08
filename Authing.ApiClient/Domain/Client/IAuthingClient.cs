using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client
{
    public interface IAuthingClient
    {
        Task<TResponse> SendRequest<TRequest, TResponse>(string url, string method, TRequest body, Dictionary<string, string> headers);
        //Task<GraphQLResponse<TResponse>> Request<TRequest, TResponse>(string url, string method, TRequest body, Dictionary<string, string> headers);
    }
}
