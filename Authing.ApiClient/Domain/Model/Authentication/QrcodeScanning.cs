using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class QrcodeScanning
    {
        public bool Redirect { get; set; }
        public int interval { get; set; }
    }
}
