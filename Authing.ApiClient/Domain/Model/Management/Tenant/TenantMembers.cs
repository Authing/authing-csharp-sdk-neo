using System;
namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class TenantMembers
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public User User { get; set; }
    }

    public class TenantMembersOption
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }

    public class TenantAddMembersResponse
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string UserPoolId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Css { get; set; }
        public ISsoPageCustomizationSettings SsoPageCustomizationSettings { get; set; }
        public string DefaultLoginTab { get; set; }
        public string DefaultRegisterTab { get; set; }
        public ApplicationPasswordTabConfig PasswordTabConfig { get; set; }
        public string[] LoginTabs { get; set; }
        public string[] RegisterTabs { get; set; }
        public ExtendsField ExtendsFields { get; set; }
        public User[] Users { get; set; }
    }
}
