namespace Authing.ApiClient.Domain.Model.Management.Groups
{
    public class ListUsersOption
    {
        public bool WithCustomData { get; set; } = false;

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}