using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Interfaces.AuthenticationClient
{
    public interface IStandardProtocol
    {
        /// <summary>
        /// 拼接 OIDC、OAuth 2.0、SAML、CAS 协议授权链接
        /// </summary>
        /// <param name="option">IProtocolInterface 接口实现类</param>
        /// <returns></returns>
        string BuildAuthorizeUrl(IProtocolInterface option);

        string BuildSamlAuthorizeUrl();

        /// <summary>
        /// 拼接 CAS 协议授权链接
        /// </summary>
        /// <param name="option">CAS 授权类</param>
        /// <returns></returns>
        string BuildCasAuthorizeUrl(CasOption option);

        /// <summary>
        /// 拼接 OAuth 2.0 协议授权链接
        /// </summary>
        /// <param name="option">OAuth 授权类</param>
        /// <returns></returns>
        string BuildOauthAuthorizeUrl(OauthOption option);

        /// <summary>
        /// 拼接 OIDC 协议授权链接
        /// </summary>
        /// <param name="option">OIDC 授权类</param>
        /// <returns></returns>
        string BuildOidcAuthorizeUrl(OidcOption option);

        /// <summary>
        /// CODE 换取 Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CodeToTokenRes> GetAccessTokenByCode(string code);

        /// <summary>
        /// AccessToken 换取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserInfo> GetUserInfoByAccessToken(string token);

        /// <summary>
        /// 使用 Refresh token 获取新的 Access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<RefreshTokenRes> GetNewAccessTokenByRefreshToken(string refreshToken);

        /// <summary>
        /// 检查 Access token 或 Refresh token 的状态
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IntrospectTokenRes> IntrospectToken(string token);

        /// <summary>
        /// 效验Token合法性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ValidateTokenRes> ValidateToken(ValidateTokenParams param);

        /// <summary>
        /// 拼接登出 URL
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        string BuildLogoutUrl(LogoutParams options);

        string BuildOidcLogoutUrl(LogoutParams options);
        string BuildEasyLogoutUrl(LogoutParams options);
        string BuildCasLogoutUrl(LogoutParams options);

        /// <summary>
        /// Client Credentials 模式获取 Access Token
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAccessTokenByClientCredentials(string scope, GetAccessTokenByClientCredentialsOption options = null);

        /// <summary>
        /// 撤回 Access token 或 Refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<GraphQLResponse<string>> RevokeToken(string token);
    }
}