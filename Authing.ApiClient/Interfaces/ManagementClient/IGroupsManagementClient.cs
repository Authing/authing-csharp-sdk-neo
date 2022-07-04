using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IGroupsManagementClient
    {
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <param name="name">分组名称</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        Task<Group> Create(
            string code,
            string name,
            string description = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <returns></returns>
        Task<CommonMessage> Delete(string code,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 更新分组信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="newCode"></param>
        /// <returns></returns>
        Task<Group> Update(
            string code,
            string name = null,
            string description = null,
            string newCode = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取分组详情
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <returns></returns>
        Task<Group> Detail(string code,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <returns></returns>
        Task<PaginatedGroups> List(
            int page = 1,
            int limit = 10,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 批量删除分组
        /// </summary>
        /// <param name="codeList">分组唯一标志列表</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteMany(IEnumerable<string> codeList,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取分组用户列表
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <returns></returns>
        /// TODO: 下一个大版本去除
        Task<PaginatedUsers> ListUsers(
            string code,
            int page = 1,
            int limit = 10,AuthingErrorBox authingErrorBox=null);

        Task<PaginatedUsers> ListUsers(
            string code,
            ListUsersOption listUsersOption = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 批量添加用户
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<CommonMessage> AddUsers(
            string code,
            IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 批量移除用户
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<CommonMessage> RemoveUsers(
            string code,
            IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);
        /**
         * 获取用户被授权的所有资源
         */
        Task<PaginatedAuthorizedResources> ListAuthorizedResources(string code, string _namespace, ResourceType resourceType = default,AuthingErrorBox authingErrorBox=null);
    }
}