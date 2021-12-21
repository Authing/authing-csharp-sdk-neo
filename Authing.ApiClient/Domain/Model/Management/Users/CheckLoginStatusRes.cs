using System;
using Authing.ApiClient.Domain.Model.Management.Applications;
namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class CheckLoginStatusRes
    {
        public bool IsLogin { get; set; }
        public User User { get; set; }
        public Application[] Application { get; set; }
    }
}
