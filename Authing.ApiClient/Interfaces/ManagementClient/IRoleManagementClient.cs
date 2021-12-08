using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
      public interface RolesManagementClient
    {

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="description">角色描述</param>
        /// <param name="parentCode">父角色唯一标志</param>
        /// <returns></returns>
            Task<Role> Create( string code, string description = null,string parentCode = null);


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO: 在下一个大版本中去除
          Task<CommonMessage> Delete(string code);

          Task<CommonMessage> Delete(
            string code,
            string nameSpace = null);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="codeList">角色 code 列表</param>
        /// <returns></returns>
        /// TODO： 在下一个大版本中去除
          Task<CommonMessage> DeleteMany(
            IEnumerable<string> codeList);

          Task<CommonMessage> DeleteMany(
            IEnumerable<string> codeList,
            string nameSpace = null);

        /// <summary>
        /// 修改角色资料
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="description">角色描述</param>
        /// <param name="newCode">新的 code</param>
        /// <returns></returns>
        /// TODO: 下一个大版本中去除
            Task<Role> Update(
            string code,
            string description = null,
            string newCode = null);

            Task<Role> Update(
            string code,
            UpdateRoleOptions updateRoleOptions);


        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
            Task<Role> Detail(
            string code);
            Task<Role> Detail(
            string code,
            string nameSpace = null);

            Task<Role> FindByCode(
            string code,
            string nameSpace = null);

        /// <summary>
        /// 获取用户池角色列表
        /// </summary>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
            Task<PaginatedRoles> List(
            int page = 1,
            int limit = 10);

            Task<PaginatedRoles> List(
            string nameSpace,
            int page = 1,
            int limit = 10);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除            
            Task<PaginatedUsers> ListUsers(
            string code);

            Task<PaginatedUsers> ListUsers(
            string code,
            ListUsersOption listUsersOption);

        /// <summary>
        /// 批量添加用户到角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
            Task<CommonMessage> AddUsers(
            string code,
            IEnumerable<string> userIds);

            Task<CommonMessage> AddUsers(
            string code,
            IEnumerable<string> userIds,
            string nameSpace = null);

        /// <summary>
        /// 批量移除角色上的用户
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
            Task<CommonMessage> RemoveUsers(
            string code,
            IEnumerable<string> userIds);

            Task<CommonMessage> RemoveUsers(
            string code,
            IEnumerable<string> userIds,
            string nameSpace = null
        );

        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
            Task<PaginatedPolicyAssignments> ListPolicies(
            string code,
            int page = 1,
            int limit = 10
        );

        /// <summary>
        /// 批量添加策略
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="policies">策略 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
            Task<CommonMessage> AddPolicies(
            string code,
            IEnumerable<string> policies);

        /// <summary>
        /// 批量移除策略
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="policies">策略 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
            Task<CommonMessage> RemovePolicies(
            string code,
            IEnumerable<string> policies);
            Task<Role> ListAuthorizedResources(string code, string nameSpace, ResourceType resourceType = default);

            Task<List<KeyValuePair<string, object>>> GetUdfValue(string roleId);

            Task<KeyValuePair<string, object>> GetSpecificUdfValue(string roleId, string udfKey);

            Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(IEnumerable<string> roleIds);
            void SetUdfValue(SetUdfValueParam setUdfValueParam);

            void SetUdfValueBatch(IEnumerable<SetUdfValueParam> setUdfValueBatchParam);

            void RemoveUdfValue(string roleId, string key);
    }
}
