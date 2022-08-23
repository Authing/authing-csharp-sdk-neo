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
using Authing.ApiClient.Domain.Model.Management.Resources;
using Authing.ApiClient.Domain.Model.Management.Applications;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.ApiClient.Domain.Utils;
using System.Linq;
using System.Net.Http;
using Authing.ApiClient.Extensions;
using Authing.Library.Domain.Model.Management.Applications;
using Authing.Library.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Authing.Library.Domain.Model.Exceptions;
using Authing.Library.Domain.Client.Impl;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class ApplicationsManagementClient : IApplicationsManagementClient
    {
        private ManagementClient client;

        public ApplicationsManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 获取用户池应用列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页个数</param>
        /// <returns></returns>
        public async Task<ApplicationList> List(int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<ApplicationList>($"api/v2/applications?page={page}&limit={limit}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name">应用名称</param>
        /// <param name="identifier">应用认证地址</param>
        /// <param name="redirectUris">应用回调链接</param>
        /// <param name="logo">应用 logo</param>
        /// <returns></returns>
        public async Task<Application> Create(string name, string identifier, string[] redirectUris, string logo = null,AuthingErrorBox authingErrorBox=null)
        {
            logo ??= "https://files.authing.co/authing-console/default-app-logo.png";
            var res = await client.RequestCustomDataWithToken<Application>($"api/v2/applications", new Dictionary<string, object>() {
                { nameof(name), name },
                { nameof(identifier), identifier },
                { nameof(redirectUris), redirectUris },
                { nameof(logo), logo }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        public async Task<bool> Delete(string appId,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Code == 200;
        }

        /// <summary>
        /// 通过 ID 获取应用详情
        /// </summary>
        /// <param name="id">应用 ID</param>
        /// <returns></returns>
        public async Task<Application> FindById(string id,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<Application>($"api/v2/applications/{id}",method:HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 通过 ID 获取应用详情,公共
        /// </summary>
        /// <param name="id">应用 ID</param>
        /// <returns></returns>
        public async Task<ApplicationV2> FindByIdV2(string id,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithOutToken<ApplicationV2>($"api/v2/applications/{id}/public-config", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 获取应用的资源列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="listResourceOption">选项</param>
        /// <returns></returns>
        public async Task<PaginatedResources> ListResource(
            string appId,
            ListResourceOption listResourceOption = null,
            AuthingErrorBox authingErrorBox=null)
        {
            var query = $"?namespace={appId}";
            if (listResourceOption != null && listResourceOption.Page != null)
            {
                query += $"&page={listResourceOption.Page}";
            }
            if (listResourceOption != null && listResourceOption.Limit != null)
            {
                query += $"&limit={listResourceOption.Limit}";
            }
            if (listResourceOption != null && listResourceOption.Type != null)
            {
                query += $"&type={listResourceOption.Type.ToString()}";
            }
            var res = await client.RequestCustomDataWithToken<PaginatedResources>($"api/v2/resources{query}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 为应用创建资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="createResourceParam"></param>
        /// <returns></returns>
        public async Task<Resources> CreateResource(string appId, CreateResourceParam createResourceParam,AuthingErrorBox authingErrorBox=null)
        {
            if (createResourceParam == null) throw new ArgumentNullException(nameof(createResourceParam));

            if (createResourceParam.Code == null)
            {
                throw new ArgumentException("请为资源设定一个资源标识符");
            }

            if (createResourceParam.Actions == null || createResourceParam.Actions?.Count() < 1)
            {
                throw new ArgumentException("请至少定义一个资源操作");
            }

            createResourceParam.NameSpace = appId;
            var res = await client.RequestCustomDataWithToken<Resources>("api/v2/resources",
                new Dictionary<string, object>()
                {
                        {nameof(createResourceParam.Code).ToLower(),createResourceParam.Code},
                        {nameof(createResourceParam.Actions).ToLower(),createResourceParam.Actions},
                        {nameof(createResourceParam.NameSpace).ToLower(),createResourceParam.NameSpace},
                        {nameof(createResourceParam.Type).ToLower(),createResourceParam.Type.ToString()},
                        {nameof(createResourceParam.Description).ToLower(),createResourceParam.Description},
                }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 更新应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <param name="updateResourceParam"></param>
        /// <returns></returns>
        public async Task<Resources> UpdateResource(string appId,
                                                    string code,
                                                    UpdateResourceParam updateResourceParam,
                                                    AuthingErrorBox authingErrorBox = null)
        {
            updateResourceParam.NameSpace = appId;
            var res = await client.RequestCustomDataWithToken<Resources>($"api/v2/resources/{code}", new Dictionary<string, object>() {
                { nameof(updateResourceParam.Actions), updateResourceParam.Actions},
                { nameof(updateResourceParam.NameSpace).ToLower(), updateResourceParam.NameSpace},
                { nameof(updateResourceParam.Type), updateResourceParam.Type.ToString() },
                { nameof(updateResourceParam.Description), updateResourceParam.Description },
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 删除应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> DeleteResource(string appId, string code,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/resources/{code}?namespace={appId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Code==200;
        }

        /// <summary>
        /// 获取应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicyQueryFilter">选项</param>
        /// <returns></returns>
        public async Task<ApplicationAccessPolicies> GetAccessPolicies(string appId, AppAccessPolicyQueryFilter appAccessPolicyQueryFilter,AuthingErrorBox authingErrorBox=null)
        {
            var query = $"?page={appAccessPolicyQueryFilter.Page}&limit={appAccessPolicyQueryFilter.Limit}";
            var res = await client.RequestCustomDataWithToken<ApplicationAccessPolicies>($"api/v2/applications/{appId}/authorization/records{query}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 启用针对某个用户、角色、分组、组织机构的应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        public async Task<CommonMessage> EnableAccessPolicy(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null)
        {
            if (appAccessPolicy.TargetIdentifiers == null)
            {
                return new CommonMessage()
                {
                    Code = 500,
                    Message = "请传入主体 id"
                };
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/authorization/enable-effect", new Dictionary<string, object>() {
                { "nameSpace", appId },
                { "targetType", appAccessPolicy.TargetType },
                { "targetIdentifiers", appAccessPolicy.TargetIdentifiers },
                { "inheritByChildren", appAccessPolicy.InheritByChildren }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message
            };
        }

        /// <summary>
        /// 停用针对某个用户、角色、分组、组织机构的应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        public async Task<CommonMessage> DisableAccessPolicy(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null)
        {
            if (appAccessPolicy.TargetIdentifiers == null)
            {
                return new CommonMessage()
                {
                    Code = 500,
                    Message = "请传入主体 id"
                };
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/authorization/disable-effect", new Dictionary<string, object>() {
                { "nameSpace", appId },
                { "targetType", appAccessPolicy.TargetType },
                { "targetIdentifiers", appAccessPolicy.TargetIdentifiers },
                { "inheritByChildren", appAccessPolicy.InheritByChildren }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message
            };
        }

        /// <summary>
        /// 删除针对某个用户、角色、分组、组织机构的应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteAccessPolicy(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null)
        {
            if (appAccessPolicy.TargetIdentifiers == null)
            {
                return new CommonMessage()
                {
                    Code = 500,
                    Message = "请传入主体 id"
                };
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/authorization/revoke", new Dictionary<string, object>() {
                { "nameSpace", appId },
                { "targetType", appAccessPolicy.TargetType },
                { "targetIdentifiers", appAccessPolicy.TargetIdentifiers },
                { "inheritByChildren", appAccessPolicy.InheritByChildren }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message
            };
        }

        /// <summary>
        /// 配置「允许主体（用户、角色、分组、组织机构节点）访问应用」的控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        public async Task<CommonMessage> AllowAccess(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null)
        {
            if (appAccessPolicy.TargetIdentifiers == null)
            {
                return new CommonMessage()
                {
                    Code = 500,
                    Message = "请传入主体 id"
                };
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/authorization/allow", new Dictionary<string, object>() {
                { "nameSpace", appId },
                { "targetType", appAccessPolicy.TargetType },
                { "targetIdentifiers", appAccessPolicy.TargetIdentifiers },
                { "inheritByChildren", appAccessPolicy.InheritByChildren }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message
            };
        }

        /// <summary>
        /// 配置「拒绝主体（用户、角色、分组、组织机构节点）访问应用」的控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        public async Task<CommonMessage> DenyAccess(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null)
        {
            if (appAccessPolicy.TargetIdentifiers == null)
            {
                return new CommonMessage()
                {
                    Code = 500,
                    Message = "请传入主体 id"
                };
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/authorization/deny", new Dictionary<string, object>() {
                { "nameSpace", appId },
                { "targetType", appAccessPolicy.TargetType },
                { "targetIdentifiers", appAccessPolicy.TargetIdentifiers },
                { "inheritByChildren", appAccessPolicy.InheritByChildren }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return new CommonMessage()
            {
                Code = res.Code,
                Message = res.Message
            };
        }

        /// <summary>
        /// 更改默认应用访问策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="updateDefaultApplicationAccessPolicyParam">选项</param>
        /// <returns></returns>
        public async Task<PublicApplication> UpdateDefaultAccessPolicy(string appId,
                                                                       UpdateDefaultApplicationAccessPolicyParam updateDefaultApplicationAccessPolicyParam,
                                                                       AuthingErrorBox authingErrorBox = null)
        {
            var permissionStrategy = new
            {
                defaultStrategy = updateDefaultApplicationAccessPolicyParam.DefaultStrategy.ToString()
            };
            var res = await client.RequestCustomDataWithToken<PublicApplication>($"api/v2/applications/{appId}", new Dictionary<string, object>() {
                { "permissionStrategy", permissionStrategy }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public async Task<Role> CreateRole(string appId,
                                           string code,
                                           string description = null,
                                           AuthingErrorBox authingErrorBox = null)
        {
            var param = new CreateRoleParam(code)
            {
                Description = description,
                Namespace = appId
            };
            var res = await client.RequestCustomDataWithToken<CreateRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteRole(string appId,
                                                    string code,
                                                    AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteRoleParam(code)
            {
                Namespace = appId
            };
            var res = await client.RequestCustomDataWithToken<DeleteRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="codeList">角色唯一标志符列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteRoles(string appId,
                                                     IEnumerable<string> codeList,
                                                     AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteRolesParam(codeList)
            {
                Namespace = appId
            };
            var res = await client.RequestCustomDataWithToken<DeleteRolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="updateRoleOptions">选项</param>
        /// <returns></returns>
        public async Task<Role> UpdateRole(string appId,
                                           UpdateRoleOptions updateRoleOptions,
                                           AuthingErrorBox authingErrorBox=null)
        {
            var param = new UpdateRoleParam(updateRoleOptions.Code)
            {
                Namespace = appId,
                Description = updateRoleOptions.Description,
                NewCode = updateRoleOptions.NewCode,
            };
            var res = await client.RequestCustomDataWithToken<UpdateRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        [Obsolete("已过时, 不建议使用")]
        public async Task<Role> FindRole(string appId,
                                         string code,
                                         AuthingErrorBox authingErrorBox = null)
        {
            var param = new RoleParam(code)
            {
                Namespace = appId
            };
            var res = await client.RequestCustomDataWithToken<RoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="codeList">角色唯一标志符列表</param>
        /// <returns></returns>
        public async Task<PaginatedRoles> GetRoles(string appId,
                                                   int page = 1,
                                                   int limit = 10,
                                                   AuthingErrorBox authingErrorBox = null)
        {
            var param = new RolesParam()
            {
                Page = page,
                Limit = limit,
                Namespace = appId
            };
            var res = await client.RequestCustomDataWithToken<RolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <returns></returns>
        public async Task<PaginatedUsers> GetUsersByRoleCode(string appId,
                                                             string code,
                                                             AuthingErrorBox authingErrorBox = null)
        {
            var _param = new RoleWithUsersParam(code)
            {
                Code = code,
                Namespace = appId
            };
            var _res = await client.RequestCustomDataWithToken<RoleWithUsersResponse>(_param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(_res, authingErrorBox);
            return _res.Data?.Result.Users;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> AddUsersToRole(string appId,
                                                        string code,
                                                        IEnumerable<string> userIds,
                                                        AuthingErrorBox authingErrorBox = null)
        {
            var param = new AssignRoleParam()
            {
                UserIds = userIds,
                RoleCode = code,
                Namespace = appId
            };
            var res = await client.RequestCustomDataWithToken<AssignRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveUsersFromRole(string appId,
                                                             string code,
                                                             IEnumerable<string> userIds,
                                                             AuthingErrorBox authingErrorBox = null)
        {
            var param = new RevokeRoleParam()
            {
                UserIds = userIds,
                RoleCode = code,
                Namespace = appId,
            };
            var res = await client.RequestCustomDataWithToken<RevokeRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取角色被授权的所有资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="resourceType">资源类型</param>
        /// <returns></returns>
        public async Task<Role> ListAuthorizedResourcesByRole(string appId,
                                                              string code,
                                                              ResourceType resourceType = default,
                                                              AuthingErrorBox authingErrorBox = null)
        {
            var param = new ListRoleAuthorizedResourcesParam(code)
            {
                ResourceType = resourceType.ToString().ToUpper(),
                Namespace = appId,
            };
            var res = await client.RequestCustomDataWithToken<ListRoleAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            if (res.Data.Result == null)
            {
                throw new Exception("角色不存在");
            }
            return res.Data?.Result;
        }

        /// <summary>
        /// 创建注册协议
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="agreement">角色唯一标志符</param>
        /// <returns></returns>
        public async Task<Agreement> createAgreement(string appId, AgreementInput agreement,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<Agreement>($"api/v2/applications/{appId}/agreements", new Dictionary<string, object>() {
                { "title", agreement.Title},
                { "required", agreement.Required},
                { "lang", agreement.Lang.GetEnumMemberValue()},
                { "availableAt",(int)agreement.AvailableAt}
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 删除注册协议
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="agreementId">协议 ID</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> deleteAgreement(string appId, int agreementId,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/agreements/{agreementId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 修改注册协议
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="agreementId">协议 ID</param>
        /// <param name="agreement">角色唯一标志符</param>
        /// <returns></returns>
        public async Task<Agreement> modifyAgreement(string appId, int agreementId, AgreementInput agreement,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<Agreement>($"api/v2/applications/{appId}/agreements/{agreementId}", new Dictionary<string, object>() {
                { "title", agreement.Title},
                { "required", agreement.Required},
                { "lang", agreement.Lang.GetEnumMemberValue()},
                { "availableAt",(int)agreement.AvailableAt}
            }.ConvertJson(),method: HttpMethod.Put, contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 获取应用注册协议列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        public async Task<PaginationAgreement> listAgreement(string appId,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<PaginationAgreement>($"api/v2/applications/{appId}/agreements", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 对应用的注册协议排序
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="order">应用下所有协议的 ID 列表，按需要的顺序排列</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> sortAgreement(string appId, IEnumerable<int> order,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/applications/{appId}/agreements/sort", new Dictionary<string, object>() {
                { "ids", order }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 查看应用下已登录用户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="page">页码</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        public async Task<ActiveUsers> ActiveUsers(string appId,
                                                   int page = 1,
                                                   int limit = 10,
                                                   AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<ActiveUsers>($"api/v2/applications/{appId}/active-users", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 刷新应用密钥
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        public async Task<Application> RefreshApplicationSecret(string appId,AuthingErrorBox authingErrorBox=null )
        {
            var res = await client.RequestCustomDataWithToken<Application>($"api/v2/application/{appId}/refresh-secret", method: new HttpMethod("PATCH")).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 更改应用类型
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="type">应用类型</param>
        /// <returns></returns>
        public async Task<Application> ChangeApplicationType(string appId, ApplicationType type,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<Application>($"api/v2/applications/{appId}", new Dictionary<string, string>() {
                { "appType", type.ToString() }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 获取应用关联租户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="type">应用类型</param>
        /// <returns></returns>
        public async Task<ApplicationTenantDetails> ApplicationTenants(string appId,AuthingErrorBox authingErrorBox=null)
        {
            var res = await client.RequestCustomDataWithToken<ApplicationTenantDetails>($"api/v2/application/{appId}/tenants", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }
    }
}