namespace Authing.ApiClient.Types
{
    public class ISetTotpRes
    {
        public string UserId { get; set; }

        public bool Enable { get; set; }

        public string Secret { get; set; }

        public string AuthenticatorType { get; set; }

        public string RecoveryCode { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string Id { get; set; }
    }
}