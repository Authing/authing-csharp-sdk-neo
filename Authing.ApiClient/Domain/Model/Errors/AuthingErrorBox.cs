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

        public void Clear()
        {
            Value = null;
        }
    }
}
