namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Action
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProgrammaticAccessAccountProps
    {
        public string Id { get; set; }
        public string? Secret { get; set; }
    }
}