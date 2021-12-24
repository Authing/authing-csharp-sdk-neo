namespace Authing.ApiClient.Domain.Model.Management.Mfa
{
    public class PhoneOrEmailBindableParams
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string MfaToken { get; set; }
    }
}