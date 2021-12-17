using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ApplicationList
    {
        public int TotalCount { get; set; }
        public Application[] List { get; set; }
    }
}
