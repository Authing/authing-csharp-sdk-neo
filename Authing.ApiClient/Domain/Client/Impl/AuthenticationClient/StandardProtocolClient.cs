using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Client.Impl.Client;
using Authing.ApiClient.Types;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Infrastructure.GraphQL;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    public partial class AuthenticationClient : BaseAuthenticationClient
    {
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

        private string BuildSamlAuthorizeUrl()
        {
            return $"{Host}/api/v2/saml-idp/{AppId}";
        }

        private string BuildCasAuthorizeUrl(CasOption option)
        {
            return option.Service is null
                ? $"{Host}/cas-idp/{AppId}"
                : $"{Host}/cas-idp/{AppId}?service={option.Service}";
        }

        private string BuildOauthAuthorizeUrl(OauthOption option)
        {
            var rd = new Random();
            var param = new
            {
                state = option.State ?? rd.Next(10, 99).ToString(),
                scope = option.Scope ?? "user",
                client_id = option.AppId ?? Options.AppId,
                redirect_uri = option.RedirectUri ?? Options.RedirectUri,
                response_type = option.ResponseType.ToString().ToLower() ?? "code",
            }.Convert2QueryParams();
            return $"{Host}/oauth/auth{param}";
        }

        private string BuildOidcAuthorizeUrl(OidcOption option)
        {
            var prompt = "";
            if (option?.Scope?.IndexOf("offline_access") != -1)
            {
                prompt = "consent";
            }

            var rd = new Random();
            var res = new
            {
                nonce = option.Nonce ?? rd.Next(10, 99).ToString(),
                state = option.State ?? rd.Next(10, 99).ToString(),
                scope = "openid profile email phone address",
                client_id = option.AppId ?? Options.AppId,
                redirect_uri = option.RedirectUri ?? Options.RedirectUri,
                response_type = !(option.ResponseType is null) ? option.ResponseType.ToString().ToLower() : "code",
                code_challenge_method = option.CodeChallengeMethod?.ToString().ToLower(),
                code_challenge = option.CodeChallenge,
                response_mode = option.ResponseMode?.ToString().ToLower(),
                prompt = string.IsNullOrEmpty(prompt) ? null : prompt,
            }.Convert2QueryParams();
            return $"{Options.Host ?? Host}/oidc/auth{res}";
        }

        public async Task<CodeToTokenRes> GetAccessTokenByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(Secret) && Options.TokenEndPointAuthMethod != TokenEndPointAuthMethod.NONE)
            {
                throw new ArgumentException("请在初始化 AuthenticationClient 时传入 appId 和 secret 参数");
            }

            var url = Options.Protocol == Protocol.OAUTH ? $"oauth/token" : $"oidc/token";
            GraphQLResponse<CodeToTokenRes> result;
            switch (Options.TokenEndPointAuthMethod)
            {
                case TokenEndPointAuthMethod.NONE:
                    result = await Post<CodeToTokenRes>(url, new Dictionary<string, string>()
                        {
                            { "client_id", Options.AppId },
                            { "client_secret", Options.Secret },
                            { "grant_type", "authorization_code" },
                            { "code", code },
                            { "redirect_uri", Options.RedirectUri }
                        });
                    return result.Data;
                case TokenEndPointAuthMethod.CLIENT_SECRET_POST:
                    result = await Post<CodeToTokenRes>(url, new Dictionary<string, string>()
                        {
                            { "grant_type", "authorization_code" },
                            { "code", code },
                            { "redirect_uri", Options.RedirectUri },
                        },
                        new Dictionary<string, string>()
                        {
                            { "Content-Type", "application/x-www-form-urlencoded" },
                            {
                                "Authorization",
                                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.AppId}:{Options.Secret}"))}"
                            }
                        });
                    return result.Data;
                case TokenEndPointAuthMethod.CLIENT_SECRET_BASIC:
                    result = await Post<CodeToTokenRes>(url, new Dictionary<string, string>()
                        {
                            { "grant_type", "authorization_code" },
                            { "code", code },
                            { "redirect_uri", Options.RedirectUri },
                        },
                        new Dictionary<string, string>() { { "Content-Type", "application/x-www-form-urlencoded" } });
                    return result.Data;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<UserInfo> GetUserInfoByAccessToken(string token)
        {
            var endPoint = Options.Protocol == Protocol.OAUTH ? "oauth/me" : "oidc/me";
            var res = await Post<UserInfo>(endPoint, new Dictionary<string, string>() { { "Authorization", $"Bearer {token}" } });
            return res.Data;
        }
    }
}
