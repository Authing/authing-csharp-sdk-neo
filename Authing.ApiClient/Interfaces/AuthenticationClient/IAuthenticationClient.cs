using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Domain.Model.Management.Department;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Interfaces.AuthenticationClient
{
    public interface IAuthenticationClient
    {
        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <returns>当前用户 ID</returns>
        string CheckLoggedIn();

        /// <summary>
        /// 用户是否进行登录，登录返回用户信息，没有登录则抛出错误
        /// </summary>
        /// <returns>用户 ID</returns>
        Task<string> CheckLoggedIn(CancellationToken cancellationToken);

        /// <summary>
        /// 设置当前用户信息
        /// </summary>
        /// <param name="user">用户数据</param>
        void SetCurrentUser(User user);

        /// <summary>
        /// 设置当前 AccessToken
        /// </summary>
        /// <param name="token">token 值</param>
        void SetToken(string token);

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="accessToken">用户 access token</param>
        /// <param name="cancellationToken">请求令牌</param>
        /// <returns>当前用户</returns>
        Task<User> CurrentUser(string accessToken = null);

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
        Task<User> RegisterByEmail(string email, string password, RegisterProfile profile = null,
            bool forceLogin = false, bool generateToken = false);

        /// <summary>
        /// 通过邮箱注册用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户信息</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken">请求令牌</param>
        /// <returns>注册的用户</returns>
        Task<User> RegisterByEmail(string email,
            string password,
            RegisterProfile profile = null,
            RegisterAndLoginOptions registerAndLoginOptions = null);

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
        Task<User> RegisterByUsername(string username,
            string password,
            RegisterProfile profile = null,
            bool forceLogin = false,
            bool generateToken = false);

        /// <summary>
        /// 通过用户名注册用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="profile">用户信息</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>注册的用户</returns>
        Task<User> RegisterByUsername(string username,
            string password,
            RegisterProfile profile = null,
            RegisterAndLoginOptions registerAndLoginOptions = null);

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
        Task<User> RegisterByPhoneCode(string phone,
            string code,
            string password = null,
            RegisterProfile profile = null,
            bool forceLogin = false,
            bool generateToken = false);

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
        Task<User> RegisterByPhoneCode(string phone,
            string code,
            string password = null,
            RegisterProfile profile = null,
            RegisterAndLoginOptions registerAndLoginOptions = null);

        /// <summary>
        /// 检查密码强度
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CheckPasswordStrengthResult</returns>
        Task<CheckPasswordStrengthResult> CheckPasswordStrength(string password);

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO: 破坏性更新
        Task<CommonMessage> SendSmsCode(string phone);

        /// <summary>
        /// 通过邮箱登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="captchaCode">人机验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        /// TODO: 下个大版本去除
        Task<User> LoginByEmail(string email,
            string password,
            bool autoRegister = false,
            string captchaCode = null);

        /// <summary>
        /// 通过邮箱登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> LoginByEmail(string email,
            string password,
            RegisterAndLoginOptions registerAndLoginOptions = null);

        /// <summary>
        /// 通过用户名登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="captchaCode">人机验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        /// TODO: 下个大版本去除
        Task<User> LoginByUsername(string username,
            string password,
            bool autoRegister = false,
            string captchaCode = null);

        /// <summary>
        /// 通过用户名登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">注册配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> LoginByUsername(string username,
            string password,
            RegisterAndLoginOptions registerAndLoginOptions = null);

        /// <summary>
        /// 通过手机号验证码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        /// TODO: 下一个大版本去除
        Task<User> LoginByPhoneCode(string phone,
            string code,
            bool autoRegister = false);

        /// <summary>
        /// 通过手机验证码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="registerAndLoginOptions">登录配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> LoginByPhoneCode(string phone,
            string code,
            RegisterAndLoginOptions registerAndLoginOptions = null);

        /// <summary>
        /// 通过手机号密码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="autoRegister">自动注册</param>
        /// <param name="captchaCode">人机验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        /// TODO：下个大版本去除
        Task<User> LoginByPhonePassword(string phone,
            string password,
            bool autoRegister = false,
            string captchaCode = null);

        /// <summary>
        /// 通过手机号密码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> LoginByPhonePassword(string phone,
            string password,
            RegisterAndLoginOptions registerAndLoginOptions = null);

        /// <summary>
        /// 通过子账户登录
        /// </summary>
        /// <param name="account">子账户</param>
        /// <param name="password">密码</param>
        /// <param name="registerAndLoginOptions">登录配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> LoginBySubAccount(string account,
            string password,
            RegisterAndLoginOptions registerAndLoginOptions = null);

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <param name="accessToken">用户的 access token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>JWTTokenStatus</returns>
        Task<JWTTokenStatus> CheckLoginStatus(string accessToken = null);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email">邮件</param>
        /// <param name="scene">场景</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        Task<CommonMessage> SendEmail(string email,
            EmailScene scene);

        /// <summary>
        /// 通过手机号验证码重置密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        Task<CommonMessage> ResetPasswordByPhoneCode(string phone,
            string code,
            string newPassword);

        /// <summary>
        /// 通过邮箱验证码重置密码
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="code">验证码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        Task<CommonMessage> ResetPasswordByEmailCode(string email,
            string code,
            string newPassword);

        /// <summary>
        /// 通过首次登录的 Token 重置密码
        /// </summary>
        /// <param name="token">首次登录的Token</param>
        /// <param name="password">修改后的密码</param>
        /// <returns></returns>
        Task<CommonMessage> ResetPasswordByFirstLoginToken(string token, string password);

        /// <summary>
        /// 通过密码强制跟临时 Token 修改密码
        /// </summary>
        /// <param name="token">登录的Token</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        Task<CommonMessage> ResetPasswordByForceResetToken(string token, string oldPassword, string newPassword);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="updates">更新项</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> UpdateProfile(UpdateUserInput updates);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> UpdatePassword(string newPassword, string oldPassword);

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="phone">新手机号</param>
        /// <param name="phoneCode">新手机号的验证码</param>
        /// <param name="oldPhone">旧手机号</param>
        /// <param name="oldPhoneCode">旧手机号的验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> UpdatePhone(string phone, string phoneCode, string oldPhone = null,
            string oldPhoneCode = null);

        /// <summary>
        /// 更新邮箱
        /// </summary>
        /// <param name="email">新邮箱</param>
        /// <param name="emailCode">新邮箱的验证码</param>
        /// <param name="oldEmail">旧邮箱</param>
        /// <param name="oldEmailCode">旧邮箱的验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> UpdateEmail(string email, string emailCode, string oldEmail = null,
            string oldEmailCode = null);

        /// <summary>
        /// 刷新 AccessToken
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>RefreshToken</returns>
        Task<RefreshToken> RefreshToken();

        /// <summary>
        /// 刷新 access token
        /// </summary>
        /// <param name="accessToken">用户 access token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>RefreshToken</returns>
        // INFO: 这个 RefreshToken 与上面的 RefreshToken 是有区别的
        Task<RefreshToken> RefreshToken(string accessToken);

        /// <summary>
        /// 关联账户
        /// </summary>
        /// <param name="primaryUserToken">主账号</param>
        /// <param name="secondaryUserToken">子账号</param>
        /// <param name="cancellationToken"></param>
        /// <returns>SimpleResponse</returns>
        Task<CommonMessage> LinkAccount(string primaryUserToken, string secondaryUserToken);

        /// <summary>
        /// 取消关联账户
        /// </summary>
        /// <param name="primaryUserToken">主账户</param>
        /// <param name="provider">提供者</param>
        /// <param name="cancellationToken"></param>
        /// <returns>SimpleResponse</returns>
        Task<CommonMessage> UnLinkAccount(string primaryUserToken, ProviderType provider);

        /// <summary>
        /// 绑定手机号，如果已绑定则会报错
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="phoneCode">手机号验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> BindPhone(string phone, string phoneCode);

        /// <summary>
        /// 解绑定手机号，如果未绑定则会报错
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> UnbindPhone();

        /// <summary>
        /// 绑定邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="emailCode">邮箱验证码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> BindEmail(string email, string emailCode);

        /// <summary>
        /// 取消绑定的邮箱
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> UnbindEmail();

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> GetCurrentUser();

        /// <summary>
        /// 当前用户登出
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommonMessage> Logout();

        void ClearUser();

        /// <summary>
        /// 获取用户自定义字段的值列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        Task<IEnumerable<ResUdv>> ListUdv();

        /// <summary>
        /// 设置自定义字段值
        /// </summary>
        /// <param name="key">自定义字段的 key</param>
        /// <param name="value">自定义字段的 value</param>
        /// <param name="cancellationToken"></param>
        /// <returns>用户自定义字段</returns>
        Task<IEnumerable<ResUdv>> SetUdv(string key, object value);

        /// <summary>
        /// 移除用户自定义字段的值
        /// </summary>
        /// <param name="key">自定义字段的 key </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ResUdv>> RemoveUdv(string key);

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<ListOrgsResult> ListOrgs();

        Task<PaginatedDepartments> ListDepartment();

        /// <summary>
        /// 通过 LDAP 进行登录
        /// </summary>
        /// <param name="username">LDAP 用户名</param>
        /// <param name="password">LDAP 用户密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        /// TODO: 下个大版本去除
        Task<User> LoginByLdap(string username, string password);

        /// <summary>
        /// 通过 AD 登录
        /// </summary>
        /// <param name="username">AD 用户账号</param>
        /// <param name="password">AD 用户密码</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        Task<User> LoginByAd(string username, string password);

        /// <summary>
        /// 获取自定义字段列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        Task<List<KeyValuePair<string, object>>> GetUdfValue();

        /// <summary>
        /// 设置自定义字段
        /// </summary>
        /// <param name="data">自定义字段相关数据</param>
        /// <param name="cancellationToken"></param>
        /// <returns>自定义字段列表</returns>
        Task<List<KeyValuePair<string, object>>> SetUdfValue(KeyValueDictionary data);

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="key">自定义字段的 key</param>
        /// <param name="cancellationToken"></param>
        /// <returns>是否成功</returns>
        Task<bool> RemoveUdfValue(string key);

        /// <summary>
        /// 获取密码等级
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>SecurityLevel</returns>
        Task<SecurityLevel> GetSecurityLevel();

        /// <summary>
        /// 允许访问的资源列表
        /// </summary>
        /// <param name="_namespace">权限分组的ID</param>
        /// <param name="_resourceType">资源类型</param>
        /// <param name="cancellationToken"></param>
        /// <returns>PaginatedAuthorizedResources</returns>
        Task<PaginatedAuthorizedResources> ListAuthorizedResources(string nameSpace, ResourceType? _resourceType);

        /// <summary>
        /// 计算密码强度
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>PasswordSecurityLevel</returns>
        PasswordSecurityLevel ComputedPasswordSecurityLevel(string password);

        /// <summary>
        /// 当前用户是否具有某种角色
        /// </summary>
        /// <param name="roleCode">角色 code</param>
        /// <param name="_namespace">命名空间</param>
        /// <param name="cancellationToken"></param>
        /// <returns>bool</returns>
        Task<bool> HasRole(string roleCode, string _namespace = "");

        /// <summary>
        /// 应用程序列表
        /// </summary>
        /// <param name="_params">列表参数</param>
        /// <param name="cancellationToken"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<ListApplicationsResponse> ListApplications(ListParams _params = null);

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="lang"></param>
        void SetLang(LangEnum lang);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">电子邮箱</param>
        /// <param name="phone">电话号码</param>
        /// <param name="externalId">ExternalID</param>
        /// <returns></returns>
        Task<bool?> IsUserExists(string userName = null, string email = null, string phone = null, string externalId = null);

        /// <summary>
        /// 检测密码是否合法
        /// </summary>
        /// <param name="password">需要检测的密码</param>
        /// <returns></returns>
        Task<CommonMessage> isPasswordValid(string password);

        /// <summary>
        /// 通过微信登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="country"></param>
        /// <param name="lang"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Task<User> LoginByWechat(string code, string country = null, string lang = null, string state = null);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        Task<string> GetToken();
    }
}