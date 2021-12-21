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
        Task<ApplicationList> List(int page = 1, int limit = 10);

        /// <summary>
        /// 创建应用
        /// </summary>
        /// <param name="name">应用名称</param>
        /// <param name="identifier">应用认证地址</param>
        /// <param name="redirectUris">应用回调链接</param>
        /// <param name="logo">应用 logo</param>
        /// <returns></returns>
        Task<Application> Create(string name, string identifier, string redirectUris, string logo = null);

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns></returns>
        Task<bool> Delete(string appId);

        /// <summary>
        /// 通过 ID 获取应用详情
        /// </summary>
        /// <param name="id">应用 ID</param>
        /// <returns></returns>
        Task<Application> FindById(string id);

        /// <summary>
        /// 获取应用的资源列表
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="listResourceOption">选项</param>
        /// <returns></returns>
        Task<ListResourceRes> ListResource(string appId, ListResourceOption listResourceOption = null);

        /// <summary>
        /// 为应用创建资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="createResourceParam"></param>
        /// <returns></returns>
        Task<Resources> CreateResource(string appId, CreateResourceParam createResourceParam);

        /// <summary>
        /// 删除应用的资源
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> DeleteResource(string appId, string code);
    }
}
