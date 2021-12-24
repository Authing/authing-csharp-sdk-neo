namespace Authing.ApiClient.Domain.Model.Management.Mfa
{
    public class VerifyTotpRecoveryCodeParams
    {
        public string RecoveryCode { get; set; }

        public string MfaToken { get; set; }

        public bool IsExternal { get; set; }
    }
}