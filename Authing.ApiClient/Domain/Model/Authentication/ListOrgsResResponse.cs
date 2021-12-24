using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class ListOrgsResResponse
    {
        public List<List<List<Model.Management.Orgs.Node>>> Nodes { get; set; }
    }
}
