using System;
namespace Authing.ApiClient.Domain.Model.Management.UserAction
{
    public class ListUserActionsRealRes
    {
        public int TotalCount { get; set; }

        public UserActionRes[] List { get; set; }
    }
}
