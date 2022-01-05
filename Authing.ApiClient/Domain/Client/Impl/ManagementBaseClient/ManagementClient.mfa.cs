using Authing.ApiClient.Interfaces.ManagementClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Mfa;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        public class MFAManagementClient : IMFAManagementClient
        {
            private ManagementClient client;

            public MFAManagementClient(ManagementClient client)
            {
                this.client = client;
            }

            /// <summary>
            /// 用户是否绑定了 TOTP MFA
            /// </summary>
            /// <param name="userid"></param>
            /// <returns></returns>
            public async Task<UserMfaStatus> GetStatus(string userid)
            {
                var result =
                   await client.RequestCustomData<RestfulResponse<UserMfaStatus>>($"api/v2/users/{userid}/mfa-bound", "",
                        method: HttpMethod.Get, contenttype: ContentType.JSON).ConfigureAwait(false);
                return result.Data.Data;

            }

            /// <summary>
            /// 解绑用户 TOTP MFA
            /// </summary>
            /// <param name="userid"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            public async Task<bool> UnAssociateMfa(string userid, UserMfaType type)
            {
                var result = await client.RequestCustomData<RestfulResponse<bool>>(
                    $"api/v2/users/{userid}/mfa-bound?type={type.ToString()}","", method: HttpMethod.Delete,
                    contenttype: ContentType.JSON).ConfigureAwait(false);
                return result.Data.Code == 200;
            }

            /// <summary>
            /// 设置用户 TOTP 的 secret 和恢复代码，并自动启用 MFA
            /// 将已有的 TOTP 的 secret 和恢复代码导入到 Authing，并为用户开启 TOTP 多因素认证
            /// </summary>
            /// <param name="options">
            /// options.userId 用户 ID
            /// options.secret TOTP 密钥
            /// options.recoveryCode 恢复代码
            /// </param>
            /// <returns></returns>
            public async Task<SetTotpResp> ImportTotp(ImportTotpParams options)
            {
                var result = await client.RequestCustomData<RestfulResponse<SetTotpResp>>("api/v2/mfa/totp/import",
                    new Dictionary<string, string>()
                    {
                        { "userid", options.UserId },
                        { "secret", options.Secret },
                        { "recoverycode", options.RecoveryCode }
                    }.ConvertJson(),
                    contenttype: ContentType.DEFAULT
                ).ConfigureAwait(false);
                return result.Data.Data;
            }

        }
    }
}
