using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Users;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Domain.Model.Management.Department;
using Authing.ApiClient.Domain.Model.Management.AuthorizedResources;
using Authing.ApiClient.Domain.Model.Management.UserAction;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IUsersManagementClient
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="createUserOption">选项</param>
        /// <returns></returns>
        Task<User> Create(CreateUserInput userInfo, CreateUserOption createUserOption = null);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="updates">更新信息</param>
        /// <returns></returns>
        Task<User> Update(string userId, UpdateUserInput updates);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="withCustomData">是否带用户自定义数据</param>
        /// <returns></returns>
        Task<User> Detail(string userId, bool withCustomData = false);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<CommonMessage> Delete(string userId);

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户 ID 列表，多个 ID 以英文逗号分隔</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteMany(IEnumerable<string> userIds);

        /// <summary>
        /// 通过 ID 列表批量获取用户信息
        /// </summary>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<IEnumerable<User>> Batch(IEnumerable<string> userIds, BatchFetchUserTypes batchFetchUserType = BatchFetchUserTypes.id);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page">当前页数，默认为 1</param>
        /// <param name="limit">每页最大数量，默认为 10</param>
        /// <returns></returns>
        Task<PaginatedUsers> List(int page = 1, int limit = 10);

        /// <summary>
        /// 获取已归档用户列表
        /// </summary>
        /// <param name="page">当前页数，默认为 1</param>
        /// <param name="limit">每页最大数量，默认为 10</param>
        /// <returns></returns>
        Task<PaginatedUsers> ListArchivedUsers(int page = 1, int limit = 10);

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="options">选项</param>
        /// <returns></returns>
        Task<bool?> Exists(ExistsOption options);

        /// <summary>
        /// 通过手机号、游戏、用户名查找用户
        /// </summary>
        /// <param name="FindUserOption">选项</param>
        /// <returns></returns>
        Task<User> Find(FindUserOption options);

        /// <summary>
        /// 模糊搜索用户
        /// </summary>
        /// <param name="query">关键字</param>
        /// <param name="SearchOption">选项</param>
        /// <returns></returns>
        Task<PaginatedUsers> Search(string query, SearchOption option = null);

        /// <summary>
        /// 刷新 access token
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<RefreshToken> RefreshToken(string userId);

        /// <summary>
        /// 获取用户分组列表
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<PaginatedGroups> ListGroups(string userId);

        /// <summary>
        /// 加入分组
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="group">分组 ID</param>
        /// <returns></returns>
        Task<CommonMessage> AddGroup(string userId, string group);

        /// <summary>
        /// 退出分组
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="group">分组 ID</param>
        /// <returns></returns>
        Task<CommonMessage> RemoveGroup(string userId, string group);

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<PaginatedRoles> ListRoles(string userId, string _namespace = null);

        /// <summary>
        /// 批量授权角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roles">用户角色 Code 列表</param>
        /// <returns></returns>
        Task<CommonMessage> AddRoles(string userId, IEnumerable<string> roles, string _namespace = null);

        /// <summary>
        /// 批量撤销用户角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roles">用户角色 Code 列表</param>
        /// <returns></returns>
        Task<CommonMessage> RemoveRoles(string userId, IEnumerable<string> roles, string _namespace = null);

        /// <summary>
        /// 获取用户所在组织机构
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<PaginatedOrgsAndNodes> ListOrgs(string userId);

        /// <summary>
        /// 获取用户所在部门
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<PaginatedDepartments> ListDepartment(string userId);

        /// <summary>
        /// 获取用户被授权的所有资源
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="_namespace">资源分组</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<PaginatedAuthorizedResources> ListAuthorizedResources(string userId, string _namespace, ListAuthorizedResourcesOption option = null);

        /// <summary>
        /// 获取某个用户的所有自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<List<KeyValuePair<string, object>>> GetUdfValue(string userId);

        /// <summary>
        /// 批量获取多个用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(string[] userIds);

        /// <summary>
        /// 设置某个用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        Task<IEnumerable<UserDefinedData>> SetUdfValue(string userId, KeyValueDictionary data);

        /// <summary>
        /// 批量设置自定义数据
        /// </summary>
        /// <param name="setUdfValueBatchInput"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDefinedData>> SetUdfValueBatch(SetUserUdfValueBatchParam[] setUdfValueBatchInput);

        /// <summary>
        /// 清除用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<CommonMessage> RemoveUdfValue(string userId, string key);

        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roleCode">角色 Code</param>
        /// <param name="_namespace">权限分组 ID</param>
        /// <returns></returns>
        Task<bool> hasRole(string userId, string roleCode, string _namespace = null);

        /// <summary>
        /// 强制一批用户下线
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roleCode">角色 Code</param>
        /// <param name="_namespace">权限分组 ID</param>
        /// <returns></returns>
        Task<CommonMessage> Kick(IEnumerable<string> userIds);

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <param name="logoutParam">选项</param>
        /// <returns></returns>
        Task<CommonMessage> Logout(LogoutParam logoutParam);

        /// <summary>
        /// 查询用户的登录状态
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="appId">应用 ID</param>
        /// <param name="devicdId">选项</param>
        /// <returns></returns>
        Task<CheckLoginStatusRes> CheckLoginStatus(string userId, string appId = null, string devicdId = null);

        /// <summary>
        /// 审计日志列表
        /// </summary>
        /// <param name="listUserActionsParam">选项</param>
        /// <returns></returns>
        Task<ListUserActionsRealRes> ListUserActions(ListUserActionsParam listUserActionsParam = null);

        /// <summary>
        /// 发送首次登录验证邮件
        /// </summary>
        /// <param name="sendFirstLoginVerifyEmailParam">选项</param>
        /// <returns></returns>
        Task<SendFirstLoginVerifyEmailResponse> SendFirstLoginVerifyEmail(SendFirstLoginVerifyEmailParam sendFirstLoginVerifyEmailParam);

        /// <summary>
        /// 批量导入用户
        /// </summary>
        /// <param name="userInfos">用户信息列表</param>
        /// <returns></returns>
        Task<CreateUsersRes> CreateUsers(IEnumerable<CreateUserInput> userInfos);

        /// <summary>
        /// 获取用户所在租户
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<User> GetUserTenants(string userId);
    }
}
