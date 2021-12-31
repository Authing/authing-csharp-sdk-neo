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
        Task<Pagination<TenantInfo>> List(int page = 1, int limit = 10);

        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        Task<TenantDetails> Details(string tenantId);

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<TenantDetails> Create(CreateTenantOption option);

        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> Update(string tenantId, CreateTenantOption option);

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> Delete(string tenantId);

        /// <summary>
        /// 配置租户品牌化
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> Config(string tenantId, ConfigTenantOption option);

        /// <summary>
        /// 获取租户成员列表
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<Pagination<TenantMembers>> Members(string tenantId, TenantMembersOption option);

        /// <summary>
        /// 添加租户成员
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<TenantAddMembersResponse> AddMembers(string tenantId, string[] userIds);

        /// <summary>
        /// 移除租户成员
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> RemoveMembers(string tenantId, string userId);

        /// <summary>
        /// 获取身份源列表
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        Task<IEnumerable<ExtIdpListOutput>> ListExtIdp(string tenantId);

        /// <summary>
        /// 获取身份源详细信息
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <returns></returns>
        Task<ExtIdpDetailOutput> ExtIdpDetail(string extIdpId);

        /// <summary>
        /// 创建身份源
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<ExtIdpDetailOutput> CreateExtIdp(CreateExtIdpOption option);

        /// <summary>
        /// 更新身份源配置
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> UpdateExtIdp(string extIdpId, UpdateExtIdpOption option);

        /// <summary>
        /// 删除身份源
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> DeleteExtIdp(string extIdpId);

        /// <summary>
        /// 创建身份源连接
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<ExtIdpConnDetailOutput> CreateExtIdpConnection(CreateExtIdpConnectionOption option);

        /// <summary>
        /// 更新身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> UpdateExtIdpConnection(string extIdpConnectionId, UpdateExtIdpConnectionOption option);

        /// <summary>
        /// 删除身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <returns></returns>
        Task<GraphQLResponse<CommonMessage>> DeleteExtIdpConnection(string extIdpConnectionId);

        /// <summary>
        /// 检查连接唯一标识是否冲突
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> CheckExtIdpConnectionIdentifierUnique(string identifier);

        /// <summary>
        /// 开关身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> ChangeExtIdpConnectionState(string extIdpConnectionId, ChangeExtIdpConnectionStateOption option);

        /// <summary>
        /// 批量开关身份源连接
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        Task<bool> BatchChangeExtIdpConnectionState(string extIdpId, ChangeExtIdpConnectionStateOption option);
    }
}
