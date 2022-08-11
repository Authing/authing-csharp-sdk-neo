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
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.ApiClient.Domain.Utils;
using System.Linq;
using System.Net.Http;
using Authing.ApiClient.Domain.Model.GraphQLParam;
using Authing.ApiClient.Extensions;
using Authing.Library.Domain.Model.Exceptions;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Model.V3Model.Management.Models;

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
        public async Task<User> Create(CreateUserInput userInfo,
                                       CreateUserOption createUserOption = null,
                                       AuthingErrorBox authingErrorBox = null)
        {
            userInfo.Password = EncryptHelper.RsaEncryptWithPublic(userInfo.Password, client.PublicKey);
            var param = new CreateUserParam(userInfo)
            {
                KeepPassword = createUserOption?.KeepPassword,
                ResetPasswordOnFirstLogin = createUserOption?.ResetPasswordOnFirstLogin,
                UserInfo = userInfo,
            };

            var res = await client.RequestCustomDataWithToken<CreateUserResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="updates">更新信息</param>
        /// <returns></returns>
        public async Task<User> Update(string userId,
                                       UpdateUserInput updates,
                                       AuthingErrorBox authingErrorBox = null)
        {
            //updates.Password = client.Encrypt(updates.Password);
            var param = new UpdateUserParam(updates)
            {
                Id = userId,
                Input = updates
            };

            if (!string.IsNullOrWhiteSpace(param.Input.Password))
            {
                param.Input.Password = EncryptHelper.RsaEncryptWithPublic(param.Input.Password, client.PublicKey);
            }

            var res = await client.RequestCustomDataWithToken<UpdateUserResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="withCustomData">是否带用户自定义数据</param>
        /// <returns></returns>
        public async Task<User> Detail(string userId,
                                       bool withCustomData = false,
                                       AuthingErrorBox authingErrorBox = null)
        {
            if (withCustomData)
            {
                var _param = new UserWithCustomDataParam()
                {
                    Id = userId
                };
                var _res = await client.RequestCustomDataWithToken<UserWithCustomDataResponse>(_param.CreateRequest()).ConfigureAwait(false);
                ErrorHelper.LoadError(_res, authingErrorBox);
                return _res.Data.Result;
            }
            var param = new UserParam() { Id = userId };
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.RequestCustomDataWithToken<UserResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> Delete(string userId,
                                                AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteUserParam(userId);
            var res = await client.RequestCustomDataWithToken<DeleteUserResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户 ID 列表，多个 ID 以英文逗号分隔</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteMany(IEnumerable<string> userIds,
                                                    AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteUsersParam(userIds);

            var res = await client.RequestCustomDataWithToken<DeleteUsersResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过 ID 列表批量获取用户信息
        /// </summary>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Batch(IEnumerable<string> userIds,
                                                   BatchFetchUserTypes batchFetchUserType = BatchFetchUserTypes.id,
                                                   AuthingErrorBox authingErrorBox = null)
        {
            var param = new UserBatchParam(userIds)
            {
                Type = batchFetchUserType.ToString()
            };
            var res = await client.RequestCustomDataWithToken<UserBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page">当前页数，默认为 1</param>
        /// <param name="limit">每页最大数量，默认为 10</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> List(int page = 1,
                                               int limit = 10,
                                               AuthingErrorBox authingErrorBox = null)
        {
            var param = new UsersParam()
            {
                Page = page,
                Limit = limit,
            };

            var res = await client.RequestCustomDataWithToken<UsersResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取已归档用户列表
        /// </summary>
        /// <param name="page">当前页数，默认为 1</param>
        /// <param name="limit">每页最大数量，默认为 10</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> ListArchivedUsers(int page = 1,
                                                            int limit = 10,
                                                            AuthingErrorBox authingErrorBox = null)
        {
            var param = new ArchivedUsersParam()
            {
                Page = page,
                Limit = limit,
            };
            var res = await client.RequestCustomDataWithToken<ArchivedUsersResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public async Task<bool?> Exists(ExistsOption options, AuthingErrorBox authingErrorBox = null)
        {
            var param = new IsUserExistsParam()
            {
                Username = options.Username,
                Email = options.Email,
                Phone = options.Phone,
                ExternalId = options.ExternalId
            };
            var res = await client.RequestCustomDataWithToken<IsUserExistsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 通过手机号、邮箱、用户名查找用户
        /// </summary>
        /// <param name="FindUserOption">选项</param>
        /// <returns></returns>
        public async Task<User> Find(FindUserOption options, AuthingErrorBox authingErrorBox = null)
        {
            var param = new FindUserParam()
            {
                Username = options.Username,
                Phone = options.Phone,
                Email = options.Email,
                ExternalId = options.ExternalId,
            };

            var res = await client.RequestCustomDataWithToken<FindUserResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 模糊搜索用户
        /// </summary>
        /// <param name="query">关键字</param>
        /// <param name="SearchOption">选项</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> Search(string query,
                                                 SearchOption option = null,
                                                 AuthingErrorBox authingErrorBox = null)
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
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.RequestCustomDataWithToken<SearchUserResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 刷新 access token
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<RefreshToken> RefreshToken(string userId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new RefreshTokenParam() { Id = userId };

            var res = await client.RequestCustomDataWithToken<RefreshTokenResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取用户分组列表
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedGroups> ListGroups(string userId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new GetUserGroupsParam(userId);

            var res = await client.RequestCustomDataWithToken<GetUserGroupsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result.Groups;
        }

        /// <summary>
        /// 加入分组
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="group">分组 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> AddGroup(string userId,
                                                  string group,
                                                  AuthingErrorBox authingErrorBox = null)
        {
            var param = new AddUserToGroupParam(new string[] { userId })
            {
                Code = group
            };

            var res = await client.RequestCustomDataWithToken<AddUserToGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 退出分组
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="group">分组 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveGroup(string userId,
                                                     string group,
                                                     AuthingErrorBox authingErrorBox = null)
        {
            var param = new RemoveUserFromGroupParam(new string[] { userId })
            {
                Code = group
            };

            var res = await client.RequestCustomDataWithToken<RemoveUserFromGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedRoles> ListRoles(string userId,
                                                    string _namespace = null,
                                                    AuthingErrorBox authingErrorBox = null)
        {
            var param = new GetUserRolesParam(userId)
            {
                Namespace = _namespace
            };
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.RequestCustomDataWithToken<GetUserRolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var user = res.Data.Result;
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            return res.Data?.Result.Roles;
        }

        /// <summary>
        /// 批量授权角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roles">用户角色 Code 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> AddRoles(string userId,
                                                  IEnumerable<string> roles,
                                                  string _namespace = null,
                                                  AuthingErrorBox authingErrorBox = null)
        {
            var param = new AssignRoleParam()
            {
                UserIds = new string[] { userId },
                RoleCodes = roles,
                Namespace = _namespace,
            };

            var res = await client.RequestCustomDataWithToken<AssignRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 批量撤销用户角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roles">用户角色 Code 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveRoles(string userId,
                                                     IEnumerable<string> roles,
                                                     string _namespace = null,
                                                     AuthingErrorBox authingErrorBox = null)
        {
            var param = new RevokeRoleParam()
            {
                UserIds = new string[] { userId },
                RoleCodes = roles,
                Namespace = _namespace,
            };
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.RequestCustomDataWithToken<RevokeRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取用户所在组织机构
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedOrgsAndNodes> ListOrgs(string userId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<IEnumerable<IEnumerable<OrgAndNode>>>($"api/v2/users/{userId}/orgs", method: System.Net.Http.HttpMethod.Get, contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            //var res = await client.Get<ListOrgsResponse>($"api/v2/users/{userId}/orgs", new GraphQLRequest()).ConfigureAwait(false);
            if (res.Code == 200)
            {
                var result = new PaginatedOrgsAndNodes()
                {
                    TotalCount = res.Data.Count(),
                    List = res.Data
                };
                return result;
            }
            else
            {
                var result = new PaginatedOrgsAndNodes()
                {
                    TotalCount = 0,
                    List = null
                };
                return result;
            }
        }

        /// <summary>
        /// 获取用户所在部门
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedDepartments> ListDepartment(string userId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new GetUserDepartmentsParam(userId);
            var res = await client.RequestCustomDataWithToken<GetUserDepartmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var user = res.Data.Result;
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            return user.Departments;
        }

        /// <summary>
        /// 获取用户被授权的所有资源
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="_namespace">资源分组</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<PaginatedAuthorizedResources> ListAuthorizedResources(string userId,
                                                                                string _namespace,
                                                                                ListAuthorizedResourcesOption option = null,
                                                                                AuthingErrorBox authingErrorBox = null)
        {
            var param = new ListUserAuthorizedResourcesParam(userId)
            {
                Namespace = _namespace,
            };
            if (option != null && option.ResourceType != null)
            {
                param.ResourceType = option.ResourceType.ToString().ToUpper();
            }
            var res = await client.RequestCustomDataWithToken<ListUserAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var user = res.Data.Result;
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }

            return user.AuthorizedResources;
        }

        /// <summary>
        /// 获取某个用户的所有自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<List<KeyValuePair<string, object>>> GetUdfValue(string userId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new UdvParam(UdfTargetType.USER, userId);
            var res = await client.RequestCustomDataWithToken<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return AuthingUtils.ConverUdvToKeyValuePair(res.Data.Result);
        }

        /// <summary>
        /// 批量获取多个用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(string[] userIds, AuthingErrorBox authingErrorBox = null)
        {
            if (userIds.Length < 1)
            {
                throw new Exception("empty user id list");
            }
            var param = new UdfValueBatchParam(UdfTargetType.USER, userIds);
            var res = await client.RequestCustomDataWithToken<UdfValueBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var dic = new Dictionary<string, List<KeyValuePair<string, object>>>();
            foreach (var item in res.Data.Result)
            {
                dic.Add(item.TargetId, AuthingUtils.ConverUdvToKeyValuePair(item.Data));
            }
            return dic;
        }

        /// <summary>
        /// 设置某个用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserDefinedData>> SetUdfValue(string userId, KeyValueDictionary data, AuthingErrorBox authingErrorBox = null)
        {
            if (data.Count() < 1)
            {
                throw new Exception("empty udf value list");
            }
            var param = new SetUdvBatchParam(UdfTargetType.USER, userId)
            {
                UdvList = data.ToList().Select(item => new UserDefinedDataInput(item.Key)
                {
                    Value = item.Value.ConvertJson()
                }),
            };
            var res = await client.RequestCustomDataWithToken<SetUdvBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        /// <summary>
        /// 批量设置自定义数据
        /// </summary>
        /// <param name="setUdfValueBatchInput"></param>
        /// <returns></returns>
        public async Task<CommonMessage> SetUdfValueBatch(SetUserUdfValueBatchParam[] setUdfValueBatchInput, AuthingErrorBox authingErrorBox = null)
        {
            if (setUdfValueBatchInput.Length < 1)
            {
                throw new Exception("empty input list");
            }
            var param = new List<SetUdfValueBatchInput>();
            setUdfValueBatchInput.ToList().ForEach(
                item =>
                    item.Data.ToList().ForEach(
                        keyValue =>
                        param.Add(
                            new SetUdfValueBatchInput(
                                item.UserId,
                                keyValue.Key,
                                keyValue.Value.ConvertJson()
                            )
                    )
            ));
            var _param = new SetUdfValueBatchParam(UdfTargetType.USER, param);
            var res = await client.RequestCustomDataWithToken<SetUdfValueBatchResponse>(_param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        /// <summary>
        /// 清除用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveUdfValue(string userId, string key, AuthingErrorBox authingErrorBox = null)
        {
            var param = new RemoveUdvParam(UdfTargetType.USER, userId, key);
            var res = await client.RequestCustomDataWithToken<SetUdfValueBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roleCode">角色 Code</param>
        /// <param name="_namespace">权限分组 ID</param>
        /// <returns></returns>
        public async Task<bool> hasRole(string userId, string roleCode, string _namespace = null, AuthingErrorBox authingErrorBox = null)
        {
            var roleList = await ListRoles(userId, _namespace, authingErrorBox).ConfigureAwait(false);

            if (roleList.TotalCount < 1)
            {
                return false;
            }

            var hasRole = roleList.List.Where(item => item.Code == roleCode).ToList();

            return hasRole.Count > 0;
        }

        /// <summary>
        /// 强制一批用户下线
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roleCode">角色 Code</param>
        /// <param name="_namespace">权限分组 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> Kick(IEnumerable<string> userIds, AuthingErrorBox authingErrorBox = null)
        {
            var result = await client.RequestCustomDataWithToken<CommonMessage>("api/v2/users/kick",
                new Dictionary<string, object>
                {
                    { nameof(userIds), userIds }
                }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return new CommonMessage() { Code = result.Code, Message = result.Message };
            //await client.Host.AppendPathSegment("api/v2/users/kick").WithHeaders(client.GetAuthHeaders()).WithOAuthBearerToken(client.AccessToken).PostJsonAsync(new
            //{
            //    userIds
            //}).ReceiveJson<CommonMessage>();
            //var res = await client.PostRaw<CommonMessage>("api/v2/users/kick", new Dictionary<string, object>() { { "userIds", userIds } }).ConfigureAwait(false);
            //Console.WriteLine(res);
            //return new CommonMessage
            //{
            //    Code = 200,
            //    Message = "强制下线成功"
            //};
        }

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <param name="logoutParam">选项</param>
        /// <returns></returns>
        public async Task<CommonMessage> Logout(LogoutParam logoutParam, AuthingErrorBox authingErrorBox = null)
        {
            if (logoutParam.UserId == null)
            {
                throw new Exception("请传入 options.userId，内容为要下线的用户 ID");
            }

            var result = await client.RequestCustomDataWithToken<CommonMessage>("logout", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return new CommonMessage
            {
                Code = result.Code,
                Message = result.Message
            };
        }

        /// <summary>
        /// 查询用户的登录状态
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="appId">应用 ID</param>
        /// <param name="devicdId">选项</param>
        /// <returns></returns>
        public async Task<CheckLoginStatusRes> CheckLoginStatus(string userId, string appId = null, string devicdId = null, AuthingErrorBox authingErrorBox = null)
        {
            var query = $"?userId={userId}";
            if (appId != null)
            {
                query += $"&appId={appId}";
            }
            if (devicdId != null)
            {
                query += $"&devicdId={devicdId}";
            }
            var res = await client.RequestCustomDataWithToken<CheckLoginStatusRes>($"api/v2/users/login-status{query}").ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 审计日志列表
        /// </summary>
        /// <param name="listUserActionsParam">选项</param>
        /// <returns></returns>
        public async Task<ListUserActionsRealRes> ListUserActions(ListUserActionsParam listUserActionsParam = null, AuthingErrorBox authingErrorBox = null)
        {
            var query = "";
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("ExcludeNonAppRecords") != null)
            {
                query += $"?exclude_non_app_records={listUserActionsParam.ExcludeNonAppRecords}";
            }
            else
            {
                query += $"?exclude_non_app_records=1";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("ClientIp") != null)
            {
                query += $"&clientip={listUserActionsParam.ClientIp}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("OperationNames") != null)
            {
                query += $"&operation_name={listUserActionsParam.OperationNames}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("UserIds") != null)
            {
                query += $"&operator_arn={listUserActionsParam.UserIds.Select(userId => $"arn:cn:authing:{client.Options.UserPoolId}:user:${userId}").ToArray()}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("Page") != null)
            {
                query += $"&page={listUserActionsParam.Page}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("Limit") != null)
            {
                query += $"&limit={listUserActionsParam.Limit}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("AppIds") != null)
            {
                query += $"&app_id={listUserActionsParam.AppIds}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("Start") != null)
            {
                query += $"&start={listUserActionsParam.Start}";
            }
            if (listUserActionsParam != null && listUserActionsParam.GetType().GetProperty("End") != null)
            {
                query += $"&end={listUserActionsParam.End}";
            }
            var res = await client.RequestCustomDataWithToken<ListUserActionsRes>("api/v2/analysis/user-action", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            if (res.Data.TotalCount == 0)
            {
                var resEmpty = new ListUserActionsRealRes()
                {
                    TotalCount = 0,
                    List = new UserActionRes[0]
                };
                return resEmpty;
            }
            var list = res.Data.List;
            var resList = list.Select(log => new UserActionRes
            {
                UserPoolId = log.userPoolId,
                Id = log.userId,
                UserName = log.userName,
                CityName = log.geoip.CityName,
                RegionName = log.geoip.RegionName,
                ClientIp = log.geoip.Ip,
                OperationDesc = log.eventDetails,
                OperationName = log.eventType,
                TimeStamp = log.timestamp,
                AppId = log.appId,
                AppName = log.appName
            }).ToArray();
            var resReal = new ListUserActionsRealRes()
            {
                TotalCount = res.Data.TotalCount,
                List = resList
            };
            return resReal;
        }

        /// <summary>
        /// 发送首次登录验证邮件
        /// </summary>
        /// <param name="sendFirstLoginVerifyEmailParam">选项</param>
        /// <returns></returns>
        public async Task<SendFirstLoginVerifyEmailResponse> SendFirstLoginVerifyEmail(SendFirstLoginVerifyEmailParam sendFirstLoginVerifyEmailParam, AuthingErrorBox authingErrorBox = null)
        {
            var param = sendFirstLoginVerifyEmailParam;
            var res = await client.RequestCustomDataWithToken<SendFirstLoginVerifyEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 批量导入用户
        /// </summary>
        /// <param name="userInfos">用户信息列表</param>
        /// <returns></returns>
        public async Task<CreateUsersRes> CreateUsers(IEnumerable<CreateUserInput> userInfos, AuthingErrorBox authingErrorBox = null)
        {
            if (userInfos.Count() > 100)
            {
                return new CreateUsersRes()
                {
                    Code = 500,
                    Message = "每次创建用户不能多于 100 个"
                };
            }
            foreach (var item in userInfos)
            {
                if (item.Gender == null)
                {
                    item.Gender = "U";
                }

                if (string.IsNullOrWhiteSpace(item.Password))
                {
                    item.Password = EncryptHelper.RsaEncryptWithPublic(item.Password, client.PublicKey);
                }
            }
            var res = await client.RequestCustomDataWithToken<CreateUserResult[]>("api/v2/users/create/batch", new Dictionary<string, object>() {
                { "users", userInfos }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return new CreateUsersRes()
            {
                Code = res.Code,
                Message = res.Message,
                Data = res.Data
            };
        }

        /// <summary>
        /// 获取用户所在租户
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<User> GetUserTenants(string userId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<User>($"api/v2/users/{userId}/tenants",method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 给用户绑定一个身份
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> LinkIdentity(LinkIdentityOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, object>() {
                { "userId", option.UserId },
                { "userIdInIdp", option.UserIdInIdp },
                { "isSocial", option.IsSocial },
                { "identifier", option.Identifier },
            };
            if (option.Type != null)
            {
                body.Add("type", option.Type);
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>("api/v2/users/identity/link", body.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 解除用户某个身份源下的所有身份
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> UnlinkIdentity(UnlinkIdentityOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, object>() {
                { "userId", option.UserId },
                { "isSocial", option.IsSocial },
                { "identifier", option.Identifier },
            };
            if (option.Type != null)
            {
                body.Add("type", option.Type);
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>("api/v2/users/identity/unlink", body.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        ///<summary>
        /// 获取用户信息
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        /// <param name="withCustomData">是否获取自定义数据</param>
        /// <param name="withIdentities">是否获取 identities</param>
        /// <param name="withDepartmentIds">是否获取部门 ID 列表</param>
        ///<returns>UserSingleRespDto</returns>
        public async Task<UserSingleRespDto> GetUser(string userId, string userIdType = "user_id", bool withCustomData = false, bool withIdentities = false, bool withDepartmentIds = false)
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user", new Dictionary<string, object> {
            {"userId",userId },
            {"userIdType",userIdType },
            {"withCustomData",withCustomData },
            {"withIdentities",withIdentities },
            {"withDepartmentIds",withDepartmentIds },
        }).ConfigureAwait(false);
            UserSingleRespDto result = client.JsonService.DeserializeObject<UserSingleRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 批量获取用户信息
        ///</summary>
        /// <param name="userIds">用户 ID 数组</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        /// <param name="withCustomData">是否获取自定义数据</param>
        /// <param name="withIdentities">是否获取 identities</param>
        /// <param name="withDepartmentIds">是否获取部门 ID 列表</param>
        ///<returns>UserListRespDto</returns>
        public async Task<UserListRespDto> GetUserBatch(string userIds, string userIdType = "user_id", bool withCustomData = false, bool withIdentities = false, bool withDepartmentIds = false)
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-batch", new Dictionary<string, object> {
            {"userIds",userIds },
            {"userIdType",userIdType },
            {"withCustomData",withCustomData },
            {"withIdentities",withIdentities },
            {"withDepartmentIds",withDepartmentIds },
        }).ConfigureAwait(false);
            UserListRespDto result = client.JsonService.DeserializeObject<UserListRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户列表
        ///</summary>
        /// <param name="page">当前页数，从 1 开始</param>
        /// <param name="limit">每页数目，最大不能超过 50，默认为 10</param>
        /// <param name="status">账户当前状态，如 已停用、已离职、正常状态、已归档</param>
        /// <param name="updatedAtStart">用户创建、修改开始时间，为精确到秒的 UNIX 时间戳；支持获取从某一段时间之后的增量数据</param>
        /// <param name="updatedAtEnd">用户创建、修改终止时间，为精确到秒的 UNIX 时间戳；支持获取某一段时间内的增量数据。默认为当前时间</param>
        /// <param name="withCustomData">是否获取自定义数据</param>
        /// <param name="withIdentities">是否获取 identities</param>
        /// <param name="withDepartmentIds">是否获取部门 ID 列表</param>
        ///<returns>UserPaginatedRespDto</returns>
        public async Task<UserPaginatedRespDto> ListUsers(long page = 1, long limit = 10, string status = null, long updatedAtStart = 0, long updatedAtEnd = 0, bool withCustomData = false, bool withIdentities = false, bool withDepartmentIds = false)
        {
            string httpResponse = await client.Request("GET", "/api/v3/list-users", new Dictionary<string, object> {
            {"page",page },
            {"limit",limit },
            {"status",status },
            {"updatedAtStart",updatedAtStart },
            {"updatedAtEnd",updatedAtEnd },
            {"withCustomData",withCustomData },
            {"withIdentities",withIdentities },
            {"withDepartmentIds",withDepartmentIds },
        }).ConfigureAwait(false);
            UserPaginatedRespDto result = client.JsonService.DeserializeObject<UserPaginatedRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户的外部身份源
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>IdentityListRespDto</returns>
        public async Task<IdentityListRespDto> GetUserIdentities(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-identities", new Dictionary<string, object> {
            {"userId",userId },
            {"userIdType",userIdType },
        }).ConfigureAwait(false);
            IdentityListRespDto result = client.JsonService.DeserializeObject<IdentityListRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户角色列表
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        /// <param name="nameSpace">所属权限分组的 code</param>
        ///<returns>RolePaginatedRespDto</returns>
        public async Task<RolePaginatedRespDto> GetUserRoles(string userId, string userIdType = "user_id", string nameSpace = null)
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-roles", new Dictionary<string, object> {
            {"userId",userId },
            {"userIdType",userIdType },
            {"namespace",nameSpace },
        }).ConfigureAwait(false);
            RolePaginatedRespDto result = client.JsonService.DeserializeObject<RolePaginatedRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户实名认证信息
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>PrincipalAuthenticationInfoPaginatedRespDto</returns>
        public async Task<PrincipalAuthenticationInfoPaginatedRespDto> GetUserPrincipalAuthenticationInfo(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-principal-authentication-info", new Dictionary<string, object> {
            {"userId",userId },
            {"userIdType",userIdType },
        }).ConfigureAwait(false);
            PrincipalAuthenticationInfoPaginatedRespDto result = client.JsonService.DeserializeObject<PrincipalAuthenticationInfoPaginatedRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 删除用户实名认证信息
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>IsSuccessRespDto</returns>
        public async Task<IsSuccessRespDto> ResetUserPrincipalAuthenticationInfo(ResetUserPrincipalAuthenticationInfoDto requestBody
    )
        {
            string httpResponse = await client.Request("POST", "/api/v3/reset-user-principal-authentication-info", requestBody).ConfigureAwait(false);
            IsSuccessRespDto result = client.JsonService.DeserializeObject<IsSuccessRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户部门列表
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        /// <param name="page">当前页数，从 1 开始</param>
        /// <param name="limit">每页数目，最大不能超过 50，默认为 10</param>
        /// <param name="withCustomData">是否获取自定义数据</param>
        /// <param name="sortBy">排序依据，如 部门创建时间、加入部门时间、部门名称、部门标志符</param>
        /// <param name="orderBy">增序或降序</param>
        ///<returns>UserDepartmentPaginatedRespDto</returns>
        public async Task<UserDepartmentPaginatedRespDto> GetUserDepartments(string userId, string userIdType = "user_id", long page = 1, long limit = 10, bool withCustomData = false, string sortBy = "JoinDepartmentAt", string orderBy = "Desc")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-departments", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
        {"page",page },
        {"limit",limit },
        {"withCustomData",withCustomData },
        {"sortBy",sortBy },
        {"orderBy",orderBy },
    }).ConfigureAwait(false);
            UserDepartmentPaginatedRespDto result = client.JsonService.DeserializeObject<UserDepartmentPaginatedRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 设置用户所在部门
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>IsSuccessRespDto</returns>
        public async Task<IsSuccessRespDto> SetUserDepartments(SetUserDepartmentsDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/set-user-departments", requestBody).ConfigureAwait(false);
            IsSuccessRespDto result = client.JsonService.DeserializeObject<IsSuccessRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户分组列表
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>GroupPaginatedRespDto</returns>
        public async Task<GroupPaginatedRespDto> GetUserGroups(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-groups", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
    }).ConfigureAwait(false);
            GroupPaginatedRespDto result = client.JsonService.DeserializeObject<GroupPaginatedRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 删除用户
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>IsSuccessRespDto</returns>
        public async Task<IsSuccessRespDto> DeleteUsersBatch(DeleteUsersBatchDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/delete-users-batch", requestBody).ConfigureAwait(false);
            IsSuccessRespDto result = client.JsonService.DeserializeObject<IsSuccessRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户 MFA 绑定信息
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>UserMfaSingleRespDto</returns>
        public async Task<UserMfaSingleRespDto> GetUserMfaInfo(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-mfa-info", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
    }).ConfigureAwait(false);
            UserMfaSingleRespDto result = client.JsonService.DeserializeObject<UserMfaSingleRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取已归档的用户列表
        ///</summary>
        /// <param name="page">当前页数，从 1 开始</param>
        /// <param name="limit">每页数目，最大不能超过 50，默认为 10</param>
        /// <param name="startAt">开始时间，为精确到秒的 UNIX 时间戳，默认不指定</param>
        ///<returns>ListArchivedUsersSingleRespDto</returns>
        public async Task<ListArchivedUsersSingleRespDto> ListArchivedUsers(long page = 1, long limit = 10, long startAt = 0)
        {
            string httpResponse = await client.Request("GET", "/api/v3/list-archived-users", new Dictionary<string, object> {
        {"page",page },
        {"limit",limit },
        {"startAt",startAt },
    }).ConfigureAwait(false);
            ListArchivedUsersSingleRespDto result = client.JsonService.DeserializeObject<ListArchivedUsersSingleRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 强制下线用户
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>IsSuccessRespDto</returns>
        public async Task<IsSuccessRespDto> KickUsers(KickUsersDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/kick-users", requestBody).ConfigureAwait(false);
            IsSuccessRespDto result = client.JsonService.DeserializeObject<IsSuccessRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 判断用户是否存在
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>IsUserExistsRespDto</returns>
        public async Task<IsUserExistsRespDto> IsUserExists(IsUserExistsReqDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/is-user-exists", requestBody).ConfigureAwait(false);
            IsUserExistsRespDto result = client.JsonService.DeserializeObject<IsUserExistsRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 创建用户
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>UserSingleRespDto</returns>
        public async Task<UserSingleRespDto> CreateUser(CreateUserReqDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/create-user", requestBody).ConfigureAwait(false);
            UserSingleRespDto result = client.JsonService.DeserializeObject<UserSingleRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 批量创建用户
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>UserListRespDto</returns>
        public async Task<UserListRespDto> CreateUsersBatch(CreateUserBatchReqDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/create-users-batch", requestBody).ConfigureAwait(false);
            UserListRespDto result = client.JsonService.DeserializeObject<UserListRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 修改用户资料
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>UserSingleRespDto</returns>
        public async Task<UserSingleRespDto> UpdateUser(UpdateUserReqDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/update-user", requestBody).ConfigureAwait(false);
            UserSingleRespDto result = client.JsonService.DeserializeObject<UserSingleRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户可访问的应用
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>AppListRespDto</returns>
        public async Task<AppListRespDto> GetUserAccessibleApps(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-accessible-apps", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
    }).ConfigureAwait(false);
            AppListRespDto result = client.JsonService.DeserializeObject<AppListRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户授权的应用
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>AppListRespDto</returns>
        public async Task<AppListRespDto> GetUserAuthorizedApps(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-authorized-apps", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
    }).ConfigureAwait(false);
            AppListRespDto result = client.JsonService.DeserializeObject<AppListRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 判断用户是否有某个角色
        ///</summary>
        /// <param name="requestBody"></param>
        ///<returns>HasAnyRoleRespDto</returns>
        public async Task<HasAnyRoleRespDto> HasAnyRole(HasAnyRoleReqDto requestBody
        )
        {
            string httpResponse = await client.Request("POST", "/api/v3/has-any-role", requestBody).ConfigureAwait(false);
            HasAnyRoleRespDto result = client.JsonService.DeserializeObject<HasAnyRoleRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户的登录历史记录
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        /// <param name="appId">应用 ID</param>
        /// <param name="clientIp">客户端 IP</param>
        /// <param name="start">开始时间戳（毫秒）</param>
        /// <param name="end">结束时间戳（毫秒）</param>
        /// <param name="page">当前页数，从 1 开始</param>
        /// <param name="limit">每页数目，最大不能超过 50，默认为 10</param>
        ///<returns>UserLoginHistoryPaginatedRespDto</returns>
        public async Task<UserLoginHistoryPaginatedRespDto> GetUserLoginHistory(string userId, string userIdType = "user_id", string appId = null, string clientIp = null, long start = 0, long end = 0, long page = 1, long limit = 10)
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-login-history", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
        {"appId",appId },
        {"clientIp",clientIp },
        {"start",start },
        {"end",end },
        {"page",page },
        {"limit",limit },
    }).ConfigureAwait(false);
            UserLoginHistoryPaginatedRespDto result = client.JsonService.DeserializeObject<UserLoginHistoryPaginatedRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 通过用户 ID，获取用户曾经登录过的应用，可以选择指定用户 ID 类型等。
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>UserLoggedInAppsListRespDto</returns>
        public async Task<UserLoggedInAppsListRespDto> GetUserLoggedinApps(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-loggedin-apps", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
    }).ConfigureAwait(false);
            UserLoggedInAppsListRespDto result = client.JsonService.DeserializeObject<UserLoggedInAppsListRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 通过用户 ID，获取用户曾经登录过的身份源，可以选择指定用户 ID 类型等。
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        ///<returns>UserLoggedInIdentitiesRespDto</returns>
        public async Task<UserLoggedInIdentitiesRespDto> GetUserLoggedinIdentities(string userId, string userIdType = "user_id")
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-logged-in-identities", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
    }).ConfigureAwait(false);
            UserLoggedInIdentitiesRespDto result = client.JsonService.DeserializeObject<UserLoggedInIdentitiesRespDto>(httpResponse);
            return result;
        }
        ///<summary>
        /// 获取用户被授权的所有资源
        ///</summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="userIdType">用户 ID 类型，可以指定为用户 ID、手机号、邮箱、用户名和 externalId。</param>
        /// <param name="nameSpace">所属权限分组的 code</param>
        /// <param name="resourceType">资源类型，如 数据、API、菜单、按钮</param>
        ///<returns>AuthorizedResourcePaginatedRespDto</returns>
        public async Task<AuthorizedResourcePaginatedRespDto> GetUserAuthorizedResources(string userId, string userIdType = "user_id", string nameSpace = null, string resourceType = null)
        {
            string httpResponse = await client.Request("GET", "/api/v3/get-user-authorized-resources", new Dictionary<string, object> {
        {"userId",userId },
        {"userIdType",userIdType },
        {"namespace",nameSpace },
        {"resourceType",resourceType },
    }).ConfigureAwait(false);
            AuthorizedResourcePaginatedRespDto result = client.JsonService.DeserializeObject<AuthorizedResourcePaginatedRespDto>(httpResponse);
            return result;
        }
    }
}
