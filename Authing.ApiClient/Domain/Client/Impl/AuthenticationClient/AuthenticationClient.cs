using System;
using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Newtonsoft.Json;
using System.Collections.Generic;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Domain.Model.Management.Department;
using System.Text.RegularExpressions;
using Authing.ApiClient.Domain.Model.Management.Users;
using System.Linq;
using System.Net.Http;
using Authing.ApiClient.Domain.Model.GraphQLParam;
using Authing.ApiClient.Domain.Model.GraphQLResponse;
using Authing.ApiClient.Interfaces.AuthenticationClient;
using System.Text;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Utils;
using Authing.Library.Domain.Model;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    /// <summary>
    /// Authing 认证客户端类
    /// </summary>
    public partial class AuthenticationClient : BaseAuthenticationClient, IStandardProtocol, IAuthenticationClient
    {
        /// <summary>
        /// 通过应用 ID 初始化
        /// </summary>
        /// <param name="appId">应用 ID</param>
        public AuthenticationClient(string appId) : base(appId)
        {
        }

        /// <summary>
        /// 通过委托完成初始化
        /// </summary>
        /// <param name="init">配置参数</param>
        public AuthenticationClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
        }

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                AccessToken = value?.Token ?? AccessToken;
            }
        }

        private User user;

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <returns>当前用户 ID</returns>
        public string CheckLoggedIn()
        {
            if (user != null)
            {
                return user.Id;
            }
            if (string.IsNullOrEmpty(AccessToken))
            {
                throw new Exception("请先登录!");
            }

            //先判断才用哪种
            var header = AccessToken.Split('.');
            JWKS jwks = null;
            var headerDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Base64Url.Decode(header[0])));
            if (headerDic.ContainsKey("alg"))
            {
                if (headerDic["alg"].ToString() == "RS256")
                {
                    var payLoadDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Base64Url.Decode(header[1])));
                    if (payLoadDic.ContainsKey("iss"))
                    {
                        //jwks = client.SendRequest<string, JWKS>(payLoadDic["iss"].ToString() + "/.well-known/jwks.json", HttpType.Get, "", null).Result;
                        jwks = RequestNoGraphQlResponseWithHost<JWKS>(payLoadDic["iss"].ToString(), ".well-known/jwks.json",
                            method: HttpMethod.Get).Result;
                    }
                }
            }

            var tokenInfo = AuthingUtils.GetPayloadByToken(AccessToken, PublicKey, Secret, jwks);
            var userDataString = tokenInfo.ContainsKey("data") ? tokenInfo["data"] : "";
            var userData = JsonConvert.DeserializeObject<UserData>(userDataString.ToString() ?? "");
            var userId = tokenInfo.ContainsKey("sub") ? tokenInfo["sub"].ToString() : userData.Id;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("不合法的 accessToken");
            }
            return userId;
        }

        /// <summary>
        /// 设置当前用户信息
        /// </summary>
        /// <param name="user">用户数据</param>
        public void SetCurrentUser(User user)
        {
            User = user;
        }

        /// <summary>
        /// 设置当前 AccessToken
        /// </summary>
        /// <param name="token">token 值</param>
        public void SetToken(string token)
        {
            user = null;
            AccessToken = token;
            CheckLoggedIn();
        }

        [Obsolete("该函数已弃用，请使用　GetCurrentUser")]
        public async Task<User> CurrentUser(string accessToken = null)
        {
            var param = new UserParam();
            Dictionary<string, string> header = new Dictionary<string, string>();
            var preprocessedRequest = new GraphQLHttpRequest(param.CreateRequest());
            if (string.IsNullOrWhiteSpace(accessToken))
                header = new Dictionary<string, string>() { { "Authorization", "Bearer " + accessToken } };
            var res = await RequestCustomDataWithToken<UserResponse>(GraphQLEndpoint, preprocessedRequest.ToHttpRequestBody(),
                header, contenttype: ContentType.JSON).ConfigureAwait(false);
            if (res != null)
            {
                user = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        [Obsolete("该方法已弃用")]
        public async Task<User> RegisterByEmail(string email, string password, RegisterProfile profile = null,
                                                bool forceLogin = false, bool generateToken = false)
        {
            var param = new RegisterByEmailParam(
                new RegisterByEmailInput(email, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    Profile = profile,
                    ForceLogin = forceLogin,
                    GenerateToken = generateToken,
                }
            );

            var res = await RequestCustomDataWithOutToken<RegisterByEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过邮箱注册用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户信息</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken">请求令牌</param>
        /// <returns>注册的用户</returns>
        public async Task<User> RegisterByEmail(string email,
                                                string password,
                                                RegisterProfile profile = null,
                                                RegisterAndLoginOptions registerAndLoginOptions = null,
                                                AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }
            var param = new RegisterByEmailParam(
                new RegisterByEmailInput(email, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    Profile = profile,
                    ForceLogin = registerAndLoginOptions?.ForceLogin,
                    GenerateToken = registerAndLoginOptions?.GenerateToken,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<RegisterByEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        [Obsolete("此方法已弃用")]
        public async Task<User> RegisterByUsername(string username,
                                                   string password,
                                                   RegisterProfile profile = null,
                                                   bool forceLogin = false,
                                                   bool generateToken = false)
        {
            var param = new RegisterByUsernameParam(
                new RegisterByUsernameInput(username, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    Profile = profile,
                    ForceLogin = forceLogin,
                    GenerateToken = generateToken,
                }
            );

            var res = await RequestCustomDataWithOutToken<RegisterByUsernameResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过用户名注册用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户信息</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>注册的用户</returns>
        public async Task<User> RegisterByUsername(string username,
                                                   string password,
                                                   RegisterProfile profile = null,
                                                   RegisterAndLoginOptions registerAndLoginOptions = null,
                                                   AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }

            var param = new RegisterByUsernameParam(
                new RegisterByUsernameInput(username, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    Profile = profile,
                    ForceLogin = registerAndLoginOptions?.ForceLogin,
                    GenerateToken = registerAndLoginOptions?.GenerateToken,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithOutToken<RegisterByUsernameResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过手机号注册
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">手机号验证码</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户资料</param>
        /// <param name="forceLogin">强制登录</param>
        /// <param name="generateToken">自动生成 token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        [Obsolete("此方法已弃用")]
        public async Task<User> RegisterByPhoneCode(string phone,
                                                    string code,
                                                    string password = null,
                                                    RegisterProfile profile = null,
                                                    bool forceLogin = false,
                                                    bool generateToken = false)
        {
            var param = new RegisterByPhoneCodeParam(
                new RegisterByPhoneCodeInput(phone, code)
                {
                    Password = EncryptHelper.RsaEncryptWithPublic(password, PublicKey),
                    Profile = profile,
                    ForceLogin = forceLogin,
                    GenerateToken = generateToken,
                }
            );

            var res = await RequestCustomDataWithOutToken<RegisterByPhoneCodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过手机验证码注册用户
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户信息</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>注册的用户</returns>
        public async Task<User> RegisterByPhoneCode(string phone,
                                                    string code,
                                                    string password = null,
                                                    RegisterProfile profile = null,
                                                    RegisterAndLoginOptions registerAndLoginOptions = null,
                                                    AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }

            var param = new RegisterByPhoneCodeParam(
                new RegisterByPhoneCodeInput(phone, code)
                {
                    Password = password is null ? null : EncryptHelper.RsaEncryptWithPublic(password, PublicKey),
                    Profile = profile,
                    ForceLogin = registerAndLoginOptions?.ForceLogin,
                    GenerateToken = registerAndLoginOptions?.GenerateToken,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithOutToken<RegisterByPhoneCodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 检查密码强度
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CheckPasswordStrengthResult</returns>
        public async Task<CheckPasswordStrengthResult> CheckPasswordStrength(string password, AuthingErrorBox authingErrorBox = null)
        {
            var param = new CheckPasswordStrengthParam(password);
            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<CheckPasswordStrengthResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            return res.Data?.Result;
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO: 破坏性更新
        public async Task<CommonMessage> SendSmsCode(string phone, AuthingErrorBox authingErrorBox = null)
        {
            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<CommonMessage>("api/v2/sms/send", new Dictionary<string, object>
            {
                {nameof(phone), phone }
            }.ConvertJson()).ConfigureAwait(false);

            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            CommonMessage ms = new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message,
            };
            return ms;
        }

        [Obsolete("此方法已弃用")]
        public async Task<User> LoginByEmail(string email,
                                             string password,
                                             bool autoRegister = false,
                                             string captchaCode = null)
        {
            var param = new LoginByEmailParam(
                new LoginByEmailInput(email, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    AutoRegister = autoRegister,
                    CaptchaCode = captchaCode,
                }
            );

            var res = await RequestCustomDataWithOutToken<LoginByEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过邮箱登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByEmail(string email,
                                             string password,
                                             RegisterAndLoginOptions registerAndLoginOptions = null,
                                             AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }

            var param = new LoginByEmailParam(
                new LoginByEmailInput(email, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    AutoRegister = registerAndLoginOptions?.AutoRegister ?? false,
                    CaptchaCode = registerAndLoginOptions?.CaptchaCode,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<LoginByEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过用户名登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="captchaCode">人机验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        [Obsolete("此方法已弃用")]
        public async Task<User> LoginByUsername(string username,
                                                string password,
                                                bool autoRegister = false,
                                                string captchaCode = null)
        {
            var param = new LoginByUsernameParam(
                new LoginByUsernameInput(username, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    AutoRegister = autoRegister,
                    CaptchaCode = captchaCode,
                }
            );

            var res = await RequestCustomDataWithOutToken<LoginByUsernameResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过用户名登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByUsername(string username,
                                                string password,
                                                RegisterAndLoginOptions registerAndLoginOptions = null,
                                                AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }

            var param = new LoginByUsernameParam(
                new LoginByUsernameInput(username, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    AutoRegister = registerAndLoginOptions?.AutoRegister ?? false,
                    CaptchaCode = registerAndLoginOptions?.CaptchaCode,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<LoginByUsernameResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }


        /// <summary>
        /// 通过手机号验证码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        [Obsolete("此方法已弃用")]
        public async Task<User> LoginByPhoneCode(string phone,
                                                 string code,
                                                 bool autoRegister = false)
        {
            var param = new LoginByPhoneCodeParam(
                new LoginByPhoneCodeInput(phone, code)
                {
                    AutoRegister = autoRegister,
                }
            );

            var res = await RequestCustomDataWithOutToken<LoginByPhoneCodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过手机验证码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="registerAndLoginOptions">登录配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByPhoneCode(string phone,
                                                 string code,
                                                 RegisterAndLoginOptions registerAndLoginOptions = null,
                                                 AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }

            var param = new LoginByPhoneCodeParam(
                new LoginByPhoneCodeInput(phone, code)
                {
                    AutoRegister = registerAndLoginOptions?.AutoRegister ?? false,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<LoginByPhoneCodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过手机号密码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="captchaCode">人机验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        [Obsolete("此方法已弃用")]
        public async Task<User> LoginByPhonePassword(string phone,
                                                     string password,
                                                     bool autoRegister = false,
                                                     string captchaCode = null)
        {
            var param = new LoginByPhonePasswordParam(
                new LoginByPhonePasswordInput(phone, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    AutoRegister = autoRegister,
                    CaptchaCode = captchaCode,
                }
            );

            var res = await RequestCustomDataWithOutToken<LoginByPhonePasswordResponse>(param.CreateRequest()).ConfigureAwait(false);
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过手机号密码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录信息</param>
        /// <param name="authingErrorBox">错误信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByPhonePassword(string phone,
                                                     string password,
                                                     RegisterAndLoginOptions registerAndLoginOptions = null,
                                                     AuthingErrorBox authingErrorBox = null)
        {
            // 序列化 registerAndLoginOptions.CustomData Params
            string ParamsString = null;
            string ContextString = null;
            if (registerAndLoginOptions?.CustomData != null)
            {
                ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
            }
            if (registerAndLoginOptions?.Context != null)
            {
                ContextString = registerAndLoginOptions.Context.ConvertJson();
            }

            var param = new LoginByPhonePasswordParam(
                new LoginByPhonePasswordInput(phone, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
                {
                    AutoRegister = registerAndLoginOptions?.AutoRegister ?? false,
                    CaptchaCode = registerAndLoginOptions?.CaptchaCode,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<LoginByPhonePasswordResponse>(param.CreateRequest()).ConfigureAwait(false);

            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过子账户登录
        /// </summary>
        /// <param name="account">子账户</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginBySubAccount(string account,
                                                  string password,
                                                  RegisterAndLoginOptions registerAndLoginOptions = null,
                                                  AuthingErrorBox authingErrorBox = null)
        {
            var param = new LoginBySubAccountParam(account, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
            {
                CaptchaCode = registerAndLoginOptions?.CaptchaCode,
                ClientIp = registerAndLoginOptions?.ClientIp,
            };

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithToken<LoginBySubAccountResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                User = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <param name="accessToken">用户的 access token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>JWTTokenStatus</returns>
        public async Task<JWTTokenStatus> CheckLoginStatus(string accessToken = null,
                                                           AuthingErrorBox authingErrorBox = null)
        {
            var param = new CheckLoginStatusParam()
            {
                Token = accessToken ?? AccessToken
            };

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<CheckLoginStatusResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            return res.Data?.Result;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email">邮件</param>
        /// <param name="scene">场景</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        public async Task<CommonMessage> SendEmail(string email,
                                                   EmailScene scene,
                                                   AuthingErrorBox authingErrorBox = null)
        {
            var param = new SendEmailParam(email, scene);

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<SendEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            if (res.Data != null)
            {
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 通过手机号验证码重置密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        public async Task<CommonMessage> ResetPasswordByPhoneCode(string phone,
                                                                  string code,
                                                                  string newPassword,
                                                                  AuthingErrorBox authingErrorBox = null)
        {
            var param = new ResetPasswordParam(code, EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey))
            {
                Phone = phone,
            };

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithToken<ResetPasswordResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过邮箱验证码重置密码
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="code">验证码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        public async Task<CommonMessage> ResetPasswordByEmailCode(string email,
                                                                  string code,
                                                                  string newPassword,
                                                                  AuthingErrorBox authingErrorBox = null)
        {
            var param = new ResetPasswordParam(code, EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey))
            {
                Email = email,
            };

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithOutToken<ResetPasswordResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过首次登录的 Token 重置密码
        /// 需要在控制台的用户侧配置登录后强制修改密码，在通过获取用户错误信息里面返回的 Token 来修改密码
        /// </summary>
        /// <param name="token">首次登录的Token</param>
        /// <param name="password">修改后的密码</param>
        /// <returns></returns>
        public async Task<CommonMessage> ResetPasswordByFirstLoginToken(string token,
                                                                        string password,
                                                                        AuthingErrorBox authingErrorBox = null)
        {
            var param = new ResetPasswordByFirstLoginTokenParam(token, EncryptHelper.RsaEncryptWithPublic(password, PublicKey));

            authingErrorBox?.Clear();

            var result = await RequestCustomDataWithToken<ResetPasswordByFirstLoginTokenResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }

            return result.Data?.Result;
        }

        /// <summary>
        /// 通过密码强制跟临时 Token 修改密码
        /// 需要在控制台的用户侧配置登录后强制修改密码，在通过获取用户错误信息里面返回的 Token 来修改密码
        /// </summary>
        /// <param name="token">登录的Token</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public async Task<CommonMessage> ResetPasswordByForceResetToken(string token,
                                                                        string oldPassword,
                                                                        string newPassword,
                                                                        AuthingErrorBox authingErrorBox = null)
        {
            var param = new ResetPasswordByForceResetTokenParam(token, EncryptHelper.RsaEncryptWithPublic(oldPassword, PublicKey), EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey));

            authingErrorBox?.Clear();

            var result = await RequestCustomDataWithToken<ResetPasswordByForceResetTokenResponse>(param.CreateRequest()).ConfigureAwait(false);

            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }
            return result.Data?.Result;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="updates">更新项</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UpdateProfile(UpdateUserInput updates,
                                              AuthingErrorBox authingErrorBox = null)
        {
            var param = new UpdateUserParam(updates);

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<UpdateUserResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            User = res.Data?.Result;
            return res.Data?.Result;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UpdatePassword(string newPassword,
                                               string oldPassword,
                                               AuthingErrorBox authingErrorBox = null)
        {
            CheckLoggedIn();
            var param = new UpdatePasswordParam(EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey))
            {
                OldPassword = EncryptHelper.RsaEncryptWithPublic(oldPassword, PublicKey),
            };
            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithOutToken<UpdatePasswordResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                User = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="phone">新手机号</param>
        /// <param name="phoneCode">新手机号的验证码</param>
        /// <param name="oldPhone">旧手机号</param>
        /// <param name="oldPhoneCode">旧手机号的验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UpdatePhone(string phone,
                                            string phoneCode,
                                            string oldPhone = null,
                                            string oldPhoneCode = null,
                                            AuthingErrorBox authingErrorBox = null)
        {
            CheckLoggedIn();
            var param = new UpdatePhoneParam(phone, phoneCode)
            {
                OldPhone = oldPhone,
                OldPhoneCode = oldPhoneCode,
            };

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<UpdatePhoneResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                User = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 更新邮箱
        /// </summary>
        /// <param name="email">新邮箱</param>
        /// <param name="emailCode">新邮箱的验证码</param>
        /// <param name="oldEmail">旧邮箱</param>
        /// <param name="oldEmailCode">旧邮箱的验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UpdateEmail(string email,
                                            string emailCode,
                                            string oldEmail = null,
                                            string oldEmailCode = null,
                                            AuthingErrorBox authingErrorBox = null)
        {
            CheckLoggedIn();
            var param = new UpdateEmailParam(email, emailCode)
            {
                OldEmail = oldEmail,
                OldEmailCode = oldEmailCode,
            };

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<UpdateEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            if (res.Data != null)
            {
                User = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 刷新 AccessToken
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>RefreshToken</returns>
        public async Task<RefreshToken> RefreshToken(AuthingErrorBox authingErrorBox = null)
        {
            var param = new RefreshTokenParam() { };

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<RefreshTokenResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            SetToken(res.Data?.Result.Token);
            return res.Data?.Result;
        }

        /// <summary>
        /// 关联账户
        /// </summary>
        /// <param name="primaryUserToken">主账号</param>
        /// <param name="secondaryUserToken">子账号</param>
        /// <param name="cancellationToken"></param>
        /// <returns>SimpleResponse</returns>
        public async Task<CommonMessage> LinkAccount(string primaryUserToken,
                                                     string secondaryUserToken,
                                                     AuthingErrorBox authingErrorBox = null)
        {
            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithToken<CommonMessage>("api/v2/users/link", new Dictionary<string, string>
            {
                {nameof(primaryUserToken),primaryUserToken },
                { nameof(secondaryUserToken),secondaryUserToken}
            }.ConvertJson()).ConfigureAwait(false);

            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            return res.Data;
        }

        /// <summary>
        /// 取消关联账户
        /// </summary>
        /// <param name="primaryUserToken">主账户</param>
        /// <param name="provider">提供者</param>
        /// <param name="cancellationToken"></param>
        /// <returns>SimpleResponse</returns>
        public async Task<CommonMessage> UnLinkAccount(string primaryUserToken,
                                                       ProviderType provider,
                                                       AuthingErrorBox authingErrorBox = null)
        {
            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithToken<CommonMessage>("api/v2/users/unlink", new Dictionary<string, string>
            {
                {nameof(primaryUserToken),primaryUserToken },
                { nameof(provider),JsonConvert.SerializeObject( provider)}
            }.ConvertJson()).ConfigureAwait(false);

            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            return res.Data;
        }

        /// <summary>
        /// 绑定手机号，如果已绑定则会报错
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="phoneCode">手机号验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> BindPhone(string phone, string phoneCode, AuthingErrorBox authingErrorBox = null)
        {
            var param = new BindPhoneParam(phone, phoneCode);

            authingErrorBox?.Clear();

            var res = await RequestCustomDataWithToken<BindPhoneResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            if (res.Data != null)
            {
                User = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 解绑定手机号，如果未绑定则会报错
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UnbindPhone(AuthingErrorBox authingErrorBox = null)
        {
            var param = new UnbindPhoneParam();

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<UnbindPhoneResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                User = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 绑定邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="emailCode">邮箱验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> BindEmail(string email, string emailCode, AuthingErrorBox authingErrorBox = null)
        {
            var param = new BindEmailParam(email, emailCode);

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<BindEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 取消绑定的邮箱
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UnbindEmail(AuthingErrorBox authingErrorBox = null)
        {
            var param = new UnbindEmailParam();

            authingErrorBox?.Clear();
            var res = await RequestCustomDataWithToken<UnbindEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> GetCurrentUser(AuthingErrorBox authingErrorBox = null)
        {
            var param = new UserParam();
            var res = await RequestCustomDataWithToken<UserResponse>(param.CreateRequest()).ConfigureAwait(false);

            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Data != null)
            {
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 根据 AccessToken 获取当前用户信息
        /// </summary>
        /// <param name="accessToken">AccessToken</param>
        /// <returns></returns>
        public async Task<User> GetCurrentUser(string accessToken, AuthingErrorBox authingErrorBox = null)
        {
            var param = new UserParam();
            var preprocessedRequest = new GraphQLHttpRequest(param.CreateRequest());
            var header = new Dictionary<string, string>() { { "Authorization", "Bearer " + accessToken } };
            var res = await RequestCustomDataWithOutToken<UserResponse>(GraphQLEndpoint, preprocessedRequest.ToHttpRequestBody(),
                header, contenttype: ContentType.JSON).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res != null)
            {
                user = res.Data?.Result;
                return res.Data?.Result;
            }
            return null;
        }

        /// <summary>
        /// 当前用户登出
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonMessage> Logout(AuthingErrorBox authingErrorBox = null)
        {
            var res = await RequestCustomDataWithToken<CommonMessage>($"api/v2/logout/?app_id={Options.AppId}", method: HttpMethod.Get).ConfigureAwait(false);

            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }

            if (res.Code == 200)
            {
                ClearUser();
            }
            return new CommonMessage { Code = res.Code, Message = res.Message };
        }

        public void ClearUser()
        {
            User = null;
            AccessToken = null;
        }

        /// <summary>
        /// 获取用户自定义字段的值列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        public async Task<IEnumerable<ResUdv>> ListUdv(AuthingErrorBox authingErrorBox = null)
        {
            CheckLoggedIn();
            var param = new UdvParam(UdfTargetType.USER, User.Id);
            var res = await RequestCustomDataWithToken<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var resUdv = AuthingUtils.ConvertUdv(res.Data?.Result);
            return resUdv;
        }

        /// <summary>
        /// 设置自定义字段值
        /// </summary>
        /// <param name="key">自定义字段的 key</param>
        /// <param name="value">自定义字段的 value</param>
        /// <param name="cancellationToken"></param>
        /// <returns>用户自定义字段</returns>
        public async Task<IEnumerable<ResUdv>> SetUdv(string key, object value, AuthingErrorBox authingErrorBox = null)
        {
            CheckLoggedIn();
            var param = new SetUdvParam(UdfTargetType.USER, User.Id, key, value.ConvertJson());
            var res = await RequestCustomDataWithToken<SetUdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var resUdv = AuthingUtils.ConvertUdv(res.Data?.Result);
            return resUdv;
        }

        /// <summary>
        /// 移除用户自定义字段的值
        /// </summary>
        /// <param name="key">自定义字段的 key </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ResUdv>> RemoveUdv(string key, AuthingErrorBox authingErrorBox = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"“{nameof(key)}”不能为 null 或空。", nameof(key));
            }

            CheckLoggedIn();
            var param = new RemoveUdvParam(UdfTargetType.USER, User.Id, key);
            var res = await RequestCustomDataWithToken<RemoveUdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var resUdv = AuthingUtils.ConvertUdv(res.Data?.Result);
            return resUdv;
        }

        /// <summary>
        /// 用户是否进行登录，登录返回用户信息，没有登录则抛出错误
        /// </summary>
        /// <returns>用户 ID</returns>
        public async Task<string> CheckLoggedIn(CancellationToken cancellationToken, AuthingErrorBox authingErrorBox = null)
        {
            var user = await GetCurrentUser(authingErrorBox).ConfigureAwait(false);
            if (user == null)
            {
                throw new AuthingException("请先登录");
            }

            return user.Id;
        }

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>组织列表</returns>
        public async Task<ListOrgsResult> ListOrgs(AuthingErrorBox authingErrorBox = null)
        {
            var res = await RequestCustomDataWithToken<object>("api/v2/users/me/orgs", method: HttpMethod.Get).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            string resultString = res.Data.ToString();

            var orgs = JsonConvert.DeserializeObject<List<List<Model.Management.Orgs.Node>>>(resultString);

            ListOrgsResult listOrgsResult = new ListOrgsResult { Orgs = orgs };

            return listOrgsResult;
        }

        public async Task<PaginatedDepartments> ListDepartment(AuthingErrorBox authingErrorBox = null)
        {
            var userId = CheckLoggedIn();
            var param = new GetUserDepartmentsParam(userId);
            var res = await RequestCustomDataWithToken<GetUserDepartmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var user = res.Data?.Result;
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            return user.Departments;
        }

        /// <summary>
        /// 通过 LDAP 进行登录
        /// </summary>
        /// <param name="username">LDAP 用户名</param>
        /// <param name="password">LDAP 用户密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        [Obsolete("此方法已弃用")]
        public async Task<User> LoginByLdap(string username, string password)
        {
            var res = await RequestCustomDataWithOutToken<User>("api/v2/ldap/verify-user", new Dictionary<string, string>
            {
                {nameof(username),username },
                { nameof(password),password}
            }.ConvertJson()).ConfigureAwait(false);

            SetCurrentUser(res.Data);

            return res.Data;
        }

        /// <summary>
        /// 通过 AD 登录
        /// </summary>
        /// <param name="username">AD 用户账号</param>
        /// <param name="password">AD 用户密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByAd(string username,
                                          string password,
                                          AuthingErrorBox authingErrorBox = null)
        {
            var firstLevelDomain = new Uri(Host).Host;

            var result = await RequestCustomDataWithOutToken<User>("api/v2/ad/verify-user", new Dictionary<string, string>
            {
                {"username",username },
                { "password",password}
            }.ConvertJson()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (authingErrorBox != null) ErrorHelper.LoadError(result, authingErrorBox);
            SetCurrentUser(result.Data);
            return result.Data;
        }

        // TODO: 缺少 uploadPhoto 方法
        // TODO: 缺少 updateAvatar 方法
        // TODO: 缺少 uploadAvatar 方法

        /// <summary>
        /// 获取自定义字段列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        public async Task<List<KeyValuePair<string, object>>> GetUdfValue(AuthingErrorBox authingErrorBox = null)
        {
            var userId = CheckLoggedIn();
            var param = new UdvParam(UdfTargetType.USER, userId);
            var res = await RequestCustomDataWithToken<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var list = res.Data?.Result;
            var resUdvList = AuthingUtils.ConverUdvToKeyValuePair(list);
            return resUdvList;
        }

        /// <summary>
        /// 设置自定义字段
        /// </summary>
        /// <param name="data">自定义字段相关数据</param>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        public async Task<List<KeyValuePair<string, object>>> SetUdfValue(KeyValueDictionary data, AuthingErrorBox authingErrorBox = null)
        {
            if (data.Count == 0)
            {
                throw new Exception("empty udf value list");
            }

            var input = new List<UserDefinedDataInput>();
            foreach (var item in data)
            {
                input.Add(new UserDefinedDataInput(item.Key)
                {
                    Key = item.Key,
                    Value = item.Value.ConvertJson()
                });
            }
            var userId = CheckLoggedIn();
            var param = new SetUdvBatchParam(UdfTargetType.USER, userId)
            {
                UdvList = input,
            };
            var res = await RequestCustomDataWithToken<SetUdvBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var list = res.Data?.Result;
            var resUdvList = AuthingUtils.ConverUdvToKeyValuePair(list);
            return resUdvList;
        }

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="key">自定义字段的 key</param>
        /// <param name="cancellationToken"></param>
        /// <returns>是否成功</returns>
        public async Task<bool> RemoveUdfValue(string key, AuthingErrorBox authingErrorBox = null)
        {
            var userId = CheckLoggedIn();
            var param = new RemoveUdvParam(UdfTargetType.USER, userId, key);
            var res = await RequestCustomDataWithToken<RemoveUdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            return !(res.Data?.Result).Any();
        }

        /// <summary>
        /// 获取密码等级
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>SecurityLevel</returns>
        public async Task<SecurityLevel> GetSecurityLevel(AuthingErrorBox authingErrorBox = null)
        {
            var result = await RequestCustomDataWithToken<SecurityLevel>("api/v2/users/me/security-level", method: HttpMethod.Get).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }
            return result.Data;
        }

        /// <summary>
        /// 允许访问的资源列表
        /// </summary>
        /// <param name="_namespace">权限分组的ID</param>
        /// <param name="_resourceType">资源类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns>PaginatedAuthorizedResources</returns>
        public async Task<PaginatedAuthorizedResources> ListAuthorizedResources(string nameSpace,
                                                                                ResourceType? _resourceType,
                                                                                AuthingErrorBox authingErrorBox = null)
        {
            var userId = CheckLoggedIn();
            var param = new ListUserAuthorizedResourcesParam(userId)
            {
                Namespace = nameSpace,
                ResourceType = _resourceType?.ToString()?.ToUpper(),
            };
            var res = await RequestCustomDataWithToken<ListUserAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            var user = res.Data?.Result;
            if (user == null)
            {
                throw new Exception("用户不存在");
            }

            var authorizedResources = user.AuthorizedResources;
            return authorizedResources;
        }

        /// <summary>
        /// 计算密码强度
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>PasswordSecurityLevel</returns>
        public PasswordSecurityLevel ComputedPasswordSecurityLevel(string password)
        {
            var higLevel = new Regex(@"(?=.*[0-9])                     #必须包含数字
                                       (?=.*[a-z])                  #必须包含小写或大写字母
                                       (?=([\x21-\x7e]+)[^a-zA-Z0-9])  #必须包含特殊符号
                                        .{6,}                         #至少8个字符，最多30个字符
                                        ", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            var middleLevel = new Regex(@"((?=.*\d)(?=.*\D)|(?=.*[a-zA-Z])(?=.*[^a-zA-Z]))(?!^.*[\u4E00-\u9FA5].*$).{6,}",
                                            RegexOptions.Multiline
| RegexOptions.IgnorePatternWhitespace);

            if (higLevel.IsMatch(password))
            {
                return PasswordSecurityLevel.HIGH;
            }
            if (middleLevel.IsMatch(password))
            {
                return PasswordSecurityLevel.MIDDLE;
            }
            return PasswordSecurityLevel.LOW;
        }

        /// <summary>
        /// 刷新 access token
        /// </summary>
        /// <param name="accessToken">用户 access token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>RefreshToken</returns>
        // INFO: 这个 RefreshToken 与上面的 RefreshToken 是有区别的
        public async Task<RefreshToken> RefreshToken(string accessToken, AuthingErrorBox authingErrorBox = null)
        {
            CheckLoggedIn();
            var param = new RefreshTokenParam();
            Dictionary<string, string> header = null;
            if (string.IsNullOrWhiteSpace(accessToken))
                header = new Dictionary<string, string>() { { "Authorization", "Bearer " + accessToken } };
            var res = await RequestCustomDataWithToken<RefreshTokenResponse>(param.CreateRequest().ConvertJson(), headers: header, contenttype: ContentType.JSON).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            return res.Data?.Result;
        }

        /// <summary>
        /// 当前用户是否具有某种角色
        /// </summary>
        /// <param name="roleCode">角色 code</param>
        /// <param name="_namespace">命名空间</param>
        /// <param name="cancellationToken"></param>
        /// <returns>bool</returns>
        public async Task<bool> HasRole(string roleCode, string _namespace = "", AuthingErrorBox authingErrorBox = null)
        {
            var userId = CheckLoggedIn();
            var param = new GetUserRolesParam(userId)
            {
                Namespace = _namespace,
            };
            var res = await RequestCustomDataWithToken<GetUserRolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (res.Errors != null)
            {
                authingErrorBox?.Set(res.Errors);
            }
            if (res.Data?.Result == null)
            {
                return false;
            }

            var user = res.Data?.Result;

            var roleList = user.Roles?.List;
            if (roleList == null || !roleList.Any())
            {
                return false;
            }

            var hasRole = roleList.Any(item => string.Equals(item.Code, roleCode));
            return hasRole;
        }

        /// <summary>
        /// 应用程序列表
        /// </summary>
        /// <param name="_params">列表参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<ListApplicationsResponse> ListApplications(ListParams _params = null, AuthingErrorBox authingErrorBox = null)
        {
            _params ??= new ListParams();
            var result = await RequestCustomDataWithToken<ListApplicationsResponse>($"api/v2/users/me/applications/allowed/?page={_params.Page}&limit={_params.Limit}", method: HttpMethod.Get).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }
            return result.Data;
        }

        public void SetLang(LangEnum lang)
        {
            Options.Lang = lang;
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="phone">电话号码</param>
        /// <param name="externalId">ExternalID</param>
        /// <returns></returns>
        public async Task<bool?> IsUserExists(string userName = null,
                                              string email = null,
                                              string phone = null,
                                              string externalId = null,
                                              AuthingErrorBox authingErrorBox = null)
        {
            IsUserExistsParam isUserExistsParam = new IsUserExistsParam()
            {
                Username = userName,
                Email = email,
                Phone = phone,
                ExternalId = externalId
            };

            var result = await RequestCustomDataWithToken<IsUserExistsResponse>(isUserExistsParam.CreateRequest()).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }
            return result.Data?.Result;
        }

        /// <summary>
        /// 检测密码是否合法
        /// </summary>
        /// <param name="password">需要检测的密码</param>
        /// <returns></returns>
        public async Task<CommonMessage> isPasswordValid(string password, AuthingErrorBox authingErrorBox = null)
        {
            var result = await RequestCustomDataWithToken<CommonMessage>($"api/v2/users/password/check?password={EncryptHelper.RsaEncryptWithPublic(password, PublicKey)}", method: HttpMethod.Get).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }

            return result.Data;
        }

        /// <summary>
        /// 通过微信登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="country"></param>
        /// <param name="lang"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<User> LoginByWechat(string code,
                                              string country = null,
                                              string lang = null,
                                              string state = null,
                                              AuthingErrorBox authingErrorBox = null)
        {
            string url = $"code={code}";
            if (!string.IsNullOrEmpty(country))
            {
                url += $"&country={country}";
            }
            if (!string.IsNullOrEmpty(lang))
            {
                url += $"lang={lang}";
            }
            if (!string.IsNullOrEmpty(state))
            {
                url += $"state={state}";
            }
            if (!string.IsNullOrEmpty(AppId))
            {
                url += $"app_id={AppId}";
            }

            var result = await RequestCustomDataWithOutToken<User>($"connection/social/wechat:mobile/{UserPoolId}/callback?{url}", method: HttpMethod.Get).ConfigureAwait(false);
            authingErrorBox?.Clear();
            if (result.Errors != null)
            {
                authingErrorBox?.Set(result.Errors);
            }
            return result.Data;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetToken(AuthingErrorBox authingErrorBox = null)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                return await GetAccessToken().ConfigureAwait(false);
            }
            return AccessToken;
        }
    }
}