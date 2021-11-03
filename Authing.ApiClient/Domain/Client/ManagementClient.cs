using System;
using System.Threading.Tasks;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client
{
    public partial class ManagementClient : BaseClient
    {
        public Action<InitAuthenticationClientOptions> Init { get; }
        private ManagementClient(string userPoolId, string secret) : base(userPoolId, secret)
        {
        }

        private ManagementClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }
            Init = init;
        }

        public static async Task<ManagementClient> InitManagementClient(string userPoolId, string secret)
        {
            var manageClient = new ManagementClient(userPoolId, secret);
            manageClient.Users = new UsersManagementClient(manageClient);
            await manageClient.GetAccessToken();
            return manageClient;
        }


        public static async Task<ManagementClient> InitManagementClient(Action<InitAuthenticationClientOptions> init)
        {
            var manageClient = new ManagementClient(init);
            await manageClient.GetAccessToken();
            manageClient.Users = new UsersManagementClient(manageClient);
            return manageClient;
        }
    }
}
