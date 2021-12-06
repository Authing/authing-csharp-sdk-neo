using System;
using System.Threading.Tasks;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient : BaseManagementClient
    {
        public Action<InitAuthenticationClientOptions> Init { get; }
        public ManagementClient(string userPoolId, string secret) : base(userPoolId, secret)
        {
        }

        public ManagementClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
            Users = new ManagementClient.UsersManagementClient(this);
            Whitelist = new ManagementClient.WhitelistManagementClient(this);
            Init = init ?? throw new ArgumentNullException(nameof(init));
        }

        public static async Task<ManagementClient> InitManagementClient(string userPoolId, string secret)
        {
            var manageClient = new ManagementClient(userPoolId, secret);
            manageClient.Users = new UsersManagementClient(manageClient);
            manageClient.Whitelist = new WhitelistManagementClient(manageClient);
            await manageClient.GetAccessToken();
            return manageClient;
        }


        public static async Task<ManagementClient> InitManagementClient(Action<InitAuthenticationClientOptions> init)
        {
            var manageClient = new ManagementClient(init);
            await manageClient.GetAccessToken();
            manageClient.Users = new ManagementClient.UsersManagementClient(manageClient);
            manageClient.Whitelist = new WhitelistManagementClient(manageClient);
            return manageClient;
        }
    }
}
