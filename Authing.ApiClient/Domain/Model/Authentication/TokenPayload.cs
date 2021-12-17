using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class TokenPayload
    {
        public string Sub;
        public string Iat;
        public int Exp;
        public UserData Data;
    }
}
