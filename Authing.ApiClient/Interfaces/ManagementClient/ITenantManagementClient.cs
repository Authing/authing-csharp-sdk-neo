using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Tenant;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Domain.Model.Management.Department;
using Authing.ApiClient.Domain.Model.Management.AuthorizedResources;
using Authing.ApiClient.Domain.Model.Management.UserAction;
using Authing.ApiClient.Types;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface ITenantManagementClient
    {
        /// <summary>
        /// 获取用户池下租户列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        Task<Pagination<TenantInfo>> List(int page = 1, int limit = 10, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        Task<TenantDetails> Details(string tenantId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<TenantDetails> Create(CreateTenantOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> Update(string tenantId, CreateTenantOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> Delete(string tenantId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 配置租户品牌化
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> Config(string tenantId, ConfigTenantOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 获取租户成员列表
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<Pagination<TenantMembers>> Members(string tenantId, TenantMembersOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 添加租户成员
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<TenantAddMembersResponse> AddMembers(string tenantId, string[] userIds, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 移除租户成员
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> RemoveMembers(string tenantId, string userId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 获取身份源列表
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        Task<IEnumerable<ExtIdpListOutput>> ListExtIdp(string tenantId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 获取身份源详细信息
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <returns></returns>
        Task<ExtIdpDetailOutput> ExtIdpDetail(string extIdpId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 创建身份源
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<ExtIdpDetailOutput> CreateExtIdp(CreateExtIdpOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 更新身份源配置
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> UpdateExtIdp(string extIdpId, UpdateExtIdpOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 删除身份源
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> DeleteExtIdp(string extIdpId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 创建身份源连接
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<ExtIdpConnDetailOutput> CreateExtIdpConnection(CreateExtIdpConnectionOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 更新身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> UpdateExtIdpConnection(string extIdpConnectionId, UpdateExtIdpConnectionOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 删除身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> DeleteExtIdpConnection(string extIdpConnectionId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 检查连接唯一标识是否冲突
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> CheckExtIdpConnectionIdentifierUnique(string identifier, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 开关身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> ChangeExtIdpConnectionState(string extIdpConnectionId, ChangeExtIdpConnectionStateOption option, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 批量开关身份源连接
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> BatchChangeExtIdpConnectionState(string extIdpId, ChangeExtIdpConnectionStateOption option, AuthingErrorBox authingErrorBox = null);
    }
}
