using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;
using Authing.ApiClient.Domain.Model.Management.Principal;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IPrincipalManagementClient
    {
        Task<PrincipalDetail> Detail(string userId);
        Task<bool> Authenticate(string userId, PrincipalInput info);
    }
}