using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
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
            
            public async Task<User> Detail(
                string userId,
                bool withCustomData = false)
            {
                if (withCustomData)
                {
                    var _param = new UserWithCustomDataParam
                    {
                        Id = userId
                    };
                    var _res = await client.Post<GraphQLResponse<UserWithCustomDataResponse>>(_param.CreateRequest());
                    return _res.Data.Result;
                }
                var param = new UserParam { Id = userId };
                var res = await client.Post<GraphQLResponse<UserResponse>>(param.CreateRequest());
                return res.Data.Result;
            }
        }
        
       
    }
}
