using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Core.Domain.Model;

namespace Authing.ApiClient.Core.Domain.Client
{
    public partial class ManagementClientCore
    {
        public UsersManagementClient Users { get; private set; }
        public class UsersManagementClient
        {
            private readonly ManagementClientCore client;
            public UsersManagementClient(ManagementClientCore client)
            {
                this.client = client;
            }
        }
        
        public async Task<User> Detail(
            string userId,
            bool withCustomData = false)
        {
            if (withCustomData)
            {
                var _param = new UserWithCustomDataParam()
                {
                    Id = userId
                };
                var _res = await Post<UserWithCustomDataResponse>(_param.CreateRequest());
                return _res.Result;
            }
            var param = new UserParam() { Id = userId };
            var res = await Post<UserResponse>(param.CreateRequest());
            return res.Result;
        }
    }
}
