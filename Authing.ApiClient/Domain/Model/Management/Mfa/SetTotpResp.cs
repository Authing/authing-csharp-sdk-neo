namespace Authing.ApiClient.Domain.Model.Management.Mfa
{
    public class SetTotpResp
    {
        public string UserId { get; set; }
        public bool Enable { get; set; }
        public string secret { get; set; }
        public string AuthenticatorType { get; set; }
        public string RecoveryCode { get; set; }
        public string CreatedAt { get; set; }
        public string UpdateAt { get; set; }
        public string Id { get; set; }
    }
}