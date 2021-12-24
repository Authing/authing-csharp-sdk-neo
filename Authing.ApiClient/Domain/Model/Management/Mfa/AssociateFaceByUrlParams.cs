namespace Authing.ApiClient.Domain.Model.Management.Mfa
{
    public class AssociateFaceByUrlParams
    {
        public string BaseFace { get; set; }
        public string CompareFace { get; set; }
        public string MFAToken { get; set; }
    }
}