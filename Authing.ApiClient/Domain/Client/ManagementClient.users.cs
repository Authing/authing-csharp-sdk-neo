using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;

namespace Authing.ApiClient.Domain.Client
{
    public partial class ManagementClient
    {
        public UsersManagementClient Users { get; private set; }
        public class UsersManagementClient
        {
            private readonly ManagementClient client;
            public UsersManagementClient(ManagementClient client)
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
