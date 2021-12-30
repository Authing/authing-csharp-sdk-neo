using Authing.ApiClient.Extensions;
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
        public string BuildCasAuthorizeUrl(CasOption option)
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
        public string BuildOauthAuthorizeUrl(OauthOption option)
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
        public string BuildOidcAuthorizeUrl(OidcOption option)
        {
            var prompt = "";
            if (option?.Scope?.IndexOf("offline_access") != -1)
            {
                prompt = "consent";
            }

            var res = new
            {
                client_id = option?.AppId ?? Options.AppId,
                scope = "openid profile email phone address",
                state = option?.State ?? AuthingUtils.GenerateRandomString(12),
                nonce = option?.Nonce ?? AuthingUtils.GenerateRandomString(12),
                response_mode = option?.ResponseMode?.ToString().ToLower(),
                response_type = !(option?.ResponseType is null) ? option.ResponseType.ToString().ToLower() : "code",
                redirect_uri = option?.RedirectUri ?? Options.RedirectUri,
                prompt = prompt.Contains("offline_access") ? "consent" : "",
                //code_challenge_method = option.CodeChallengeMethod?.ToString().ToLower(),
                //code_challenge = option.CodeChallenge,
            }.Convert2QueryParams();
            return $"{Options.Host ?? Host}/login{res}";
        }

        /// <summary>
        /// CODE 换取 Token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<CodeToTokenRes> GetAccessTokenByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(Options.Secret) &&
                Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new ArgumentException("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            var url = Options.Protocol == Protocol.OAUTH ? $"oauth/token" : $"oidc/token";
            GraphQLResponse<CodeToTokenRes> result;
            switch (Options.TokenEndPointAuthMethod)
            {
                case TokenEndPointAuthMethod.CLIENT_SECRET_POST:
                    result = await RequestCustomData<CodeToTokenRes>(url, new Dictionary<string, string>()
                    {
                        { "client_id", Options.AppId },
                        { "client_secret", Options.Secret },
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", Options.RedirectUri }
                    }.ConvertJson());
                    return result.Data;
                case TokenEndPointAuthMethod.CLIENT_SECRET_BASIC:
                    result = await RequestCustomData<CodeToTokenRes>(url, new Dictionary<string, string>()
                        {
                            { "grant_type", "authorization_code" },
                            { "code", code },
                            { "redirect_uri", Options.RedirectUri },
                        }.ConvertJson(),
                        new Dictionary<string, string>()
                        {
                            {
                                "Authorization",
                                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}"
                            }
                        });
                    return result.Data;
                case TokenEndPointAuthMethod.NONE:
                    result = await RequestCustomData<CodeToTokenRes>(url, new Dictionary<string, string>()
                    {
                        { "client_id", Options.AppId },
                        //{ "client_secret", Options.Secret },
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", Options.RedirectUri },
                    }.ConvertJson());
                    //result = await Post<CodeToTokenRes>(url, new Dictionary<string, string>()
                    //{
                    //    { "client_id", Options.AppId },
                    //    { "grant_type", "authorization_code" },
                    //    { "code", code },
                    //    { "redirect_uri", Options.RedirectUri },
                    //}, null);
                    return result.Data;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// AccessToken 换取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetUserInfoByAccessToken(string token)
        {
            var endPoint = Options.Protocol == Protocol.OAUTH
                ? "oauth/me?access_token={token}"
                : $"oidc/me?access_token={token}";
            //var res = await Post<UserInfo>(endPoint, new Dictionary<string, string>() { { "Authorization", $"Bearer {token}" } });
            GraphQLResponse<UserInfo> res;
            switch (Options.Protocol)
            {
                case Protocol.OAUTH:
                    res = await Post<UserInfo>(endPoint, new GraphQLRequest());
                    break;
                case Protocol.OIDC:
                case Protocol.SAML:
                case Protocol.CAS:
                    res = await Get<UserInfo>(endPoint, new GraphQLRequest());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return res.Data;
        }

        /// <summary>
        /// 使用 Refresh token 获取新的 Access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetNewAccessTokenByRefreshToken(string refreshToken)
        {
            // TODO: 注意返回类型的转换
            var _ = Options?.Protocol switch
            {
                Protocol.OIDC => "oidc/token",
                Protocol.OAUTH => "oauth/token",
                _ => throw new ArgumentException("初始化 AuthenticationClient 时传入的 protocol 参数必须为 oauth 或 oidc，请检查参数")
            };
            if (Options?.Secret != null && Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new ArgumentException("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_POST)
            {
                return await GetNewAccessTokenByRefreshTokenWithClientSecretPost(refreshToken);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_BASIC)
            {
                return await GetNewAccessTokenByRefreshTokenWithClientSecretBasic(refreshToken);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.NONE)
            {
                return await GetNewAccessTokenByRefreshTokenWithNone(refreshToken);
            }

            throw new ArgumentException("请检查参数 TokenEndPointAuthMethod");

        }

        private async Task<HttpResponseMessage> GetNewAccessTokenByRefreshTokenWithNone(string refreshToken)
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

            var result = await Post<HttpResponseMessage>(api, param, new Dictionary<string, string>());

            return result.Data;
        }

        private async Task<HttpResponseMessage> GetNewAccessTokenByRefreshTokenWithClientSecretBasic(string refreshToken)
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
            var result = await Post<HttpResponseMessage>(api, param, new Dictionary<string, string>());

            return result.Data;
        }

        private  async Task<HttpResponseMessage> GetNewAccessTokenByRefreshTokenWithClientSecretPost(string refreshToken)
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
            var result = await Post<HttpResponseMessage>(api, param, new Dictionary<string, string>());
            return result.Data;
        }

        /// <summary>
        /// 检查 Access token 或 Refresh token 的状态
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> IntrospectToken(string token)
        {
            var api = Options?.Protocol switch
            {
                Protocol.OIDC => "oidc/token",
                Protocol.OAUTH => "oauth/token",
                _ => throw new Exception("初始化 AuthenticationClient 时传入的 protocol 参数必须为 oauth 或 oidc，请检查参数")
            };
            if (Options?.Secret != null && Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new Exception("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_POST)
            {
                return await IntrospectTokenWithClientSecretPost(api, token);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.CLIENT_SECRET_BASIC)
            {
                return await IntrospectTokenWithClientSecretBasic(api, token);
            }

            if (Options.TokenEndPointAuthMethod == TokenEndPointAuthMethod.NONE)
            {
                return await IntrospectTokenWithNone(api, token);
            }

            throw new Exception(
                "初始化 AuthenticationClient 时传入的 revocationEndPointAuthMethod 参数可选值为 client_secret_base、client_secret_post、none，请检查参数");

        }

        private async Task<HttpResponseMessage> IntrospectTokenWithClientSecretPost(string url, string token)
        {
            var result = await RequestCustomData<HttpResponseMessage>(url, new Dictionary<string, string>()
            {
                { "client_id", Options.AppId },
                { "client_secret", Options.Secret },
                { "grant_type", "authorization_code" },
                { "token", token },
            }.ConvertJson());
            return result.Data;
        }

        private async Task<HttpResponseMessage> IntrospectTokenWithClientSecretBasic(string url, string token)
        {
            var result = await RequestCustomData<HttpResponseMessage>(url, new Dictionary<string, string>()
                {
                    { "token", token },
                }.ConvertJson(),
                new Dictionary<string, string>()
                {
                    {
                        "Authorization",
                        $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}"
                    }
                });
            return result.Data;
        }

        private async Task<HttpResponseMessage> IntrospectTokenWithNone(string url, string token)
        {
            var result = await RequestCustomData<HttpResponseMessage>(url, new Dictionary<string, string>()
            {
                { "client_id", Options.AppId },
                { "token", token },
            }.ConvertJson());
            return result.Data;
        }

        /// <summary>
        /// 效验Token合法性
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ValidateToken(ValidateTokenParams param)
        {
            if (string.IsNullOrWhiteSpace(param.AccessToken) && string.IsNullOrWhiteSpace(param.IdToken))
                throw new AggregateException("请在传入的参数对象中包含 accessToken 或 idToken 属性");
            if (param.AccessToken.Length > 0 && param.IdToken.Length > 0)
                throw new ArgumentException("accessToken 和 idToken 只能传入一个，不能同时传入");

            var url = $"api/v2/oidc/validate_token";
            url += !string.IsNullOrWhiteSpace(param.AccessToken) ? $"?access_token={param.AccessToken}" : $"?id_token={param.IdToken}";

            var result = await RequestCustomData<HttpResponseMessage>(url, "", null, HttpMethod.Get, ContentType.JSON);
            return result.Data;
        }

        /// <summary>
        /// 拼接登出 URL
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public string BuildLogoutUrl(LogoutParams options)
        {
            switch (Options.Protocol)
            {
                case Protocol.OIDC:
                    return BuildCasLogoutUrl(options);
                case Protocol.OAUTH:
                    if (options.Expert != null)
                        return BuildOidcLogoutUrl(options);
                    return BuildEasyLogoutUrl(options);
                case Protocol.SAML:
                case Protocol.CAS:
                    return BuildEasyLogoutUrl(options);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string BuildOidcLogoutUrl(LogoutParams options)
        {
            return string.IsNullOrWhiteSpace(options.RedirectUri)
                ? $"{Host}/login/profile/logout?redirect_uri={options.RedirectUri}"
                : $"{Host}/login/profile/logout";
        }

        public string BuildEasyLogoutUrl(LogoutParams options)
        {
            if (string.IsNullOrWhiteSpace(options.RedirectUri) && string.IsNullOrWhiteSpace(options.IdToken) ||
                !string.IsNullOrWhiteSpace(options.RedirectUri) || !string.IsNullOrWhiteSpace(options.IdToken))
                throw new ArgumentException("必须同时传入 idToken 和 redirectUri 参数，或者同时都不传入");
            return string.IsNullOrWhiteSpace(options.RedirectUri)
                ? $"{Host}/oidc/session/end?id_token_hint={options.IdToken}&post_logout_redirect_uri={options.RedirectUri}"
                : $"{Host}/oidc/session/end";
        }

        public string BuildCasLogoutUrl(LogoutParams options)
        {
            return string.IsNullOrWhiteSpace(options.RedirectUri)
                ? $"{Host}/cas-idp/logout"
                : $"{Host}/cas-idp/logout?url={options.RedirectUri}";
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
            var result = await RequestCustomData<HttpResponseMessage>(
                "oidc/token",
                new Dictionary<string, string>()
                {
                    {"client_id",$"{options.AccessKey}"},
                    {"client_secret",$"{options.AccessSecret}"},
                    {"grant_type","client_credentials"},
                    {"scope",scope}
                }.ConvertJson());
            return result.Data;
        }

        /// <summary>
        /// 撤回 Access token 或 Refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RevokeToken(string token)
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
                    return await RevokeTokenWithNone(url, token);
                case TokenEndPointAuthMethod.CLIENT_SECRET_POST:
                    return await RevokeTokenWithClientSecretPost(url, token);
                case TokenEndPointAuthMethod.CLIENT_SECRET_BASIC:
                    return await RevokeTokenWithClientSecretBasic(url, token);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<HttpResponseMessage> RevokeTokenWithClientSecretPost(string url, string token)
        {
            var result = await RequestCustomData<HttpResponseMessage>(
                url,
                new Dictionary<string, string>()
                {
                    {"token",token},
                    {"client_id",Options.AppId},
                    {"client_secret",Options.Secret}
                }.ConvertJson());
            return result.Data;
        }

        public async Task<HttpResponseMessage> RevokeTokenWithClientSecretBasic(string url, string token)
        {
            if (Options.Protocol == Protocol.OAUTH)
                throw new ArgumentException("OAuth 2.0 暂不支持用 client_secret_basic 模式身份验证撤回 Token");
            var result = await RequestCustomData<HttpResponseMessage>(
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
                });
            return result.Data;
        }

        public async Task<HttpResponseMessage> RevokeTokenWithNone(string url, string token)
        {
            var result = await RequestCustomData<HttpResponseMessage>(
                url,
                new Dictionary<string, string>()
                {
                    {"token",token},
                    {"client_id",Options.AppId}
                }.ConvertJson());
            return result.Data;
        }

        /// <summary>
        /// 检验 CAS 1.0 Ticket 合法性
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ValidateTicketV1Res> ValidateTicketV1(string ticket, string service)
        {
            var result = await Get<ValidateTicketV1Response>($"cas-idp/${AppId}/validate/?ticket={ticket}&service={service}", null);

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
        public async Task<string> GetCodeChallengeDigest(CodeChallengeDigestOption options)
        {
            if (options.CodeChallenge == null)
            {
                throw new Exception("请提供 options.codeChallenge，值为一个长度大于等于 43 的字符串");
            }
            if (options.Method == CodeChallengeDigestMethod.S256)
            {
                string result = EncryptHelper.SHA256Hash(options.CodeChallenge);
                return result.Replace('+', '-').Replace('/', '_').Replace("=",string.Empty);
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
        public async Task<string> ValidateTicketV2(string ticket, string service,ValidateTicketFormat validateTicketFormat)
        {
            var result = await Get<ValidateTicketV2Response>($"cas-idp/{AppId}/serviceValidate/?ticket={ticket}&service={service}&format={validateTicketFormat}", null);

            return result.Data.Result;
        }

        public async Task<User> TrackSession()
        {
            var result = await Get<User>("cas/session", null);
            return result.Data;
        }
    }
}
