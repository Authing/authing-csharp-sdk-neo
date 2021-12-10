using System;
namespace Authing.ApiClient.Types
{
    public enum BatchFetchUserTypes
    {
        ID,
        USERNAME,
        PHONE,
        EMAIL,
        EXTERNALID,
    }

    public class ExistsOption
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? ExternalId { get; set; }
    }
}
