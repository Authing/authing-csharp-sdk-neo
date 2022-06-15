using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Types
{
    public enum ContentType
    {
        [Description("application/x-www-form-urlencoded")]
        DEFAULT,

        [Description("application/json")]
        JSON,
    }
}