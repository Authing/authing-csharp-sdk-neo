using System;
namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class TenantInfo
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
    }

    public class ISsoPageCustomizationSettings
    {
        public bool HideIdp { get; set; }
        public bool HideForgetPassword { get; set; }
        public bool AutoRegisterThenLogin { get; set; }
        public bool HideSocialLogin { get; set; }
        public bool HideLoginByPhoneCode { get; set; }
        public bool HideRegister { get; set; }
        public bool HideUserPasswordLogin { get; set; }
        public bool HideWxMpScanLogin { get; set; }
        public bool HideRegisterByPhone { get; set; }
        public bool HideRegisterByEmail { get; set; }
    }

    public class ApplicationPasswordTabConfig
    {
        public string[] EnabledLoginMethods { get; set; }
    }

    public class ExtendsField
    {
        public string type { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string inputType { get; set; }
    }
}
