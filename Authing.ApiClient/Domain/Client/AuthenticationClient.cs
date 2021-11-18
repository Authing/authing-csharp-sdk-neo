using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Exceptions;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Client
{
    /// <summary>
    /// Authing 认证客户端类
    /// </summary>
    public partial class AuthenticationClient : BaseClient
    {
        [Obsolete("建议使用委托完成初始化")]
        /// <summary>
        /// 通过用户池 ID 初始化
        /// </summary>
        /// <param name="userPoolId">用户池 ID，可以在控制台获取</param>
        public AuthenticationClient(string userPoolId, string secret) : base(userPoolId, secret)
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
            var res = await Post<UserResponse>(param.CreateRequest());
            user = res.Data.Result;
            return res.Data.Result;
        }
    }
}
