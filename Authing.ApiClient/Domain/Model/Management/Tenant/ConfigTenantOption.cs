using System;
namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
    public class ConfigTenantOption
    {
        public string Css { get; set; }
        public SsoPageCustomizationSettings SsoPageCustomizationSettings { get; set; }


    }

    public class SsoPageCustomizationSettings
    {
        public bool AutoRegisterThenLogin { get; set; }
        public bool HideForgetPassword { get; set; }
        public bool HideIdp { get; set; }
        public bool HideSocialLogin { get; set; }
    }
}
