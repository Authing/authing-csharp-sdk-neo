using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ValidateTicketV1Res
    {
        public bool Valid { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }

    }
}
