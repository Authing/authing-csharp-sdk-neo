namespace Authing.ApiClient.Types
{
    public class LogoutParams
    {
        public bool? Expert { get; set; } = null;
#nullable enable
        public string? RedirectUri { get; set; }
        public string? IdToken { get; set; }
#nullable disable
    }
}