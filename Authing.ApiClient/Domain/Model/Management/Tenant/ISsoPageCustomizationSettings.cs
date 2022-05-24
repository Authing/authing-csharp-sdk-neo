namespace Authing.ApiClient.Domain.Model.Management.Tenant
{
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
}