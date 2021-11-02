using System;
using System.Threading.Tasks;
using Authing.ApiClient.Client;
using Authing.ApiClient.Core.Domain.Client;
using Authing.ApiClient.Core.Types;

namespace Authing.ApiClient.Mgmt
{
    public class ManagementClient
    {
        public static async Task<ManagementClientCore> InitManagementClient(string userPoolId, string secret)
        {
            var client = AuthingClient.Of();
            var manageClient = await ManagementClientCore.InitManagementClient(userPoolId, secret, client);
            return manageClient;
        }


        public static async Task<ManagementClientCore> InitManagementClient(Action<InitAuthenticationClientOptions> init)
        {
            var client = AuthingClient.Of();
            var manageClient = await ManagementClientCore.InitManagementClient(init, client);
            return manageClient;
        }
    }
}
