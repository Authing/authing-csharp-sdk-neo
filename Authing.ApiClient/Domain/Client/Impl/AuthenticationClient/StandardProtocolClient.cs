﻿using Authing.ApiClient.Extensions;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Interfaces.AuthenticationClient;
using Authing.ApiClient.Domain.Model;
using System.Linq;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public partial class AuthenticationClient : BaseAuthenticationClient, IStandardProtocol
    {
        /// <summary>
        /// 拼接 OIDC、OAuth 2.0、SAML、CAS 协议授权链接
        /// </summary>
        /// <param name="option">IProtocolInterface 接口实现类</param>
        /// <returns></returns>
        public string BuildAuthorizeUrl(IProtocolInterface option)
        {
            if (Host == null)
            {
                throw new ArgumentException("请在初始化 AuthenticationClient 时传入应用域名 Host 参数，形如：https://app1.authing.cn");
            }

            if (option as OidcOption != null)
            {
                return BuildOidcAuthorizeUrl(option as OidcOption);
            }

            if (option as OauthOption != null)
            {
                return BuildOauthAuthorizeUrl(option as OauthOption);
            }

            if (option as CasOption != null)
            {
                return BuildCasAuthorizeUrl(option as CasOption);
            }

            if (option as SamlOption != null)
            {
                return BuildSamlAuthorizeUrl();
            }

            throw new ArgumentException("接口型实现必须是 OidcOption, OauthOption, CasOption 其中一种");

        }

        public string BuildSamlAuthorizeUrl()
        {
            return $"{Host}/api/v2/saml-idp/{AppId}";
        }

        /// <summary>
        /// 拼接 CAS 协议授权链接
        /// </summary>
        /// <param name="option">CAS 授权类</param>
        /// <returns></returns>
        private string BuildCasAuthorizeUrl(CasOption option)
        {
            return option.Service is null
                ? $"{Host}/cas-idp/{AppId}"
                : $"{Host}/cas-idp/{AppId}?service={option.Service}";
        }

        /// <summary>
        /// 拼接 OAuth 2.0 协议授权链接
        /// </summary>
        /// <param name="option">OAuth 授权类</param>
        /// <returns></returns>
        private string BuildOauthAuthorizeUrl(OauthOption option)
        {
            var rd = new Random();
            var param = new
            {
                state = option.State ?? rd.Next(10, 99).ToString(),
                scope = option.Scope ?? "user",
                client_id = option.AppId ?? Options.AppId,
                redirect_uri = option.RedirectUri ?? Options.RedirectUri,
                response_type = !(option.ResponseType is null) ? option.ResponseType.ToString().ToLower() : "code",
            }.Convert2QueryParams();
            return $"{Host}/oauth/auth{param}";
        }

        /// <summary>
        /// 拼接 OIDC 协议授权链接
        /// </summary>
        /// <param name="option">OIDC 授权类</param>
        /// <returns></returns>
        private string BuildOidcAuthorizeUrl(OidcOption option)
        {
            //var prompt = "";
            //if (option?.Scope?.IndexOf("offline_access") != -1)
            //{
            //    prompt = "consent";
            //}
            option.Scope ??= "openid profile email phone address";
            var res = new
            {
                client_id = option?.AppId ?? Options.AppId,
                scope = $"{option.Scope}",
                state = option?.State ?? AuthingUtils.GenerateRandomString(12),
                nonce = option?.Nonce ?? AuthingUtils.GenerateRandomString(12),
                response_mode = option?.ResponseMode?.ToString().ToLower(),
                response_type = !(option?.ResponseType is null) ? option.ResponseType.ToString().ToLower() : "code",
                redirect_uri = option?.RedirectUri ?? Options.RedirectUri,
                prompt = option.Scope.Contains("offline_access") ? "consent" : "",
                //code_challenge_method = option.CodeChallengeMethod?.ToString().ToLower(),
                //code_challenge = option.CodeChallenge,
            }.Convert2QueryParams();
            return $"{Options.Host ?? Host}/oidc/auth{res}";
        }

        /// <summary>
        /// CODE 换取 Token
        /// </summary>
        /// <param name="code"> 网页回调返回的 CODE </param>
        /// <returns></returns>
        public async Task<CodeToTokenRes> GetAccessTokenByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(Options.Secret) &&
                Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new ArgumentException("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            var url = Options.Protocol == Protocol.OAUTH ? $"oauth/token" : $"oidc/token";
            CodeToTokenRes result;
            switch (Options.TokenEndPointAuthMethod)
            {
                case TokenEndPointAuthMethod.CLIENT_SECRET_POST:
                    result = await RequestNoGraphQlResponse<CodeToTokenRes>(url, new Dictionary<string, string>()
                    {
                        { "client_id", Options.AppId },
                        { "client_secret", Options.Secret },
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", Options.RedirectUri },
                    }.ConvertJson()).ConfigureAwait(false);
                    return result;
                case TokenEndPointAuthMethod.CLIENT_SECRET_BASIC:
                    var headers = GetAuthHeaders();
                    headers.Add("Authorization",
                        $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}");
                    result = await RequestNoGraphQlResponse<CodeToTokenRes>(url, new Dictionary<string, string>()
                        {
                            { "grant_type", "authorization_code" },
                            { "code", code },
                            { "redirect_uri", Options.RedirectUri },
                        }.ConvertJson(),
                        headers
                            //new Dictionary<string, string>()
                            //{
                            //    {
                            //        "Authorization",
                            //        $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}"
                            //    }
                            //}
                            ).ConfigureAwait(false);
                    return result;
                case TokenEndPointAuthMethod.NONE:
                    result = await RequestNoGraphQlResponse<CodeToTokenRes>(url, new Dictionary<string, string>()
                    {
                        { "client_id", Options.AppId },
                        //{ "client_secret", Options.Secret },
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", Options.RedirectUri },
                    }.ConvertJson()).ConfigureAwait(false);
                    return result;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// AccessToken 换取用户信息
        /// </summary>
        /// <param name="token"> 用户的AccessToken </param>
        /// <returns></returns>
        public async Task<UserInfo> GetUserInfoByAccessToken(string token)
        {
            var endPoint = Options.Protocol == Protocol.OAUTH
                ? $"oauth/me?access_token={token}"
                : $"oidc/me?access_token={token}";
            UserInfo res;
            switch (Options.Protocol)
            {
                case Protocol.OAUTH:
                    res = await RequestNoGraphQlResponse<UserInfo>(endPoint).ConfigureAwait(false);
                    break;
                case Protocol.OIDC:
                case Protocol.SAML:
                case Protocol.CAS:
                    res = await RequestNoGraphQlResponse<UserInfo>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return res;
        }

        /// <summary>
        /// 使用 Refresh token 获取新的 Access token
        /// </summary>
        /// <param name="refreshToken">用户的 RefreshToken</param>
        /// <returns></returns>
        public async Task<RefreshTokenRes> GetNewAccessTokenByRefreshToken(string refreshToken)
        {
            // TODO: 注意返回类型的转换
            var _ = Options?.Protocol switch
            {
                Protocol.OIDC => "oidc/token",
                Protocol.OAUTH => "oauth/token",
                _ => throw new ArgumentException("初始化 AuthenticationClient 时传入的 protocol 参数必须为 oauth 或 oidc，请检查参数")
            };
            if (Options?.Secret == null && Options?.AppId == null && Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new ArgumentException("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_POST)
            {
                return await GetNewAccessTokenByRefreshTokenWithClientSecretPost(refreshToken).ConfigureAwait(false);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_BASIC)
            {
                return await GetNewAccessTokenByRefreshTokenWithClientSecretBasic(refreshToken).ConfigureAwait(false);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.NONE)
            {
                return await GetNewAccessTokenByRefreshTokenWithNone(refreshToken).ConfigureAwait(false);
            }

            throw new ArgumentException("请检查参数 TokenEndPointAuthMethod");

        }

        private async Task<RefreshTokenRes> GetNewAccessTokenByRefreshTokenWithNone(string refreshToken)
        {
            var api = Options.Protocol switch
            {
                Protocol.OIDC => "oidc/token",
                Protocol.OAUTH => "oauth/token",
                _ => throw new ArgumentOutOfRangeException()
            };

            var param = new Dictionary<string, string>()
            {
                { "client_id", $"{Options.AppId}" },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            var result = await RequestNoGraphQlResponse<RefreshTokenRes>(api, param.ConvertJson()).ConfigureAwait(false);

            return result;
        }

        private async Task<RefreshTokenRes> GetNewAccessTokenByRefreshTokenWithClientSecretBasic(string refreshToken)
        {
            var api = Options.Protocol switch
            {
                Protocol.OIDC => "oidc/token",
                Protocol.OAUTH => "oauth/token",
                _ => throw new ArgumentOutOfRangeException()
            };

            var param = new Dictionary<string, string>()
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };
            var result = await RequestNoGraphQlResponse<RefreshTokenRes>(api, param.ConvertJson()).ConfigureAwait(false);

            return result;
        }

        private async Task<RefreshTokenRes> GetNewAccessTokenByRefreshTokenWithClientSecretPost(string refreshToken)
        {
            var api = Options.Protocol switch
            {
                Protocol.OIDC => "oidc/token",
                Protocol.OAUTH => "oauth/token",
                _ => throw new ArgumentOutOfRangeException()
            };

            var param = new Dictionary<string, string>()
            {
                { "client_id", $"{Options.AppId}" },
                { "client_secret", $"{Options.Secret}" },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken },

            };
            var result = await RequestNoGraphQlResponse<RefreshTokenRes>(api, param.ConvertJson()).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// 检查 Access token 或 Refresh token 的状态
        /// </summary>
        /// <param name="token">用户的 Refresh token 或 Access token</param>
        /// <returns></returns>
        public async Task<IntrospectTokenRes> IntrospectToken(string token)
        {
            var api = Options?.Protocol switch
            {
                Protocol.OIDC => "oidc/token/introspection",
                Protocol.OAUTH => "oauth/token/introspection",
                _ => throw new Exception("初始化 AuthenticationClient 时传入的 protocol 参数必须为 oauth 或 oidc，请检查参数")
            };
            if (Options?.Secret == null && Options?.AppId == null && Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new Exception("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_POST)
            {
                return await IntrospectTokenWithClientSecretPost(api, token).ConfigureAwait(false);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_BASIC)
            {
                return await IntrospectTokenWithClientSecretBasic(api, token).ConfigureAwait(false);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.NONE)
            {
                return await IntrospectTokenWithNone(api, token).ConfigureAwait(false);
            }

            throw new Exception(
                "初始化 AuthenticationClient 时传入的 revocationEndPointAuthMethod 参数可选值为 client_secret_base、client_secret_post、none，请检查参数");

        }

        private async Task<IntrospectTokenRes> IntrospectTokenWithClientSecretPost(string url, string token)
        {
            var result = await RequestNoGraphQlResponse<IntrospectTokenRes>(url, new Dictionary<string, string>()
            {
                { "client_id", Options.AppId },
                { "client_secret", Options.Secret },
                { "token", token },
            }.ConvertJson()).ConfigureAwait(false);
            return result;
        }

        private async Task<IntrospectTokenRes> IntrospectTokenWithClientSecretBasic(string url, string token)
        {
            var result = await RequestNoGraphQlResponse<IntrospectTokenRes>(url, new Dictionary<string, string>()
                {
                    { "token", token },
                }.ConvertJson(),
                new Dictionary<string, string>()
                {
                    {
                        "Authorization",
                        $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}"
                    }
                }).ConfigureAwait(false);
            return result;
        }

        private async Task<IntrospectTokenRes> IntrospectTokenWithNone(string url, string token)
        {
            var result = await RequestNoGraphQlResponse<IntrospectTokenRes>(url, new Dictionary<string, string>()
            {
                { "client_id", Options.AppId },
                { "token", token },
            }.ConvertJson()).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// 校验Token合法性
        /// </summary>
        /// <param name="param">校验内容</param>
        /// <returns></returns>
        public async Task<ValidateTokenRes> ValidateToken(ValidateTokenParams param)
        {
            if (string.IsNullOrWhiteSpace(param.AccessToken) && string.IsNullOrWhiteSpace(param.IdToken))
                throw new AggregateException("请在传入的参数对象中包含 accessToken 或 idToken 属性");
            if (param.AccessToken?.Length > 0 && param.IdToken?.Length > 0)
                throw new ArgumentException("accessToken 和 idToken 只能传入一个，不能同时传入");

            var url = $"api/v2/oidc/validate_token";
            url += !string.IsNullOrWhiteSpace(param.AccessToken) ? $"?access_token={param.AccessToken}" : $"?id_token={param.IdToken}";

            var result = await RequestNoGraphQlResponse<ValidateTokenRes>(url, "", null, HttpMethod.Get, ContentType.JSON).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// 拼接登出 URL
        /// </summary>
        /// <param name="options">登出参数</param>
        /// <returns></returns>
        public string BuildLogoutUrl(LogoutParams options)
        {
            switch (Options.Protocol)
            {
                case Protocol.CAS:
                    return BuildCasLogoutUrl(options);
                case Protocol.OIDC:
                    if (options.Expert != null)
                        return BuildOidcLogoutUrl(options);
                    return BuildEasyLogoutUrl(options);
                case Protocol.SAML:
                case Protocol.OAUTH:
                    return BuildEasyLogoutUrl(options);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string BuildEasyLogoutUrl(LogoutParams options)
        {
            return string.IsNullOrWhiteSpace(options.RedirectUri)
                ? $"{Host}/login/profile/logout"
                : $"{Host}/login/profile/logout?redirect_uri={options.RedirectUri}";
        }

        private string BuildOidcLogoutUrl(LogoutParams options)
        {
            if (string.IsNullOrWhiteSpace(options.RedirectUri) && string.IsNullOrWhiteSpace(options.IdToken) ||
                string.IsNullOrWhiteSpace(options.RedirectUri) || string.IsNullOrWhiteSpace(options.IdToken))
                throw new ArgumentException("必须同时传入 idToken 和 redirectUri 参数，或者同时都不传入");
            return string.IsNullOrWhiteSpace(options.RedirectUri)
                ? $"{Host}/oidc/session/end"
                : $"{Host}/oidc/session/end?id_token_hint={options.IdToken}&post_logout_redirect_uri={options.RedirectUri}";
        }

        private string BuildCasLogoutUrl(LogoutParams options)
        {
            return string.IsNullOrWhiteSpace(options.RedirectUri)
                ? $"{Host}/cas-idp/{AppId}/logout"
                : $"{Host}/cas-idp/{AppId}/logout?url={options.RedirectUri}";
        }

        /// <summary>
        /// Client Credentials 模式获取 Access Token
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAccessTokenByClientCredentials(string scope, GetAccessTokenByClientCredentialsOption options = null)
        {
            if (string.IsNullOrWhiteSpace(scope))
                throw new ArgumentException(
                    "请传入 scope 参数，请看文档：https://docs.authing.cn/v2/guides/authorization/m2m-authz.html");
            if (options is null)
                throw new ArgumentException(
                    "请在调用本方法时传入 { accessKey: string, accessSecret: string }，请看文档：https://docs.authing.cn/v2/guides/authorization/m2m-authz.html");
            var result = await RequestCustomDataWithOutToken<HttpResponseMessage>(
                "oidc/token",
                new Dictionary<string, string>()
                {
                    {"client_id",$"{options.AccessKey}"},
                    {"client_secret",$"{options.AccessSecret}"},
                    {"grant_type","client_credentials"},
                    {"scope",scope}
                }.ConvertJson()).ConfigureAwait(false);
            return result.Data;
        }

        /// <summary>
        /// 撤回 Access token 或 Refresh token
        /// </summary>
        /// <param name="token">用户的 Access token 或 Refresh token</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<string>> RevokeToken(string token)
        {
            if (Options.Protocol != Protocol.OAUTH && Options.Protocol != Protocol.OIDC)
                throw new ArgumentException(
                    "初始化 AuthenticationClient 时传入的 protocol 参数必须为 ProtocolEnum.OAUTH 或 ProtocolEnum.OIDC，请检查参数");
            if (string.IsNullOrWhiteSpace(Options.Secret) &&
                Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.NONE)
                throw new AggregateException("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");

            var url = Options.Protocol == Protocol.OIDC ? "oidc/token/revocation" : "oauth/token/revocation";

            switch (Options.TokenEndPointAuthMethod)
            {
                case TokenEndPointAuthMethod.NONE:
                    return await RevokeTokenWithNone(url, token).ConfigureAwait(false);
                case TokenEndPointAuthMethod.CLIENT_SECRET_POST:
                    return await RevokeTokenWithClientSecretPost(url, token).ConfigureAwait(false);
                case TokenEndPointAuthMethod.CLIENT_SECRET_BASIC:
                    return await RevokeTokenWithClientSecretBasic(url, token).ConfigureAwait(false);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<GraphQLResponse<string>> RevokeTokenWithClientSecretPost(string url, string token)
        {
            var result = await RequestCustomDataWithOutToken<string>(
                url,
                new Dictionary<string, string>()
                {
                    {"token",token},
                    {"client_id",Options.AppId},
                    {"client_secret",Options.Secret}
                }.ConvertJson()).ConfigureAwait(false);
            return result;
        }

        private async Task<GraphQLResponse<string>> RevokeTokenWithClientSecretBasic(string url, string token)
        {
            if (Options.Protocol == Protocol.OAUTH)
                throw new ArgumentException("OAuth 2.0 暂不支持用 client_secret_basic 模式身份验证撤回 Token");
            var result = await RequestCustomDataWithOutToken<string>(
                "oidc/token/revocation",
                new Dictionary<string, string>()
                {
                    {"token",token}
                }.ConvertJson(), new Dictionary<string, string>()
                {
                    {
                        "Authorization",
                        $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}"
                    }
                }).ConfigureAwait(false);
            return result;
        }

        private async Task<GraphQLResponse<string>> RevokeTokenWithNone(string url, string token)
        {
            var result = await RequestCustomDataWithOutToken<string>(
                url,
                new Dictionary<string, string>()
                {
                    {"token",token},
                    {"client_id",Options.AppId}
                }.ConvertJson()).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// 检验 CAS 1.0 Ticket 合法性
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ValidateTicketV1Res> ValidateTicketV1(string ticket, string service)
        {
            var result = await RequestCustomDataWithOutToken<ValidateTicketV1Response>($"cas-idp/${AppId}/validate/?ticket={ticket}&service={service}", method: HttpMethod.Get).ConfigureAwait(false);

            if (result.Data.Result.Split('\n').Contains("yes"))
            {
                return new ValidateTicketV1Res() { Valid = true };
            }
            else
            {
                return new ValidateTicketV1Res() { Valid = false, Message = "ticket 不合格" };
            }

        }

        /// <summary>
        /// 获取 codechallengedigest
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public string GetCodeChallengeDigest(CodeChallengeDigestOption options)
        {
            if (options.CodeChallenge == null)
            {
                throw new Exception("请提供 options.codeChallenge，值为一个长度大于等于 43 的字符串");
            }
            if (options.Method == CodeChallengeDigestMethod.S256)
            {
                string result = EncryptHelper.SHA256Hash(options.CodeChallenge);
                return result.Replace('+', '-').Replace('/', '_').Replace("=", string.Empty);
            }
            if (options.Method == CodeChallengeDigestMethod.PLAIN)
            {
                return options.CodeChallenge;
            }
            throw new Exception("不支持的 options.method，可选值为 S256、plain");
        }

        /// <summary>
        /// 生成 codechallenge
        /// </summary>
        /// <returns>codechallenge</returns>
        public string GenerateCodeChallenge()
        {
            return AuthingUtils.GenerateRandomString(43);
        }

        /// <summary>
        /// 通过远端服务验证票据合法性
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="service"></param>
        /// <param name="validateTicketFormat"></param>
        /// <returns></returns>
        public async Task<string> ValidateTicketV2(string ticket, string service, ValidateTicketFormat validateTicketFormat)
        {
            var result = await RequestCustomDataWithOutToken<ValidateTicketV2Response>($"cas-idp/{AppId}/serviceValidate/?ticket={ticket}&service={service}&format={validateTicketFormat}", method: HttpMethod.Get).ConfigureAwait(false);

            return result.Data.Result;
        }

        public async Task<User> TrackSession()
        {
            var result = await RequestCustomDataWithOutToken<User>("cas/session", method: HttpMethod.Get).ConfigureAwait(false);
            return result.Data;
        }
    }
}
