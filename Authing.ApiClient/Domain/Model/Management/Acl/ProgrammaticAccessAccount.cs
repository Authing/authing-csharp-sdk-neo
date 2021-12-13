namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class ProgrammaticAccessAccount
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string Remarks { get; set; }
        public int TokenLifetime { get; set; }
        public bool Enabled { get; set; }
        public string UserId { get; set; }
    }
}