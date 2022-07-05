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
using Authing.ApiClient.Types;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.Library.Domain.Model.Management.Applications;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IApplicationsManagementClient
    {
        /// <summary>
        /// 获取用户池应用列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页个数</param>
        /// <returns></returns>
        Task<ApplicationList> List(int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name">应用名称</param>
        /// <param name="identifier">应用认证地址</param>
        /// <param name="redirectUris">应用回调链接</param>
        /// <param name="logo">应用 logo</param>
        /// <returns></returns>
        Task<Application> Create(string name, string identifier, string[] redirectUris, string logo = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        Task<bool> Delete(string appId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 通过 ID 获取应用详情
        /// </summary>
        /// <param name="id">应用 ID</param>
        /// <returns></returns>
        Task<Application> FindById(string id,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 通过 ID 获取应用详情,公共
        /// </summary>
        /// <param name="id">应用 ID</param>
        /// <returns></returns>
        Task<ApplicationV2> FindByIdV2(string id,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取应用的资源列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="listResourceOption">选项</param>
        /// <returns></returns>
        Task<PaginatedResources> ListResource(string appId, ListResourceOption listResourceOption = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 为应用创建资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="createResourceParam"></param>
        /// <returns></returns>
        Task<Resources> CreateResource(string appId, CreateResourceParam createResourceParam,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 更新应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <param name="updateResourceParam"></param>
        /// <returns></returns>
        Task<Resources> UpdateResource(string appId, string code, UpdateResourceParam updateResourceParam,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> DeleteResource(string appId, string code,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicyQueryFilter">选项</param>
        /// <returns></returns>
        Task<ApplicationAccessPolicies> GetAccessPolicies(string appId, AppAccessPolicyQueryFilter appAccessPolicyQueryFilter,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 启用针对某个用户、角色、分组、组织机构的应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        Task<CommonMessage> EnableAccessPolicy(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 停用针对某个用户、角色、分组、组织机构的应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        Task<CommonMessage> DisableAccessPolicy(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除针对某个用户、角色、分组、组织机构的应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteAccessPolicy(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 配置「允许主体（用户、角色、分组、组织机构节点）访问应用」的控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        Task<CommonMessage> AllowAccess(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 配置「拒绝主体（用户、角色、分组、组织机构节点）访问应用」的控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicy">选项</param>
        /// <returns></returns>
        Task<CommonMessage> DenyAccess(string appId, AppAccessPolicy appAccessPolicy,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 更改默认应用访问策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="updateDefaultApplicationAccessPolicyParam">选项</param>
        /// <returns></returns>
        Task<PublicApplication> UpdateDefaultAccessPolicy(string appId, UpdateDefaultApplicationAccessPolicyParam updateDefaultApplicationAccessPolicyParam,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        Task<Role> CreateRole(string appId, string code, string description = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteRole(string appId, string code,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="codeList">角色唯一标志符列表</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteRoles(string appId, IEnumerable<string> codeList,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="updateRoleOptions">选项</param>
        /// <returns></returns>
        Task<Role> UpdateRole(string appId, UpdateRoleOptions updateRoleOptions,AuthingErrorBox authingErrorBox=null);

        [Obsolete("已过时, 不建议使用")]
        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <returns></returns>
        Task<Role> FindRole(string appId, string code,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="codeList">角色唯一标志符列表</param>
        /// <returns></returns>
        Task<PaginatedRoles> GetRoles(string appId, int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <returns></returns>
        Task<PaginatedUsers> GetUsersByRoleCode(string appId, string code,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <returns></returns>
        Task<CommonMessage> AddUsersToRole(string appId, string code, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<CommonMessage> RemoveUsersFromRole(string appId, string code, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取角色被授权的所有资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code">角色唯一标志符</param>
        /// <param name="resourceType">资源类型</param>
        /// <returns></returns>
        Task<Role> ListAuthorizedResourcesByRole(string appId, string code, ResourceType resourceType = default,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 创建注册协议
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="agreement">角色唯一标志符</param>
        /// <returns></returns>
        Task<Agreement> createAgreement(string appId, AgreementInput agreement,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除注册协议
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="agreementId">协议 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> deleteAgreement(string appId, int agreementId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 修改注册协议
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="agreementId">协议 ID</param>
        /// <param name="agreement">角色唯一标志符</param>
        /// <returns></returns>
        Task<Agreement> modifyAgreement(string appId, int agreementId, AgreementInput agreement,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取应用注册协议列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        Task<PaginationAgreement> listAgreement(string appId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 对应用的注册协议排序
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="order">应用下所有协议的 ID 列表，按需要的顺序排列</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> sortAgreement(string appId, IEnumerable<int> order,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 查看应用下已登录用户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="page">页码</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        Task<ActiveUsers> ActiveUsers(string appId, int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 刷新应用密钥
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        Task<Application> RefreshApplicationSecret(string appId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 更改应用类型
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="type">应用类型</param>
        /// <returns></returns>
        Task<Application> ChangeApplicationType(string appId, ApplicationType type,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取应用关联租户
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="type">应用类型</param>
        /// <returns></returns>
        Task<ApplicationTenantDetails> ApplicationTenants(string appId,AuthingErrorBox authingErrorBox=null);
    }
}