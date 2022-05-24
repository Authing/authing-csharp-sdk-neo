using System;
using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ResourcesBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserPoolId { get; set; }
        public string Code { get; set; }
        public IEnumerable<Action> Actions { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int NamespaceId { get; set; }
        public object ApiIdentifier { get; set; }
        public string Namespace { get; set; }
    }
}