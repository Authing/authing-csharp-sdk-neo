﻿using System;
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
using Authing.ApiClient.Domain.Model.GraphQLParam;
using Authing.ApiClient.Extensions;

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
            userInfo.Password = EncryptHelper.RsaEncryptWithPublic(userInfo.Password, client.PublicKey);
            var param = new CreateUserParam(userInfo)
            {
                KeepPassword = createUserOption?.KeepPassword,
                ResetPasswordOnFirstLogin = createUserOption?.ResetPasswordOnFirstLogin,
                UserInfo = userInfo,
            };

            var res = await client.Post<CreateUserResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<UpdateUserResponse>(param.CreateRequest()).ConfigureAwait(false);
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
                var _res = await client.Post<UserWithCustomDataResponse>(_param.CreateRequest()).ConfigureAwait(false);
                return _res.Data.Result;
            }
            var param = new UserParam() { Id = userId };
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.Post<UserResponse>(param.CreateRequest()).ConfigureAwait(false);
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
            var res = await client.Post<DeleteUserResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<DeleteUsersResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        /// <summary>
        /// 通过 ID 列表批量获取用户信息
        /// </summary>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Batch(
            IEnumerable<string> userIds,
            BatchFetchUserTypes batchFetchUserType = BatchFetchUserTypes.id)
        {
            var param = new UserBatchParam(userIds)
            {
                Type = batchFetchUserType.ToString()
            };
            var res = await client.Post<UserBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<UsersResponse>(param.CreateRequest()).ConfigureAwait(false);
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
            var res = await client.Post<ArchivedUsersResponse>(param.CreateRequest()).ConfigureAwait(false);
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
            var res = await client.Post<IsUserExistsResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<FindUserResponse>(param.CreateRequest()).ConfigureAwait(false);
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
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.Post<SearchUserResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<RefreshTokenResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<GetUserGroupsResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<AddUserToGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<RemoveUserFromGroupResponse>(param.CreateRequest()).ConfigureAwait(false);
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
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.Post<GetUserRolesResponse>(param.CreateRequest()).ConfigureAwait(false);
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

            var res = await client.Post<AssignRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
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
            await client.GetAccessToken().ConfigureAwait(false);
            var res = await client.Post<RevokeRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户所在组织机构
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<PaginatedOrgsAndNodes> ListOrgs(string userId)
        {
            var res = await client.RequestCustomDataWithToken<IEnumerable<IEnumerable<OrgAndNode>>>($"api/v2/users/{userId}/orgs", method: System.Net.Http.HttpMethod.Get,contenttype: ContentType.JSON).ConfigureAwait(false);
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
        public async Task<PaginatedDepartments> ListDepartment(string userId)
        {
            var param = new GetUserDepartmentsParam(userId);
            var res = await client.Post<GetUserDepartmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
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
        public async Task<PaginatedAuthorizedResources> ListAuthorizedResources(
            string userId,
            string _namespace,
            ListAuthorizedResourcesOption option = null)
        {
            var param = new ListUserAuthorizedResourcesParam(userId)
            {
                Namespace = _namespace,
            };
            if (option != null && option.ResourceType != null)
            {
                param.ResourceType = option.ResourceType.ToString().ToUpper();
            }
            var res = await client.Post<ListUserAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
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
        public async Task<List<KeyValuePair<string, object>>> GetUdfValue(string userId)
        {
            var param = new UdvParam(UdfTargetType.USER, userId);
            var res = await client.Post<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            return AuthingUtils.ConverUdvToKeyValuePair(res.Data.Result);
        }

        /// <summary>
        /// 批量获取多个用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(string[] userIds)
        {
            if (userIds.Length < 1)
            {
                throw new Exception("empty user id list");
            }
            var param = new UdfValueBatchParam(UdfTargetType.USER, userIds);
            var res = await client.Post<UdfValueBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
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
        public async Task<IEnumerable<UserDefinedData>> SetUdfValue(string userId, KeyValueDictionary data)
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
            var res = await client.Post<SetUdvBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        /// <summary>
        /// 批量设置自定义数据
        /// </summary>
        /// <param name="setUdfValueBatchInput"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserDefinedData>> SetUdfValueBatch(SetUserUdfValueBatchParam[] setUdfValueBatchInput)
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
            var res = await client.Post<SetUdvBatchResponse>(_param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        /// <summary>
        /// 清除用户的自定义数据
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveUdfValue(string userId, string key)
        {
            var param = new RemoveUdvParam(UdfTargetType.USER, userId, key);
            var res = await client.Post<SetUdfValueBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        /// <summary>
        /// 判断用户是否有某个角色
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="roleCode">角色 Code</param>
        /// <param name="_namespace">权限分组 ID</param>
        /// <returns></returns>
        public async Task<bool> hasRole(string userId, string roleCode, string _namespace = null)
        {
            var roleList = await ListRoles(userId, _namespace).ConfigureAwait(false);

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
        public async Task<CommonMessage> Kick(IEnumerable<string> userIds)
        {
            var result = await client.RequestCustomDataWithToken<CommonMessage>("api/v2/users/kick", 
                new Dictionary<string, object>
                {
                    { nameof(userIds), userIds }
                }.ConvertJson(),contenttype: ContentType.JSON).ConfigureAwait(false);
            
            return new CommonMessage(){Code =result.Code,Message = result.Message};
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
        public async Task<CommonMessage> Logout(LogoutParam logoutParam)
        {
            if (logoutParam.UserId == null)
            {
                throw new Exception("请传入 options.userId，内容为要下线的用户 ID");
            }

            await client.Get<CommonMessage>("logout", new ExpnadAllRequest().CreateRequest()).ConfigureAwait(false);
            return new CommonMessage
            {
                Code = 200,
                Message = "强制登出成功"
            };
        }

        /// <summary>
        /// 查询用户的登录状态
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="appId">应用 ID</param>
        /// <param name="devicdId">选项</param>
        /// <returns></returns>
        public async Task<CheckLoginStatusRes> CheckLoginStatus(string userId, string appId = null, string devicdId = null)
        {
            var query = $"?userId={userId}";
            if (appId != null) {
                query += $"&appId={appId}";
            }
            if (devicdId != null)
            {
                query += $"&devicdId={devicdId}";
            }
            var res = await client.Get<CheckLoginStatusRes>($"api/v2/users/login-status{query}", new GraphQLRequest()).ConfigureAwait(false);
            return res.Data;
        }

        /// <summary>
        /// 审计日志列表
        /// </summary>
        /// <param name="listUserActionsParam">选项</param>
        /// <returns></returns>
        public async Task<ListUserActionsRealRes> ListUserActions(ListUserActionsParam listUserActionsParam = null)
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
            var res = await client.Get<ListUserActionsResObject>("api/v2/analysis/user-action", new GraphQLRequest()).ConfigureAwait(false);
            if (res.Data.Data.TotalCount == 0)
            {
                var resEmpty = new ListUserActionsRealRes()
                {
                    TotalCount = 0,
                    List = new UserActionRes[0]
                };
                return resEmpty;
            }
            var list = res.Data.Data.List;
            var resList = list.Select(log => new UserActionRes
            {
                UserPoolId = log.UserPoolId,
                Id = log.UserId,
                UserName = log.UserName,
                CityName = log.Geoip.CityName,
                RegionName = log.Geoip.RegionName,
                ClientIp = log.Geoip.Ip,
                OperationDesc = log.OperationDesc,
                OperationName = log.OperationName,
                TimeStamp = log.Timestamp,
                AppId = log.AppId,
                AppName = log.AppName
            }).ToArray();
            var resReal = new ListUserActionsRealRes()
            {
                TotalCount = res.Data.Data.TotalCount,
                List = resList
            };
            return resReal;
        }

        /// <summary>
        /// 发送首次登录验证邮件
        /// </summary>
        /// <param name="sendFirstLoginVerifyEmailParam">选项</param>
        /// <returns></returns>
        public async Task<SendFirstLoginVerifyEmailResponse> SendFirstLoginVerifyEmail(SendFirstLoginVerifyEmailParam sendFirstLoginVerifyEmailParam)
        {
            var param = sendFirstLoginVerifyEmailParam;
            var res = await client.Post<SendFirstLoginVerifyEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data;
        }

        /// <summary>
        /// 批量导入用户
        /// </summary>
        /// <param name="userInfos">用户信息列表</param>
        /// <returns></returns>
        public async Task<CreateUsersRes> CreateUsers(IEnumerable<CreateUserInput> userInfos)
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
            }
            var res = await client.PostRaw<CreateUsersRes>("api/v2/users/create/batch", new Dictionary<string,object>() {
                { "users", userInfos }
            }).ConfigureAwait(false);
            return res.Data;
        }

        /// <summary>
        /// 获取用户所在租户
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<User> GetUserTenants(string userId)
        {
            var res = await client.Get<User>($"api/v2/users/{userId}/tenants", new GraphQLRequest()).ConfigureAwait(false);
            return res.Data;
        }

        /// <summary>
        /// 给用户绑定一个身份
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> LinkIdentity(LinkIdentityOption option)
        {
            var body = new Dictionary<string, object>() {
                { "userId", option.UserId },
                { "userIdInIdp", option.UserIdInIdp },
                { "isSocial", option.IsSocial },
                { "identifier", option.Identifier },
            };
            if (option.Type != null) {
                body.Add("type", option.Type);
            }
            var res = await client.PostRaw<CommonMessage>("api/v2/users/identity/link", body).ConfigureAwait(false);
            return res;
        }

        /// <summary>
        /// 解除用户某个身份源下的所有身份
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> UnlinkIdentity(UnlinkIdentityOption option)
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
            var res = await client.PostRaw<CommonMessage>("api/v2/users/identity/unlink", body).ConfigureAwait(false);
            return res;
        }
    }
}
