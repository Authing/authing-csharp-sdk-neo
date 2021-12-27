using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Mfa;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IMFAManagementClient
    {
        /// <summary>
        /// 用户是否绑定了 TOTP MFA
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<UserMfaStatus> GetStatus(string userid);

        /// <summary>
        /// 解绑用户 TOTP MFA
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> UnAssociateMfa(string userid, UserMfaType type);

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
        Task<SetTotpResp> ImportTotp(ImportTotpParams options);
    }
}
