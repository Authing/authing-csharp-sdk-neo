namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class SearchOption
    {
        public string[]? Fields { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 10;

        public SearchUserDepartmentOpt[] DepartmentOpts { get; set; }

        // public IEnumerable<SearchUserDepartmentOpt>? DepartmentOpts { get; set; }
        public SearchUserGroupOpt[] GroupOpts { get; set; }

        public SearchUserRoleOpt[] RoleOpts { get; set; }

        public bool WithCustomData { get; set; }


    }

    #region SearchUserDepartmentOpt

    #endregion

    #region SearchUserGroupOpt

    #endregion

    #region SearchUserRoleOpt

    #endregion
}
