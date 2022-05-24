namespace Authing.ApiClient.Types
{
    public class AssosicateMfaAuthenticatorRes
    {
        public string AuthenticatorType { get; set; }

        public string Secret { get; set; }

        public string QrCodeUri { get; set; }

        public string QrCodeDataUrl { get; set; }

        public string RecoveryCode { get; set; }
    }
}