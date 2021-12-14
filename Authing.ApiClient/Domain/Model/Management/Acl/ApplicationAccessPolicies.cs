using System;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ApplicationAccessPolicies
    {
        private DateTime AssignedAt { get; set; }
        public bool? InheritByChildren { get; set; }
        public bool Enabled { get; set; }
        public string PolicyId { get; set; }
        public string Code { get; set; }
        public Policy Policy { get; set; }
        public PolicyAssignmentTargetType TargetType { get; set; }
        public string TargetIdentifier { get; set; }
        public string NameSpace { get; set; }
    }
}