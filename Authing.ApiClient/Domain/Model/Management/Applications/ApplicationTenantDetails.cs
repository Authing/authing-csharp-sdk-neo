using System;
namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class ApplicationTenantDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Protocol { get; set; }
        public string IsIntegrate { get; set; }
        public TenantInfo[] Tenants { get; set; }
    }

    public class TenantInfo
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string UserPoolId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public object Css { get; set; }
        public object SsoPageCustomizationSettings { get; set; }
        public string DefaultLoginTab { get; set; }
        public string DefaultRegisterTab { get; set; }
        public object PasswordTabConfig { get; set; }
        public string[] LoginTabs { get; set; }
        public string[] RegisterTabs { get; set; }
        public object ExtendsFields { get; set; }
    }
}
