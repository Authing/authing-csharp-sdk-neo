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
using Authing.ApiClient.Interfaces.AuthenticationClient;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    /// <summary>
    /// Authing 认证客户端类
    /// </summary>
    public partial class AuthenticationClient : BaseAuthenticationClient, IStandardProtocol,IAuthenticationClient
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

            var tokenInfo = AuthingUtils.GetPayloadByToken(AccessToken);
            var userDataString = tokenInfo.ContainsKey("data") ? tokenInfo["data"]: "";
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
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="accessToken">用户 access token</param>
        /// <param name="cancellationToken">请求令牌</param>
        /// <returns>当前用户</returns>
        public async Task<User> CurrentUser(string accessToken = null)
        {
            var param = new UserParam();
            var res = await Request<UserResponse>(param.CreateRequest(), accessToken);
            user = res.Data.Result;
            return res.Data.Result;
        }

        [Obsolete("该方法已弃用")]
        /// <summary>
        /// 通过邮箱注册
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户资料</param>
        /// <param name="forceLogin">强制登录</param>
        /// <param name="generateToken">自动生成 token</param>
        /// <param name="cancellationToken">请求令牌</param>
        /// <returns>注册的用户</returns>
        /// TODO: 下个大版本弃用
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

            var res = await Request<RegisterByEmailResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                                RegisterAndLoginOptions registerAndLoginOptions = null)
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

            var res = await Request<RegisterByEmailResponse>(param.CreateRequest());

            User = res.Data.Result;
            return res.Data.Result;
        }

        [Obsolete("此方法已弃用")]
        /// <summary>
        /// 通过用户名注册
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户资料</param>
        /// <param name="forceLogin">强制登录</param>
        /// <param name="generateToken">自动生成 token</param>
        /// <param name="cancellationToken">请求令牌</param>
        /// <returns>注册的用户</returns>
        /// TODO: 下个大版本弃用
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

            var res = await Request<RegisterByUsernameResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                                   RegisterAndLoginOptions registerAndLoginOptions = null)
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

            var res = await Request<RegisterByUsernameResponse>(param.CreateRequest());

            User = res.Data.Result;
            return res.Data.Result;
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
        /// TODO: 下个大版本弃用
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

            var res = await Request<RegisterByPhoneCodeResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                                    RegisterAndLoginOptions registerAndLoginOptions = null)
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
                    Password = EncryptHelper.RsaEncryptWithPublic(password, PublicKey),
                    Profile = profile,
                    ForceLogin = registerAndLoginOptions?.ForceLogin,
                    GenerateToken = registerAndLoginOptions?.GenerateToken,
                    ClientIp = registerAndLoginOptions?.ClientIp,
                    Params = ParamsString,
                    Context = ContextString,
                }
            );

            var res = await Request<RegisterByPhoneCodeResponse>(param.CreateRequest());

            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 检查密码强度
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CheckPasswordStrengthResult</returns>
        public async Task<CheckPasswordStrengthResult> CheckPasswordStrength(string password)
        {
            var param = new CheckPasswordStrengthParam(password);
            var res = await Request<CheckPasswordStrengthResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO: 破坏性更新
        public async Task<CommonMessage> SendSmsCode(string phone)
        {
            var res = await Post<CommonMessage>("api/v2/sms/send", new Dictionary<string, object>
            {
                {nameof(phone), phone }
            });

            CommonMessage ms = new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message,
            };
            return ms;
        }

        [Obsolete("此方法已弃用")]
        /// <summary>
        /// 通过邮箱登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="captchaCode">人机验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
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

            var res = await Request<LoginByEmailResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                             RegisterAndLoginOptions registerAndLoginOptions = null)
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

            var res = await Request<LoginByEmailResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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

            var res = await Request<LoginByUsernameResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                                RegisterAndLoginOptions registerAndLoginOptions = null)
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

            var res = await Request<LoginByUsernameResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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

            var res = await Request<LoginByPhoneCodeResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                                 RegisterAndLoginOptions registerAndLoginOptions = null)
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

            var res = await Request<LoginByPhoneCodeResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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

            var res = await Request<LoginByPhonePasswordResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 通过手机号密码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByPhonePassword(string phone,
                                                     string password,
                                                     RegisterAndLoginOptions registerAndLoginOptions = null)
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

            var res = await Request<LoginByPhonePasswordResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
                                                  RegisterAndLoginOptions registerAndLoginOptions = null)
        {
            var param = new LoginBySubAccountParam(account, EncryptHelper.RsaEncryptWithPublic(password, PublicKey))
            {
                CaptchaCode = registerAndLoginOptions?.CaptchaCode,
                ClientIp = registerAndLoginOptions?.ClientIp,
            };

            var res = await Request<LoginBySubAccountResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <param name="accessToken">用户的 access token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>JWTTokenStatus</returns>
        public async Task<JWTTokenStatus> CheckLoginStatus(string accessToken = null)
        {
            var param = new CheckLoginStatusParam()
            {
                Token = accessToken ?? AccessToken
            };
            var res = await Request<CheckLoginStatusResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email">邮件</param>
        /// <param name="scene">场景</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        public async Task<CommonMessage> SendEmail(string email,
                                                   EmailScene scene)
        {
            var param = new SendEmailParam(email, scene);
            var res = await Request<SendEmailResponse>(param.CreateRequest());
            return res.Data.Result;
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
                                                                  string newPassword)
        {
            var param = new ResetPasswordParam(code, EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey))
            {
                Phone = phone,
            };
            var res = await Request<ResetPasswordResponse>(param.CreateRequest());
            return res.Data.Result;
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
                                                                  string newPassword)
        {
            var param = new ResetPasswordParam(code, EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey))
            {
                Email = email,
            };
            var res = await Request<ResetPasswordResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 通过首次登录的 Token 重置密码
        /// </summary>
        /// <param name="token">首次登录的Token</param>
        /// <param name="password">修改后的密码</param>
        /// <returns></returns>
        public async Task<CommonMessage> ResetPasswordByFirstLoginToken(string token, string password)
        {
            var param = new ResetPasswordByFirstLoginTokenParam(token, password);

            var result = await Request<ResetPasswordByFirstLoginTokenResponse>(param.CreateRequest());

            return result.Data.Result;
        }

        /// <summary>
        /// 通过密码强制跟临时 Token 修改密码
        /// </summary>
        /// <param name="token">登录的Token</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public async Task<CommonMessage> ResetPasswordByForceResetToken(string token, string oldPassword, string newPassword)
        {
            var param = new ResetPasswordByForceResetTokenParam(token, oldPassword, newPassword);

            var result = await Request<ResetPasswordByForceResetTokenResponse>(param.CreateRequest());

            return result.Data.Result;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="updates">更新项</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UpdateProfile(UpdateUserInput updates)
        {
            var param = new UpdateUserParam(updates);
            var res = await Request<UpdateUserResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UpdatePassword(string newPassword, string oldPassword)
        {
            CheckLoggedIn();
            var param = new UpdatePasswordParam(EncryptHelper.RsaEncryptWithPublic(newPassword, PublicKey))
            {
                OldPassword = EncryptHelper.RsaEncryptWithPublic(oldPassword, PublicKey),
            };
            var res = await Request<UpdatePasswordResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
        public async Task<User> UpdatePhone(string phone, string phoneCode, string oldPhone = null,
                                            string oldPhoneCode = null)
        {
            CheckLoggedIn();
            var param = new UpdatePhoneParam(phone, phoneCode)
            {
                OldPhone = oldPhone,
                OldPhoneCode = oldPhoneCode,
            };
            var res = await Request<UpdatePhoneResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
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
        public async Task<User> UpdateEmail(string email, string emailCode, string oldEmail = null,
                                            string oldEmailCode = null)
        {
            CheckLoggedIn();
            var param = new UpdateEmailParam(email, emailCode)
            {
                OldEmail = oldEmail,
                OldEmailCode = oldEmailCode,
            };
            var res = await Request<UpdateEmailResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 刷新 AccessToken
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>RefreshToken</returns>
        public async Task<RefreshToken> RefreshToken()
        {
            var param = new RefreshTokenParam() { };
            var res = await Request<RefreshTokenResponse>(param.CreateRequest());
            SetToken(res.Data.Result.Token);
            return res.Data.Result;
        }

        /// <summary>
        /// 关联账户
        /// </summary>
        /// <param name="primaryUserToken">主账号</param>
        /// <param name="secondaryUserToken">子账号</param>
        /// <param name="cancellationToken"></param>
        /// <returns>SimpleResponse</returns>
        public async Task<CommonMessage> LinkAccount(string primaryUserToken, string secondaryUserToken)
        {
            var res = await Post<CommonMessage>("api/v2/users/link", new Dictionary<string, string>
            {
                {nameof(primaryUserToken),primaryUserToken },
                { nameof(secondaryUserToken),secondaryUserToken}
            });

            return res.Data;
        }

        /// <summary>
        /// 取消关联账户
        /// </summary>
        /// <param name="primaryUserToken">主账户</param>
        /// <param name="provider">提供者</param>
        /// <param name="cancellationToken"></param>
        /// <returns>SimpleResponse</returns>
        public async Task<CommonMessage> UnLinkAccount(string primaryUserToken, ProviderType provider)
        {
            var res = await Post<CommonMessage>("api/v2/users/unlink", new Dictionary<string, string>
            {
                {nameof(primaryUserToken),primaryUserToken },
                { nameof(provider),JsonConvert.SerializeObject( provider)}
            });

            return res.Data;
        }

        /// <summary>
        /// 绑定手机号，如果已绑定则会报错
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="phoneCode">手机号验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> BindPhone(string phone, string phoneCode)
        {
            var param = new BindPhoneParam(phone, phoneCode);
            var res = await Request<BindPhoneResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 解绑定手机号，如果未绑定则会报错
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UnbindPhone()
        {
            var param = new UnbindPhoneParam();
            var res = await Request<UnbindPhoneResponse>(param.CreateRequest());
            User = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 绑定邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="emailCode">邮箱验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> BindEmail(string email, string emailCode)
        {
            var param = new BindEmailParam(email, emailCode);
            var res = await Request<BindEmailResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 取消绑定的邮箱
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> UnbindEmail()
        {
            var param = new UnbindEmailParam();
            var res = await Request<UnbindEmailResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> GetCurrentUser()
        {
            var param = new UserParam();
            var res = await Request<UserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 当前用户登出
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonMessage> Logout()
        {
            var res = await Get<CommonMessage>($"api/v2/logout/?app_id={Options.AppId}", null);

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

        private object GetHeaders()
        {
            const string SDK_VERSION = "4.2.4.7";
            // 如果用户需要则取得 headers 之后进行合并
            var res = new
            {
                x_authing_sdk_version = $"csharp:{SDK_VERSION}",
                x_authing_userpool_id = Options.UserPoolId ?? "",
                x_authing_request_from = Options.RequestFrom ?? "sdk",
                x_authing_app_id = Options.AppId ?? "",
                x_authing_lang = Options.Lang.ToString().ToUpper(),
                // Authorization = $"Bearer {AccessToken}",
            };
            if (String.IsNullOrEmpty(AccessToken))
            {
                return new
                {
                    x_authing_sdk_version = $"csharp:{SDK_VERSION}",
                    x_authing_userpool_id = Options.UserPoolId ?? "",
                    x_authing_request_from = Options.RequestFrom ?? "sdk",
                    x_authing_app_id = Options.AppId ?? "",
                    x_authing_lang = Options.Lang.ToString().ToUpper(),
                    Authorization = $"Bearer {AccessToken}",
                };
            }
            return res;
        }

        /// <summary>
        /// 获取用户自定义字段的值列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        public async Task<IEnumerable<ResUdv>> ListUdv()
        {
            CheckLoggedIn();
            var param = new UdvParam(UdfTargetType.USER, User.Id);
            var res = await Request<UdvResponse>(param.CreateRequest());
            var resUdv = AuthingUtils.ConvertUdv(res.Data.Result);
            return resUdv;
        }

        /// <summary>
        /// 设置自定义字段值
        /// </summary>
        /// <param name="key">自定义字段的 key</param>
        /// <param name="value">自定义字段的 value</param>
        /// <param name="cancellationToken"></param>
        /// <returns>用户自定义字段</returns>
        public async Task<IEnumerable<ResUdv>> SetUdv(string key, object value)
        {
            CheckLoggedIn();
            var param = new SetUdvParam(UdfTargetType.USER, User.Id, key, value.ConvertJson());
            var res = await Request<SetUdvResponse>(param.CreateRequest());
            var resUdv = AuthingUtils.ConvertUdv(res.Data.Result);
            return resUdv;
        }


        /// <summary>
        /// 移除用户自定义字段的值
        /// </summary>
        /// <param name="key">自定义字段的 key </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ResUdv>> RemoveUdv(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"“{nameof(key)}”不能为 null 或空。", nameof(key));
            }

            CheckLoggedIn();
            var param = new RemoveUdvParam(UdfTargetType.USER, User.Id, key);
            var res = await Request<RemoveUdvResponse>(param.CreateRequest());
            var resUdv = AuthingUtils.ConvertUdv(res.Data.Result);
            return resUdv;
        }

        /// <summary>
        /// 用户是否进行登录，登录返回用户信息，没有登录则抛出错误
        /// </summary>
        /// <returns>用户 ID</returns>
        public async Task<string> CheckLoggedIn(CancellationToken cancellationToken)
        {
            var user = await GetCurrentUser();
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
        /// <returns>HttpResponseMessage</returns>
        public async Task<ListOrgsResult> ListOrgs()
        {
            var res = await Get<object>("api/v2/users/me/orgs", null);
            string resultString = res.Data.ToString();

            var orgs = JsonConvert.DeserializeObject<List<List<Model.Management.Orgs.Node>>>(resultString);

            ListOrgsResult listOrgsResult = new ListOrgsResult { Orgs = orgs };

            return listOrgsResult;

        }

        public async Task<PaginatedDepartments> ListDepartment()
        {
            var userId = CheckLoggedIn();
            var param = new GetUserDepartmentsParam(userId);
            var res = await Request<GetUserDepartmentsResponse>(param.CreateRequest());
            var user = res.Data.Result;
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
            var res = await Post<User>("api/v2/ldap/verify-user", new Dictionary<string, string>
            {
                {nameof(username),username },
                { nameof(password),password}
            });

            SetCurrentUser(res.Data);

            return res.Data;
        }

        // TODO: 缺少 loginByLdap 重载方法

        /// <summary>
        /// 通过 AD 登录
        /// </summary>
        /// <param name="username">AD 用户账号</param>
        /// <param name="password">AD 用户密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        public async Task<User> LoginByAd(string username, string password)
        {
            var firstLevelDomain = new Uri(Host).Host;

            var result = await Post<User>("api/v2/ldap/verify-user", new Dictionary<string, string>
            {
                {"username",username },
                { "password",password}
            });




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
        public async Task<List<KeyValuePair<string, object>>> GetUdfValue()
        {
            var userId = CheckLoggedIn();
            var param = new UdvParam(UdfTargetType.USER, userId);
            var res = await Request<UdvResponse>(param.CreateRequest());
            var list = res.Data.Result;
            var resUdvList = AuthingUtils.ConverUdvToKeyValuePair(list);
            return resUdvList;
        }

        /// <summary>
        /// 设置自定义字段
        /// </summary>
        /// <param name="data">自定义字段相关数据</param>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        public async Task<List<KeyValuePair<string, object>>> SetUdfValue(KeyValueDictionary data)
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
                    Value = item.Value.ConvertJson(),
                });
            }
            var userId = CheckLoggedIn();
            var param = new SetUdvBatchParam(UdfTargetType.USER, userId)
            {
                UdvList = input,
            };
            var res = await Request<SetUdvBatchResponse>(param.CreateRequest());
            var list = res.Data.Result;
            var resUdvList = AuthingUtils.ConverUdvToKeyValuePair(list);
            return resUdvList;
        }

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="key">自定义字段的 key</param>
        /// <param name="cancellationToken"></param>
        /// <returns>是否成功</returns>
        public async Task<bool> RemoveUdfValue(string key)
        {
            var userId = CheckLoggedIn();
            var param = new RemoveUdvParam(UdfTargetType.USER, userId, key);
            var res = await Request<RemoveUdvResponse>(param.CreateRequest());
            return true;
        }

        /// <summary>
        /// 获取密码等级
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>SecurityLevel</returns>
        public async Task<SecurityLevel> GetSecurityLevel()
        {
            var result = await Get<SecurityLevel>("api/v2/users/me/security-level", null);
            return result.Data;
        }

        /// <summary>
        /// 允许访问的资源列表
        /// </summary>
        /// <param name="_namespace">权限分组的ID</param>
        /// <param name="_resourceType">资源类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns>PaginatedAuthorizedResources</returns>
        public async Task<PaginatedAuthorizedResources> ListAuthorizedResources(string nameSpace, ResourceType? _resourceType)
        {
            var userId = CheckLoggedIn();
            var param = new ListUserAuthorizedResourcesParam(userId)
            {
                Namespace = nameSpace,
                ResourceType = _resourceType?.ToString()?.ToUpper(),
            };
            var res = await Request<ListUserAuthorizedResourcesResponse>(param.CreateRequest());
            var user = res.Data.Result;
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
        public async Task<RefreshToken> RefreshToken(string accessToken)
        {
            CheckLoggedIn();
            var param = new RefreshTokenParam();
            var res = await Request<RefreshTokenResponse>(param.CreateRequest(), accessToken);
            return res.Data.Result;
        }

        /// <summary>
        /// 当前用户是否具有某种角色
        /// </summary>
        /// <param name="roleCode">角色 code</param>
        /// <param name="_namespace">命名空间</param>
        /// <param name="cancellationToken"></param>
        /// <returns>bool</returns>
        public async Task<bool> HasRole(string roleCode, string _namespace = "")
        {
            var userId = CheckLoggedIn();
            var param = new GetUserRolesParam(userId)
            {
                Namespace = _namespace,
            };
            var res = await Request<GetUserRolesResponse>(param.CreateRequest());
            if (res.Data.Result == null)
            {
                return false;
            }
            var user = res.Data.Result;

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
        public async Task<ListApplicationsResponse> ListApplications(ListParams _params = null)
        {
            _params ??= new ListParams();
            var result = await Get<ListApplicationsResponse>($"api/v2/users/me/applications/allowed/?page={_params.Page}&limit={_params.Limit}", null);

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
        public async Task<bool?> IsUserExists(string userName = null, string email = null, string phone = null, string externalId = null)
        {
            IsUserExistsParam isUserExistsParam = new IsUserExistsParam()
            {
                Username = userName,
                Email = email,
                Phone = phone,
                ExternalId = externalId
            };

            var result = await Request<IsUserExistsResponse>(isUserExistsParam.CreateRequest());

            return result.Data.Result;
        }

        /// <summary>
        /// 检测密码是否合法
        /// </summary>
        /// <param name="password">需要检测的密码</param>
        /// <returns></returns>
        public async Task<CommonMessage> isPasswordValid(string password)
        {
            var result = await Get<CommonMessage>($"api/v2/users/password/check?password={EncryptHelper.RsaEncryptWithPublic(password, PublicKey)}", null);
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
        public async Task<User> LoginByWechat(string code, string country = null, string lang = null, string state = null)
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

            var result = await Get<User>($"connection/social/wechat:mobile/{UserPoolId}/callback?{url}", null);
            return result.Data;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetToken()
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                return await GetAccessToken();
            }
            return AccessToken;
        }

    }
}