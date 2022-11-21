using Authing.ApiClient.Infrastructure.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model.Exceptions
{
    public class AuthingErrorBox
    {
        public GraphQLError[] Value { get;private set; }

        public AuthingErrorBox()
        {

        }

        public void Set(GraphQLError[] ex)
        {
            Value = ex;
        }

        public void Set(long statusCode, string message, long apiCode)
        {
            Value = new GraphQLError[1];

            Value[0].Message = new GraphQLErrorMessage() 
            {
                Code=statusCode,
                Message=message,
                ApiCode=apiCode,
            };
        }

        public void Clear()
        {
            Value = null;
        }
    }
}
