namespace Authing.ApiClient.Domain.Model.Management.Mfa
{
    public class ImportTotpParams
    {
        public string  UserId { get; set; }
        public string Secret { get; set; }
        public string RecoveryCode { get; set; }
    }
}