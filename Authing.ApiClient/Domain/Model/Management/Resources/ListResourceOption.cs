using System;
using Authing.ApiClient.Types;
using System.Collections.Generic;
namespace Authing.ApiClient.Domain.Model.Management.Resources
{
    public class ListResourceOption
    {
        public int? Page { get; set; } = 1;

        public int? Limit { get; set; } = 10;

        public ResourceType? Type { get; set; }
    }
}
