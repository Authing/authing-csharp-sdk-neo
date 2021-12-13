using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IRolesManagementClient
    {

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="description">角色描述</param>
        /// <param name="parentCode">父角色唯一标志</param>
        /// <param name="nameSpace">分组ID</param>
        /// <returns></returns>
        Task<Role> Create(string code, string description = null, string parentCode = null, string nameSpace = null);


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO: 在下一个大版本中去除
        [Obsolete("此方法已过时")]
        Task<CommonMessage> Delete(string code);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="nameSpace">权限分组ID</param>
        /// <returns></returns>
        Task<CommonMessage> Delete(string code, string nameSpace = null);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="codeList">角色 code 列表</param>
        /// <returns></returns>
        /// TODO： 在下一个大版本中去除
        [Obsolete("此方法已过时")]
        Task<CommonMessage> DeleteMany(IEnumerable<string> codeList);
        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="codeList">角色 code 列表</param>
        /// <param name="nameSpace">权限分组的ID</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteMany(IEnumerable<string> codeList, string nameSpace = null);

        /// <summary>
        /// 修改角色资料
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="description">角色描述</param>
        /// <param name="newCode">新的 code</param>
        /// <returns></returns>
        /// TODO: 下一个大版本中去除
        [Obsolete("此方法已过时")]
        Task<Role> Update(string code, string description = null, string newCode = null);

        /// <summary>
        /// 修改角色资料
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="updateRoleOptions">修改角色资料</param>
        Task<Role> Update(UpdateRoleOptions updateRoleOptions);


        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        [Obsolete("此方法已过时")]
        Task<Role> Detail(string code);

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="nameSpace">分组的ID</param>
        /// <returns></returns>
        Task<Role> Detail(string code, string nameSpace = null);

        /// <summary>
        /// 查询角色详情
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="nameSpace">分组的ID</param>
        /// <returns></returns>
        Task<Role> FindByCode(string code, string nameSpace = null);

        /// <summary>
        /// 获取用户池角色列表
        /// </summary>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        [Obsolete("此方法已过时")]
        Task<PaginatedRoles> List(int page = 1, int limit = 10);

        /// <summary>
        /// 获取用户池角色列表
        /// </summary>
        /// <param name="nameSpace">分组的ID</param>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <returns></returns>
        Task<PaginatedRoles> List(string nameSpace, int page = 1, int limit = 10);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除    
        [Obsolete("此方法已过时")]
        Task<PaginatedUsers> ListUsers(string code);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="listUsersOption">用户列表查询条件</param>
        /// <returns></returns>
        Task<PaginatedUsers> ListUsers(string code, ListUsersOption listUsersOption);

        /// <summary>
        /// 批量添加用户到角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        [Obsolete("此方法已过时")]
        Task<CommonMessage> AddUsers(
        string code,
        IEnumerable<string> userIds);

        /// <summary>
        /// 批量添加用户到角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户的ID</param>
        /// <param name="nameSpace">分组的ID</param>
        /// <returns></returns>
        Task<CommonMessage> AddUsers(string code, IEnumerable<string> userIds, string nameSpace = null);

        /// <summary>
        /// 批量移除角色上的用户
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        [Obsolete("此方法已过时")]
        Task<CommonMessage> RemoveUsers(string code, IEnumerable<string> userIds);

        /// <summary>
        /// 批量移除角色上的用户
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <param name="nameSpace">分组的ID</param>
        /// <returns></returns>
        Task<CommonMessage> RemoveUsers(string code, IEnumerable<string> userIds, string nameSpace = null);

        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaginatedPolicyAssignments> ListPolicies(string code, int page = 1, int limit = 10);

        /// <summary>
        /// 批量添加策略
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="policies">策略唯一标识的集合</param>
        /// <returns></returns>
        Task<CommonMessage> AddPolicies(string code, IEnumerable<string> policies);

        /// <summary>
        /// 批量移除策略
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="policies">策略唯一标识的集合</param>
        /// <returns></returns>
        Task<CommonMessage> RemovePolicies(string code, IEnumerable<string> policies);

        /// <summary>
        /// 获取角色被授权的所有资源列表
        /// </summary>
        /// <param name="code">角色 Code</param>
        /// <param name="nameSpace">权限分组的 ID</param>
        /// <param name="resourceType">可选，资源类型，默认会返回所有有权限的资源</param>
        /// <returns></returns>
        Task<Role> ListAuthorizedResources(string code, string nameSpace, ResourceType resourceType = default);

        /// <summary>
        /// 获取某个角色扩展字段列表
        /// </summary>
        /// <param name="roleCode">角色 Code</param>
        /// <returns></returns>
        Task<List<KeyValuePair<string, object>>> GetUdfValue(string roleCode);

        /// <summary>
        /// 获取某个角色某个扩展字段
        /// </summary>
        /// <param name="roleId">角色 Code</param>
        /// <param name="udfKey">角色自定义扩展字段的 Key</param>
        /// <returns></returns>
        Task<KeyValuePair<string, object>> GetSpecificUdfValue(string roleId, string udfKey);

        /// <summary>
        /// 获取多个角色扩展字段列表
        /// </summary>
        /// <param name="roleIds">角色 Code 列表</param>
        /// <returns></returns>
        Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(IEnumerable<string> roleIds);

        /// <summary>
        /// 设置角色扩展字段列表
        /// </summary>
        /// <param name="setUdfValueParam">拓展字段列表信息</param>
        Task<IEnumerable<UserDefinedData>> SetUdfValue(SetUdfValueParam setUdfValueParam);

        /// <summary>
        /// 设置多个角色扩展字段列表
        /// </summary>
        /// <param name="setUdfValueBatchParam">拓展字段信息</param>
        /// <returns></returns>
        Task<IEnumerable<UserDefinedData>> SetUdfValueBatch(IEnumerable<SetUdfValueParam> setUdfValueBatchParam);

        /// <summary>
        /// 移除用户自定义数据
        /// </summary>
        /// <param name="roleId">角色 Code</param>
        /// <param name="key">字段 Key</param>
        /// <returns></returns>
        Task<IEnumerable<UserDefinedData>> RemoveUdfValue(string roleId, string key);
    }
}
