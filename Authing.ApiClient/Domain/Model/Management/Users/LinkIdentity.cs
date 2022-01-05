using System;
namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class LinkIdentityOption
    {
        public string UserId { get; set; }
        public string UserIdInIdp { get; set; }
        public bool IsSocial { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
    }

    public class UnlinkIdentityOption
    {
        public string UserId { get; set; }
        public bool IsSocial { get; set; }
        public string Type { get; set; }
        public string Identifier { get; set; }
    }
}
