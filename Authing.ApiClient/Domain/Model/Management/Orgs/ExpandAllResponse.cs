﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Management.Orgs
{
   public class ExpandAllResponse
    {
        [JsonProperty("")]
        public List<Org> Orgs { get; set; }
    }
}
