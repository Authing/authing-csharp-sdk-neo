using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Users;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Domain.Model.Management.Department;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class UsersManagementClient : IUsersManagementClient
    {
        private ManagementClient client;
        public UsersManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public async Task<User> Create(
            CreateUserInput userInfo,
            CreateUserOption createUserOption = null)
        {
            //userInfo.Password = client.Encrypt(userInfo.Password);
            var param = new CreateUserParam(userInfo)
            {
                KeepPassword = createUserOption?.KeepPassword,
                ResetPasswordOnFirstLogin = createUserOption?.ResetPasswordOnFirstLogin,
                UserInfo = userInfo,
            };

            var res = await client.Post<CreateUserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="updates">更新信息</param>
        /// <returns></returns>
        public async Task<User> Update(
            string userId,
            UpdateUserInput updates)
        {
            //updates.Password = client.Encrypt(updates.Password);
            var param = new UpdateUserParam(updates)
            {
                Id = userId,
                Input = updates
            };

            var res = await client.Post<UpdateUserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="withCustomData">是否带用户自定义数据</param>
        /// <returns></returns>
        public async Task<User> Detail(
                string userId,
                bool withCustomData = false)
        {
            if (withCustomData)
            {
                var _param = new UserWithCustomDataParam()
                {
                    Id = userId
                };
                var _res = await client.Post<UserWithCustomDataResponse>(_param.CreateRequest());
                return _res.Data.Result;
            }
            var param = new UserParam() { Id = userId };
            await client.GetAccessToken();
            var res = await client.Post<UserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> Delete(
            string userId)
        {
            var param = new DeleteUserParam(userId);
            var res = await client.Post<DeleteUserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户 ID 列表，多个 ID 以英文逗号分隔</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteMany(
            IEnumerable<string> userIds)
        {
            var param = new DeleteUsersParam(userIds);

            var res = await client.Post<DeleteUsersResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 通过 ID 列表批量获取用户信息
        /// </summary>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Batch(
            IEnumerable<string> userIds,
            BatchFetchUserTypes batchFetchUserType = BatchFetchUserTypes.ID)
        {
            var param = new UserBatchParam(userIds)
            {
                Type = batchFetchUserType.ToString().ToUpper()
            };
            var res = await client.Post<UserBatchResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page">当前页数，默认为 1</param>
        /// <param name="limit">每页最大数量，默认为 10</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> List(
            int page = 1,
            int limit = 10)
        {
            var param = new UsersParam()
            {
                Page = page,
                Limit = limit,
            };

            var res = await client.Post<UsersResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取已归档用户列表
        /// </summary>
        /// <param name="page">当前页数，默认为 1</param>
        /// <param name="limit">每页最大数量，默认为 10</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> ListArchivedUsers(
            int page = 1,
            int limit = 10)
        {
            var param = new ArchivedUsersParam()
            {
                Page = page,
                Limit = limit,
            };
            var res = await client.Post<ArchivedUsersResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public async Task<bool?> Exists(ExistsOption options)
        {
            var param = new IsUserExistsParam()
            {
                Username = options.Username,
                Email = options.Email,
                Phone = options.Phone,
                ExternalId = options.ExternalId
            };
            var res = await client.Post<IsUserExistsResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 通过手机号、游戏、用户名查找用户
        /// </summary>
        /// <param name="FindUserOption">选项</param>
        /// <returns></returns>
        public async Task<User> Find(FindUserOption options)
        {
            var param = new FindUserParam()
            {
                Username = options.Username,
                Phone = options.Phone,
                Email = options.Email,
                ExternalId = options.ExternalId,
            };

            var res = await client.Post<FindUserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 模糊搜索用户
        /// </summary>
        /// <param name="query">关键字</param>
        /// <param name="SearchOption">选项</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> Search(
            string query,
            SearchOption option = null)
        {
            if (option == null)
            {
                option = new SearchOption();
            }
            var param = new SearchUserParam(query)
            {
                Page = option.Page,
                Limit = option.Limit,
                Fields = option.Fields,
                DepartmentOpts = option.DepartmentOpts,
                GroupOpts = option.GroupOpts,
                RoleOpts = option.RoleOpts,
            };
            await client.GetAccessToken();
            var res = await client.Post<SearchUserResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 刷新 access token
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<RefreshToken> RefreshToken(string userId)
        {
            var param = new RefreshTokenParam() { Id = userId };

            var res = await client.Post<RefreshTokenResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户分组列表
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedGroups> ListGroups(string userId)
        {
            var param = new GetUserGroupsParam(userId);

            var res = await client.Post<GetUserGroupsResponse>(param.CreateRequest());
            return res.Data.Result.Groups;
        }

        /// <summary>
        /// 加入分组
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="group">分组 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> AddGroup(
            string userId,
            string group)
        {
            var param = new AddUserToGroupParam(new string[] { userId })
            {
                Code = group
            };

            var res = await client.Post<AddUserToGroupResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 退出分组
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="group">分组 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveGroup(
            string userId,
            string group)
        {
            var param = new RemoveUserFromGroupParam(new string[] { userId })
            {
                Code = group
            };

            var res = await client.Post<RemoveUserFromGroupResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedRoles> ListRoles(
            string userId,
            string _namespace = null)
        {
            var param = new GetUserRolesParam(userId)
            {
                Namespace = _namespace
            };
            await client.GetAccessToken();
            var res = await client.Post<GetUserRolesResponse>(param.CreateRequest());
            var user = res.Data.Result;
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            return res.Data.Result.Roles;
        }

        /// <summary>
        /// 批量授权角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roles">用户角色 Code 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> AddRoles(
            string userId,
            IEnumerable<string> roles,
            string _namespace = null)
        {
            var param = new AssignRoleParam()
            {
                UserIds = new string[] { userId },
                RoleCodes = roles,
                Namespace = _namespace,
            };

            var res = await client.Post<AssignRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 批量撤销用户角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roles">用户角色 Code 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveRoles(
            string userId,
            IEnumerable<string> roles,
            string _namespace = null)
        {
            var param = new RevokeRoleParam()
            {
                UserIds = new string[] { userId },
                RoleCodes = roles,
                Namespace = _namespace,
            };
            await client.GetAccessToken();
            var res = await client.Post<RevokeRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户所在组织机构
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedOrgs> ListOrgs(string userId)
        {
            var result = await client.Get<PaginatedOrgs>($"api/v2/users/{userId}/orgs", new ExpnadAllRequest().CreateRequest());
            return result.Data;
        }

        /// <summary>
        /// 获取用户所在部门
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedDepartments> ListDepartment(string userId)
        {
            var param = new GetUserDepartmentsParam(userId);
            var res = await client.Post<GetUserDepartmentsResponse>(param.CreateRequest());
            var user = res.Data.Result;
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            return user.Departments;
        }
    }
}
