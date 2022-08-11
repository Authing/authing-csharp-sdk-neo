using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    /// <summary>
    /// 分组管理类
    /// </summary>
    public class GroupsManagementClient : IGroupsManagementClient
    {
        private readonly ManagementClient client;

        /// <summary>
        /// 分组管管理类构造器
        /// </summary>
        /// <param name="client"></param>
        public GroupsManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="code">分组唯一标识符</param>
        /// <param name="name">分组名称</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public async Task<Group> Create(string code, string name, string description = null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new CreateGroupParam(code, name, description);

            var res = await client.RequestCustomDataWithToken<CreateGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="code">分组唯一标识符</param>
        /// <returns></returns>
        public async Task<CommonMessage> Delete(string code,AuthingErrorBox authingErrorBox=null)
        {
            var param = new DeleteGroupsParam(new string[] { code });

            var res = await client.RequestCustomDataWithToken<DeleteGroupsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 更新分组信息
        /// </summary>
        /// <param name="code">分组唯一标识符</param>
        /// <param name="name">分组名称</param>
        /// <param name="description">描述</param>
        /// <param name="newCode">新分组唯一标识符</param>
        /// <returns></returns>
        public async Task<Group> Update(string code, string name = null, string description = null, string newCode = null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UpdateGroupParam(code)
            {
                Name = name,
                Description = description,
                NewCode = newCode,
            };

            var res = await client.RequestCustomDataWithToken<UpdateGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 获取分组详情
        /// </summary>
        /// <param name="code">分组唯一标识符</param>
        /// <returns></returns>
        public async Task<Group> Detail(string code,AuthingErrorBox authingErrorBox=null)
        {
            var param = new GroupParam(code);

            var res = await client.RequestCustomDataWithToken<GroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <returns></returns>
        public async Task<PaginatedGroups> List(int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            var param = new GroupsParam()
            {
                Page = page,
                Limit = limit,
            };

            var res = await client.RequestCustomDataWithToken<GroupsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 批量删除分组
        /// </summary>
        /// <param name="codeList">分组唯一标识符列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteMany(IEnumerable<string> codeList,AuthingErrorBox authingErrorBox=null)
        {
            var param = new DeleteGroupsParam(codeList);

            var res = await client.RequestCustomDataWithToken<DeleteGroupsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 获取分组用户列表
        /// </summary>
        /// <param name="code">分组唯一标志</param>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <returns></returns>
        /// TODO: 下一个大版本去除
        [Obsolete("旧版本")]
        public async Task<PaginatedUsers> ListUsers(string code, int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            var param = new GroupWithUsersParam(code)
            {
                Page = page,
                Limit = limit,
            };

            var res = await client.RequestCustomDataWithToken<GroupWithUsersResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result.Users ?? null;
        }

        public async Task<PaginatedUsers> ListUsers(string code, ListUsersOption listUsersOption = null,AuthingErrorBox authingErrorBox=null)
        {
            if (listUsersOption != null && !listUsersOption.WithCustomData)
            {
                var _param = new RoleWithUsersParam(code)
                {
                    Code = code,
                    Page = listUsersOption?.Page,
                    Limit = listUsersOption?.Limit,
                };
                var _res = await client.RequestCustomDataWithToken<GroupWithUsersResponse>(_param.CreateRequest()).ConfigureAwait(false);
                ErrorHelper.LoadError(_res, authingErrorBox);
                return _res.Data?.Result?.Users ?? null;
            }
            else
            {
                var _param = new RoleWithUsersWithCustomDataParam(code)
                {
                    Code = code,
                    Page = listUsersOption?.Page,
                    Limit = listUsersOption?.Limit
                };
                var _res = await client.RequestCustomDataWithToken<GroupWithUsersWithCustomDataResponse>(_param.CreateRequest()).ConfigureAwait(false);
                ErrorHelper.LoadError(_res, authingErrorBox);
                return _res.Data?.Result?.Users ?? null;
            }
        }

        /// <summary>
        /// 批量添加用户
        /// </summary>
        /// <param name="code">分组唯一标识符标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> AddUsers(string code, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null)
        {
            var param = new AddUserToGroupParam(userIds)
            {
                Code = code,
            };

            var res = await client.RequestCustomDataWithToken<AddUserToGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 批量移除用户
        /// </summary>
        /// <param name="code">分组唯一标识符</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveUsers(string code, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RemoveUserFromGroupParam(userIds)
            {
                Code = code,
            };

            var res = await client.RequestCustomDataWithToken<RemoveUserFromGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 获取分组被授权的所有资源列表
        /// </summary>
        /// <param name="code">分组 </param>
        /// <param name="_namespace">权限分组的 Code</param>
        /// <param name="resourceType">
        /// 可选，资源类型，默认会返回所有有权限的资源，现有资源类型如下：
        /// DATA：数据类型；
        /// API：API 类型数据；
        /// MENU：菜单类型数据；
        /// BUTTON：按钮类型数据。
        /// </param>
        /// <returns></returns>
        public async Task<PaginatedAuthorizedResources> ListAuthorizedResources(string code, string _namespace, ResourceType resourceType = default,AuthingErrorBox authingErrorBox=null)
        {
            var param = new ListGroupAuthorizedResourcesParam(code)
            {
                Namespace = _namespace,
                ResourceType = resourceType.ToString().ToUpper(),
            };
            var res = await client.RequestCustomDataWithToken<ListGroupAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var group = res.Data?.Result;
            if (group == null)
            {
                throw new Exception("分组不存在");
            }
            var authorizedResources = group.AuthorizedResources;
            return authorizedResources ?? null;
        }
    }
}