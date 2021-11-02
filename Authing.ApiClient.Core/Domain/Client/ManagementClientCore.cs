using System;
using System.Threading.Tasks;
using Authing.ApiClient.Core.Types;

namespace Authing.ApiClient.Core.Domain.Client
{
    public partial class ManagementClientCore : BaseClient
    {
        public Action<InitAuthenticationClientOptions> Init { get; }
        private ManagementClientCore(string userPoolId, string secret, IAuthingClient client) : base(userPoolId, secret, client)
        {
        }

        private ManagementClientCore(Action<InitAuthenticationClientOptions> init, IAuthingClient client) : base(init, client)
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }
            Init = init;
        }

        public static async Task<ManagementClientCore> InitManagementClient(string userPoolId, string secret, IAuthingClient client)
        {
            var manageClient = new ManagementClientCore(userPoolId, secret, client);
            manageClient.Users = new UsersManagementClient(manageClient);
            await manageClient.GetAccessToken();
            return manageClient;
        }


        public static async Task<ManagementClientCore> InitManagementClient(Action<InitAuthenticationClientOptions> init, IAuthingClient client)
        {
            var manageClient = new ManagementClientCore(init, client);
            await manageClient.GetAccessToken();
            manageClient.Users = new UsersManagementClient(manageClient);
            return manageClient;
        }
    }
}
