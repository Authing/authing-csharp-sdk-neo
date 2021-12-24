using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Client
{
    public interface IAuthingClient
    {
        Task<TResponse> SendRequest<TRequest, TResponse>(string url, HttpType httpType, TRequest body, Dictionary<string, string> headers);
        //Task<GraphQLResponse<TResponse>> Request<TRequest, TResponse>(string url, string method, TRequest body, Dictionary<string, string> headers);
        Task<TResponse> SendRequest<TRequest, TResponse>(string url, HttpType httpType, Dictionary<string,string> body, Dictionary<string, string> headers);

        Task<TResponse> PostRaw<TResponse>(string url, string serializedata, Dictionary<string, string> headers);

        Task<TResponse> RequestCustomData<TResponse>(string url, string serializedata, Dictionary<string, string> headers = null!, HttpMethod method = null!, ContentType contenttype = ContentType.DEFAULT);

    }
}
