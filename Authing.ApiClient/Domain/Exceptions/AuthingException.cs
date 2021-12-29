using System;

namespace Authing.ApiClient.Domain.Exceptions
{
    public class AuthingException : Exception
    {
        /// <summary>
        /// The returned status code
        /// </summary>
        public int StatusCode { get; }
        public object ResultData { get; set; }

        public AuthingException()
        {
        }

        public AuthingException(string message) : base($"The API request failed: {message}")
        {
        }

        public AuthingException(string message, System.Exception innerException) : base($"The API request failed: {message}", innerException)
        {
        }

        public AuthingException(string message, int statusCode) : base($"The API request failed with code {statusCode}: {message}")
        {
            StatusCode = statusCode;
        }

        public AuthingException(string message, int statusCode,object data) : base($"The API request failed with code {statusCode}: {message}")
        {
            StatusCode = statusCode;
            ResultData = data;
        }

    }
}