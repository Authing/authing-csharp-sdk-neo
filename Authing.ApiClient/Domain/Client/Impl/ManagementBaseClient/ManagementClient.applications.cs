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
using Authing.ApiClient.Extensions;
using Flurl;
using Flurl.Http;

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
        public async Task<ApplicationList> List(int page = 1, int limit = 10)
        {
            var res = await client.Host.AppendPathSegment("api/v2/applications").WithOAuthBearerToken(client.AccessToken).SetQueryParams(new
            {
                page,
                limit
            }).GetJsonAsync<ApplicationList>();
            return res;
        }

        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name">应用名称</param>
        /// <param name="identifier">应用认证地址</param>
        /// <param name="redirectUris">应用回调链接</param>
        /// <param name="logo">应用 logo</param>
        /// <returns></returns>
        public async Task<Application> Create(string name, string identifier, string redirectUris, string logo = null)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("name", name);
            keyValuePairs.Add("identifier", identifier);
            keyValuePairs.Add("redirectUris", redirectUris);
            if (logo != null) {
                keyValuePairs.Add("logo", logo);
            }
            var result = await client.Post<Application>("api/v2/applications", keyValuePairs);
            return result.Data;
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        public async Task<bool> Delete(string appId)
        {
            await client.Host.AppendPathSegment($"api/v2/applications/{appId}").WithOAuthBearerToken(client.AccessToken).DeleteAsync();
            return true;
        }

        /// <summary>
        /// 通过 ID 获取应用详情
        /// </summary>
        /// <param name="id">应用 ID</param>
        /// <returns></returns>
        public async Task<Application> FindById(string id)
        {
            var res = await client.Get<Application>($"api/v2/applications/{id}", new ExpnadAllRequest().CreateRequest());
            return res.Data;
        }

        /// <summary>
        /// 获取应用的资源列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="listResourceOption">选项</param>
        /// <returns></returns>
        public async Task<ListResourceRes> ListResource(
            string appId,
            ListResourceOption listResourceOption = null)
        {
            var param = new ListResourceParam() {
                Namespace = appId,
                Page = listResourceOption.Page,
                Limit = listResourceOption.Limit,
                Type = listResourceOption?.Type
            };
            var res = await client.Host.AppendPathSegment("api/v2/analysis/user-action").SetQueryParams(param).WithHeaders(client.GetAuthHeaders()).GetJsonAsync<ListResourceRes>();
            return res;
        }

        /// <summary>
        /// 为应用创建资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="createResourceParam"></param>
        /// <returns></returns>
        public async Task<Resources> CreateResource(string appId, CreateResourceParam createResourceParam)
        {
            createResourceParam.NameSpace = appId;
            var res = await client.Host.AppendPathSegment("api/v2/resources").WithOAuthBearerToken(client.AccessToken).PostJsonAsync(createResourceParam).ReceiveJson<Resources>();
            return res;
        }

        /// <summary>
        /// 更新应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <param name="updateResourceParam"></param>
        /// <returns></returns>
        public async Task<Resources> UpdateResource(string appId, string code, UpdateResourceParam updateResourceParam)
        {
            updateResourceParam.NameSpace = appId;
            var res = await client.Host.AppendPathSegment($"api/v2/resources/${code}").WithOAuthBearerToken(client.AccessToken).PostJsonAsync(updateResourceParam).ReceiveJson<Resources>();
            return res;
        }

        /// <summary>
        /// 删除应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> DeleteResource(string appId, string code)
        {
            var res = await client.Host.AppendPathSegment($"api/v2/resources/${code}").SetQueryParam("namespace", appId).WithOAuthBearerToken(client.AccessToken).DeleteAsync().ReceiveJson<CommonMessage>();
            return true;
        }

        /// <summary>
        /// 获取应用访问控制策略
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appAccessPolicyQueryFilter"></param>
        /// <returns></returns>
        public async Task<ApplicationAccessPolicies> GetAccessPolicies(string appId, AppAccessPolicyQueryFilter appAccessPolicyQueryFilter)
        {
            appAccessPolicyQueryFilter.AppId = appId;
            var res = await aclManagementClient.GetAccessPolicies(appAccessPolicyQueryFilter, cancellationToken);
            return res;
        }
    }
}
