using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Principal;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        public class PrincipalManagementClient : IPrincipalManagementClient
        {
            private ManagementClient _client;

            public PrincipalManagementClient(ManagementClient client)
            {
                _client = client;
            }

            public async Task<PrincipalDetail> Detail(string userId)
            {
                var result = await _client.RequestCustomData<RestfulResponse<PrincipalDetail>>(
                    $"api/v2/users/{userId}/management/principal_authentication", "", method: HttpMethod.Delete,
                    contenttype: ContentType.JSON);
                return result.Data.Data;
            }

            public async Task<bool> Authenticate(string userId, PrincipalInput info)
            {
                var url = $"api/v2/users/{userId}/management/principal_authentication";
                Dictionary<string, string> param = new Dictionary<string, string>();
                if (info.Type == "P")
                {
                    param.Add("type","P");
                    param.Add("name",info.Name);
                    param.Add("idCard",info.IdCard);
                    param.Add("ext",info.BankCard);
                }
                else
                {
                    param.Add("type", "E");
                    param.Add("name", info.EnterpriseName);
                    param.Add("idCard", info.EnterpriseCode);
                    param.Add("ext", info.LegalPersonName);
                }

                var result = await _client.RequestCustomData<RestfulResponse<bool>>(url, param.ConvertJson());
                return result.Data.Data;
            }
        }
    }
}
