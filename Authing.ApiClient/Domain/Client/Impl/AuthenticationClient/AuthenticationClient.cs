using System;
using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.AuthenticationClient
{
    /// <summary>
    /// Authing 认证客户端类
    /// </summary>
    public class AuthenticationClient : BaseAuthenticationClient
    {
        

        /// <summary>
        /// 通过应用 ID 初始化
        /// </summary>
        /// <param name="appId">应用 ID</param>
        public AuthenticationClient(string appId): base(appId)
        {
        }

        /// <summary>
        /// 通过委托完成初始化
        /// </summary>
        /// <param name="init">配置参数</param>
        public AuthenticationClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
        }

        private User User
        {
            get => user;
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
            if (string.IsNullOrEmpty("userId"))
            {
                throw new Exception("不合法的 accessToken");
            }

            return "userId";
        }

        /// <summary>
        /// 设置当前用户信息
        /// </summary>
        /// <param name="currentUser">用户数据</param>
        public void SetCurrentUser(User currentUser)
        {
            User = currentUser;
        }

        /// <summary>
        /// 设置当前 Token
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
        public async Task<User> CurrentUser(
            string accessToken = null,
            CancellationToken cancellationToken = default)
        {
            var param = new UserParam();
            var res = await Post<GraphQLResponse<UserResponse>>(param.CreateRequest());
            user = res.Data.Result;
            return res.Data.Result;
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <param name="accessToken">用户的 access token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>JWTTokenStatus</returns>
        public async Task<JWTTokenStatus> CheckLoginStatus(
            string accessToken = null)
        {
            var param = new CheckLoginStatusParam()
            {
                Token = accessToken
            };
            var res = await Post<GraphQLResponse<CheckLoginStatusResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        ///// <summary>
        ///// 通过邮箱注册
        ///// </summary>
        ///// <param name="email">邮箱</param>
        ///// <param name="password">密码</param>
        ///// <param name="profile">用户资料</param>
        ///// <param name="forceLogin">强制登录</param>
        ///// <param name="generateToken">自动生成 token</param>
        ///// <param name="cancellationToken">请求令牌</param>
        ///// <returns>注册的用户</returns>
        ///// TODO: 下个大版本弃用
        //public async Task<User> RegisterByEmail(
        //    string email,
        //    string password,
        //    RegisterProfile profile = null,
        //    bool forceLogin = false,
        //    bool generateToken = false,
        //    CancellationToken cancellationToken = default)
        //{
        //    var param = new RegisterByEmailParam(
        //        new RegisterByEmailInput(email, Encrypt(password))
        //        {
        //            Profile = profile,
        //            ForceLogin = forceLogin,
        //            GenerateToken = generateToken,
        //        }
        //    );

        //    var res = await Request<RegisterByEmailResponse>(param.CreateRequest(), cancellationToken);
        //    User = res.Result;
        //    return res.Result;
        //}

        ///// <summary>
        ///// 通过邮箱注册用户
        ///// </summary>
        ///// <param name="email">邮箱</param>
        ///// <param name="password">密码</param>
        ///// <param name="profile">用户信息</param>
        ///// <param name="registerAndLoginOptions">注册配置信息</param>
        ///// <param name="cancellationToken">请求令牌</param>
        ///// <returns>注册的用户</returns>
        //public async Task<User> RegisterByEmail(
        //    string email,
        //    string password,
        //    RegisterProfile profile = null,
        //    RegisterAndLoginOptions registerAndLoginOptions = null,
        //    CancellationToken cancellationToken = default)
        //{
        //    // 序列化 registerAndLoginOptions.CustomData Params
        //    //string ParamsString = null;
        //    //string ContextString = null;
        //    //if (registerAndLoginOptions?.CustomData != null)
        //    //{
        //    //    ParamsString = registerAndLoginOptions.CustomData.ConvertJson();
        //    //}
        //    //if (registerAndLoginOptions?.Context != null)
        //    //{
        //    //    ContextString = registerAndLoginOptions.Context.ConvertJson();
        //    //}
        //    //var param = new RegisterByEmailParam(
        //    //    new RegisterByEmailInput(email, Encrypt(password))
        //    //    {
        //    //        Profile = profile,
        //    //        ForceLogin = registerAndLoginOptions?.ForceLogin,
        //    //        GenerateToken = registerAndLoginOptions?.GenerateToken,
        //    //        ClientIp = registerAndLoginOptions?.ClientIp,
        //    //        Params = ParamsString,
        //    //        Context = ContextString,
        //    //    }
        //    //);

        //    //var res = await Request<RegisterByEmailResponse>(param.CreateRequest(), cancellationToken);

        //    //User = res.Result;
        //    //return res.Result;
        //}
    }
}