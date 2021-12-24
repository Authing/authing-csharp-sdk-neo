using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ValidateTokenParams
    {
        public string AccessToken { get; set; }

        public string IdToken { get; set; }
    }
}
