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
using Authing.ApiClient.Domain.Model.Management.Tenant;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.ApiClient.Domain.Utils;
using System.Linq;
using System.Net.Http;
using Authing.ApiClient.Extensions;
using Authing.Library.Domain.Model.Exceptions;
using Authing.Library.Domain.Client.Impl;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class TenantManagementClient : ITenantManagementClient
    {
        private ManagementClient client;
        public TenantManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 获取用户池下租户列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        public async Task<Pagination<TenantInfo>> List(int page = 1,
                                                       int limit = 10,
                                                       AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<Pagination<TenantInfo>>($"api/v2/tenants?page={page}&limit={limit}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        public async Task<TenantDetails> Details(string tenantId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<TenantDetails>($"api/v2/tenant/{tenantId}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<TenantDetails> Create(CreateTenantOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, string>() {
                { "name", option.Name },
                { "appIds", option.AppIds }
            };
            if (option.Logo != null)
            {
                body.Add("logo", option.Logo);
            }
            if (option.Description != null)
            {
                body.Add("description", option.Description);
            }
            var res = await client.RequestCustomDataWithToken<TenantDetails>("api/v2/tenant", body.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<bool> Update(string tenantId, CreateTenantOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, string>();
            if (option.Name != null)
            {
                body.Add("name", option.Name);
            }
            if (option.AppIds != null)
            {
                body.Add("appIds", option.AppIds);
            }
            if (option.Logo != null)
            {
                body.Add("logo", option.Logo);
            }
            if (option.Description != null)
            {
                body.Add("description", option.Description);
            }
            var res = await client.RequestCustomDataWithToken<bool>($"api/v2/tenant/{tenantId}", body.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> Delete(string tenantId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/tenant/{tenantId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 配置租户品牌化
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<bool> Config(string tenantId, ConfigTenantOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, object>() { };
            if (option.Css != null)
            {
                body.Add("css", option.Css);
            }
            if (option.SsoPageCustomizationSettings != null)
            {
                body.Add("ssoPageCustomizationSettings", option.SsoPageCustomizationSettings);
            }
            var res = await client.RequestCustomDataWithToken<bool>($"api/v2/tenant/{tenantId}", body.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 获取租户成员列表
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<Pagination<TenantMembers>> Members(string tenantId, TenantMembersOption option, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<Pagination<TenantMembers>>($"api/v2/tenant/{tenantId}/users?page={option.Page}&limit={option.Limit}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 添加租户成员
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        public async Task<TenantAddMembersResponse> AddMembers(string tenantId, string[] userIds, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<TenantAddMembersResponse>($"api/v2/tenant/{tenantId}/user", new Dictionary<string, object>() {
                { "userIds", userIds }
            }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 移除租户成员
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <param name="userId">用户 ID</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> RemoveMembers(string tenantId, string userId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/tenant/{tenantId}/user?userId={userId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 获取身份源列表
        /// </summary>
        /// <param name="tenantId">租户 ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<ExtIdpListOutput>> ListExtIdp(string tenantId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<IEnumerable<ExtIdpListOutput>>($"api/v2/extIdp?tenantId={tenantId}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 获取身份源详细信息
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <returns></returns>
        public async Task<ExtIdpDetailOutput> ExtIdpDetail(string extIdpId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<ExtIdpDetailOutput>($"api/v2/extIdp/{extIdpId}", method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 创建身份源
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<ExtIdpDetailOutput> CreateExtIdp(CreateExtIdpOption option, AuthingErrorBox authingErrorBox = null)
        {
            var connections = new List<Dictionary<string, string>>();
            for (var i = 0; i < option.Connections.Length; i++)
            {
                var con = new Dictionary<string, string>() {
                    { "type", option.Connections[i].Type.ToDescription() }
                };
                if (option.Connections[i].Identifier != null)
                {
                    con.Add("identifier", option.Connections[i].Identifier);
                }
                connections.Add(con);
            }
            var body = new Dictionary<string, object>() {
                { "type", option.Type.ToDescription() },
                { "connections", connections },
            };
            if (option.TenantId != null)
            {
                body.Add("tenantId", option.TenantId);
            }
            if (option.Name != null)
            {
                body.Add("name", option.Name);
            }
            var res = await client.RequestCustomDataWithToken<ExtIdpDetailOutput>("api/v2/extIdp", body.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 更新身份源配置
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> UpdateExtIdp(string extIdpId, UpdateExtIdpOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, string>() {
                { "name", option.Name }
            };
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/extIdp/{extIdpId}", body.ConvertJson(), method: HttpMethod.Put).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 删除身份源
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> DeleteExtIdp(string extIdpId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/extIdp/{extIdpId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 创建身份源连接
        /// </summary>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<ExtIdpConnDetailOutput> CreateExtIdpConnection(CreateExtIdpConnectionOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, object>() {
                { "extIdpId", option.ExtIdpId },
                { "type", option.Type.ToDescription() },
                { "identifier", option.Identifier },
                { "displayName", option.DisplayName },
                { "fields", option.Fields },
            };
            if (option.UserMatchFields != null)
            {
                body.Add("userMatchFields", option.UserMatchFields);
            }
            if (option.Logo != null)
            {
                body.Add("logo", option.Logo);
            }
            var res = await client.RequestCustomDataWithToken<ExtIdpConnDetailOutput>("api/v2/extIdpConn", body.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data;
        }

        /// <summary>
        /// 更新身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> UpdateExtIdpConnection(string extIdpConnectionId, UpdateExtIdpConnectionOption option, AuthingErrorBox authingErrorBox = null)
        {
            var body = new Dictionary<string, object>();
            if (option.DisplayName != null)
            {
                body.Add("displayName", option.DisplayName);
            }
            if (option.Fields != null)
            {
                body.Add("fields", option.Fields);
            }
            if (option.UserMatchFields != null)
            {
                body.Add("userMatchFields", option.UserMatchFields);
            }
            if (option.Logo != null)
            {
                body.Add("logo", option.Logo);
            }
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/extIdpConn/{extIdpConnectionId}", body.ConvertJson(), method: HttpMethod.Put, contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 删除身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <returns></returns>
        public async Task<GraphQLResponse<CommonMessage>> DeleteExtIdpConnection(string extIdpConnectionId, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/extIdpConn/{extIdpConnectionId}", method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res;
        }

        /// <summary>
        /// 检查连接唯一标识是否冲突
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<bool> CheckExtIdpConnectionIdentifierUnique(string identifier, AuthingErrorBox authingErrorBox = null)
        {
            try
            {
                var res = await client.RequestCustomDataWithToken<CommonMessage>("api/v2/check/extIdpConn/identifier", new Dictionary<string, string>() {
                    { "identifier", identifier }
                }.ConvertJson()).ConfigureAwait(false);
                ErrorHelper.LoadError(res, authingErrorBox);
                return res.Code == 200;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 开关身份源连接
        /// </summary>
        /// <param name="extIdpConnectionId">身份源连接 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<bool> ChangeExtIdpConnectionState(string extIdpConnectionId, ChangeExtIdpConnectionStateOption option, AuthingErrorBox authingErrorBox = null)
        {
            try
            {
                var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/extIdpConn/{extIdpConnectionId}/state", new Dictionary<string, object>() {
                    { "appId", option.AppId },
                    { "tenantId", option.TenantId },
                    { "enabled", option.Enabled}
                }.ConvertJson(), method: HttpMethod.Put, contenttype: ContentType.JSON).ConfigureAwait(false);
                ErrorHelper.LoadError(res, authingErrorBox);
                return res.Code == 200;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 批量开关身份源连接
        /// </summary>
        /// <param name="extIdpId">身份源 ID</param>
        /// <param name="option">选项</param>
        /// <returns></returns>
        public async Task<bool> BatchChangeExtIdpConnectionState(string extIdpId, ChangeExtIdpConnectionStateOption option, AuthingErrorBox authingErrorBox = null)
        {
            try
            {
                var res = await client.RequestCustomDataWithToken<CommonMessage>($"api/v2/extIdp/{extIdpId}/connState", new Dictionary<string, object>() {
                    { "appId", option.AppId },
                    { "tenantId", option.TenantId },
                    { "enabled", option.Enabled }
                }.ConvertJson(), method: HttpMethod.Put).ConfigureAwait(false);
                ErrorHelper.LoadError(res, authingErrorBox);
                return res.Code == 200;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
