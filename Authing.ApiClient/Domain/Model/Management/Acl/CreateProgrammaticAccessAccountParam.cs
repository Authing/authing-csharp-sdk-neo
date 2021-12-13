namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class CreateProgrammaticAccessAccountParam
    {
        public string Remarks { get; set; } = "";
        public int Token_lifetime { get; set; } = 600;

        public string AppId { get; set; }
    }
}