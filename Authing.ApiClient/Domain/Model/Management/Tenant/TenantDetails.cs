using System;
using Authing.ApiClient.Domain.Model.Management.Applications;
namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class TenantDetails
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
        public Application[] Apps { get; set; }
    }
}
