using System;
using System.Net;
using System.Net.Http.Headers;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client.Impl.Client
{
    public static class GraphQLResponseExtensions
    {
        public static GraphQLHttpResponse<T> ToGraphQLHttpResponse<T>(this GraphQLResponse<T> response, HttpResponseHeaders responseHeaders, HttpStatusCode statusCode) => new GraphQLHttpResponse<T>(response, responseHeaders, statusCode);

        /// <summary>
        /// Casts <paramref name="response"/> to <see cref="GraphQLHttpResponse{T}"/>. Throws if the cast fails.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <exception cref="InvalidCastException"><paramref name="response"/> is not a <see cref="GraphQLHttpResponse{T}"/></exception>
        /// <returns></returns>
        public static GraphQLHttpResponse<T> AsGraphQLHttpResponse<T>(this GraphQLResponse<T> response) => (GraphQLHttpResponse<T>)response;
    }
}