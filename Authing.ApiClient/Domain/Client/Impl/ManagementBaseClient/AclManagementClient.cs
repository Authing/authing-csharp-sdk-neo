﻿using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Acl;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Utils;
using Authing.Library.Domain.Model.Exceptions;
using Authing.Library.Domain.Client.Impl;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{

    /// <summary>
    /// 权限控制类
    /// </summary>
    public class AclManagementClient : IAclManagementClient
    {
        private readonly ManagementClient client;

        /// <summary>
        /// 权限控制类构造器
        /// </summary>
        /// <param name="client">ManagementClient 实例</param>
        public AclManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 创建权限分组
        /// </summary>
        /// <param name="code">权限分组唯一标识符</param>
        /// <param name="name">权限分组名</param>
        /// <param name="description">可选，权限分组描述</param>
        /// <returns></returns>
        public async Task<NameSpace> CreateNamespace(string code, string name, string description, AuthingErrorBox authingErrorBox = null)
        {
            //var res = await client.Post<NameSpace>($"api/v2/resource-namespace/{client.UserPoolId}",
            //    new Dictionary<string, string>()
            //    {
            //        { nameof(code), code },
            //        { nameof(name), name },
            //        { nameof(description), description },
            //    });
            var res = await client.RequestCustomDataWithToken<NameSpace>($"api/v2/resource-namespace/{client.UserPoolId}",
                new Dictionary<string, string>()
                {
                        { nameof(code), code },
                        { nameof(name), name },
                        { nameof(description), description },
                }.ConvertJson()).ConfigureAwait(false);

            ErrorHelper.LoadError(res, authingErrorBox);

            return res.Data ?? null;
        }

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="page">页码，默认为 1</param>
        /// <param name="limit">每页个数，默认为 10</param>
        /// <returns></returns>
        public async Task<Namespaces> ListNamespaces(int page = 1, int limit = 10, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<Namespaces>(
                $"api/v2/resource-namespace/{client.UserPoolId}?limit={limit}&page={page}"
                , method: HttpMethod.Get).ConfigureAwait(false);

            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        /// <summary>
        /// 更新权限分组
        /// </summary>
        /// <param name="code">权限分组 ID</param>
        /// <param name="updateNamespaceParam">需要更新的数据
        /// updates.code <String> 可选，权限分组唯一标识符。
        /// updates.name<String> 可选，权限分组名称。
        /// updates.description<String> 可选，权限分组描述。
        /// </param>
        /// <returns></returns>
        public async Task<NameSpace> UpdateNamespace(string nameSpaceId, UpdateNamespaceParam updateNamespaceParam, AuthingErrorBox authingErrorBox = null)
        {
            if (updateNamespaceParam == null) throw new ArgumentNullException(nameof(updateNamespaceParam));

            //var res = await client.Put<NameSpace>($"api/v2/resource-namespace/{client.UserPoolId}/{nameSpaceId}",
            //    new Dictionary<string, string>()
            //    {
            //        { nameof(updateNamespaceParam.Code), updateNamespaceParam.Code },
            //        { nameof(updateNamespaceParam.Name), updateNamespaceParam.Name },
            //        { nameof(updateNamespaceParam.Description), updateNamespaceParam.Description }
            //    });

            var res = await client.RequestCustomDataWithToken<NameSpace>($"api/v2/resource-namespace/{client.UserPoolId}/{nameSpaceId}",
                new Dictionary<string, string>()
                {
                        { nameof(updateNamespaceParam.Code), updateNamespaceParam.Code },
                        { nameof(updateNamespaceParam.Name), updateNamespaceParam.Name },
                        { nameof(updateNamespaceParam.Description), updateNamespaceParam.Description }
                }.ConvertJson(), method: HttpMethod.Put, contenttype: ContentType.JSON).ConfigureAwait(false);

            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="code">权限分组 ID</param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteNamespace(int code, AuthingErrorBox authingErrorBox = null)
        {
            //var res = await client.Delete<object>($"api/v2/resource-namespace/{client.UserPoolId}/{code}",
            //    new GraphQLRequest());
            var res = await client.RequestCustomDataWithToken<object>(
                $"api/v2/resource-namespace/{client.UserPoolId}/{code}", method: HttpMethod.Delete).ConfigureAwait(false);

            ErrorHelper.LoadError(res, authingErrorBox);

            CommonMessage commonMessage = new CommonMessage()
            {
                Code = res.Code,
                Message=res.Message
            };

            return commonMessage;
        }

        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="resourceQueryFilter">过滤参数类
        /// namespace 权限分组唯一标识符
        /// type 资源类型，可选值为 DATA、API、MENU、UI、BUTTON
        /// fetchAll 是否拉取全部，true：是 false：否
        /// 每页条目数量，默认是 10
        /// 分页，获取第几页，默认从 1 开始
        /// </param>
        /// <returns></returns>
        public async Task<ListResourcesRes> ListResources(ResourceQueryFilter resourceQueryFilter, AuthingErrorBox authingErrorBox = null)
        {
            if (resourceQueryFilter == null) throw new ArgumentNullException(nameof(resourceQueryFilter));
            string endPoint = $"api/v2/resources?";
            endPoint += string.IsNullOrWhiteSpace(resourceQueryFilter.NameSpaceCode)
                ? ""
                : $"&namespace={resourceQueryFilter.NameSpaceCode}";
            endPoint += $"&type={resourceQueryFilter.Type}";
            endPoint += $"&limit={(resourceQueryFilter.FetchAll ? "-1" : $"{resourceQueryFilter.Limit}")}";
            endPoint += $"&page={resourceQueryFilter.Page}";

            var res = await client.RequestCustomDataWithToken<ListResourcesRes>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="resourceQueryFilter">过滤参数类</param>
        /// <returns></returns>
        public async Task<ListResourcesRes> GetResources(ResourceQueryFilter resourceQueryFilter, AuthingErrorBox authingErrorBox = null)
        {
            if (resourceQueryFilter == null) throw new ArgumentNullException(nameof(resourceQueryFilter));
            string endPoint = $"api/v2/resources?";
            endPoint += string.IsNullOrWhiteSpace(resourceQueryFilter.NameSpaceCode)
                ? ""
                : $"&namespace={resourceQueryFilter.NameSpaceCode}";
            endPoint += $"&type={resourceQueryFilter.Type}";
            endPoint += $"&limit={(resourceQueryFilter.FetchAll ? "-1" : $"{resourceQueryFilter.Limit}")}";
            endPoint += $"&page={resourceQueryFilter.Page}";

            var res = await client.RequestCustomDataWithToken<ListResourcesRes>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="createResourceParam">创建资源参数类
        /// code 资源标识符
        /// namespace 权限分组 ID
        /// type 资源类型，可选值为 DATA、API、MENU、UI、BUTTON
        /// actions 资源操作对象数组。其中 name 为操作名称，填写一个动词，description 为操作描述，填写述信息
        /// description 资源描述信息
        /// </param>
        /// <returns></returns>
        public async Task<Resources> CreateResource(ResourceParam createResourceParam, AuthingErrorBox authingErrorBox = null)
        {
            if (createResourceParam == null) throw new ArgumentNullException(nameof(createResourceParam));

            if (createResourceParam.Code == null)
            {
                throw new ArgumentException("请为资源设定一个资源标识符");
            }

            if (createResourceParam.Actions?.Count() < 1)
            {
                throw new ArgumentException("请至少定义一个资源操作");
            }

            if (createResourceParam.NameSpace == null)
            {
                throw new ArgumentException("请传入权限分组标识符");
            }

            var res = await client.RequestCustomDataWithToken<Resources>("api/v2/resources",
                new Dictionary<string, object>()
                {
                        {nameof(createResourceParam.Code).ToLower(),createResourceParam.Code},
                        {nameof(createResourceParam.Actions).ToLower(),createResourceParam.Actions},
                        {nameof(createResourceParam.ApiIdentifier).ToLower(),createResourceParam.ApiIdentifier ?? ""},
                        {nameof(createResourceParam.NameSpace).ToLower(),createResourceParam.NameSpace},
                        {nameof(createResourceParam.Type).ToLower(),createResourceParam.Type.ToString()},
                        {nameof(createResourceParam.Description).ToLower(),createResourceParam.Description},
                }.ConvertJson(),contenttype: ContentType.JSON).ConfigureAwait(false);

            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        /// <summary>
        /// 根据资源代码查询资源
        /// </summary>
        /// <param name="code">资源名称</param>
        /// <param name="nameSpace">分组 Code</param>
        /// <returns></returns>
        public async Task<Resources> FindResourceByCode(string code, string nameSpace = "", AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/resources/by-code/{code}";
            endPoint += string.IsNullOrWhiteSpace(nameSpace) ? "" : $"?namespace={nameSpace}";
            var resut = await client.RequestCustomDataWithToken<Resources>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(resut, authingErrorBox);
            return resut.Data ?? null;
        }

        /// <summary>
        /// 根据资源 ID 查询资源
        /// </summary>
        /// <param name="id">资源 ID</param>
        /// <returns></returns>
        public async Task<Resources> GetResourceById(string id, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/resources/detail?id={id}";
            var resut = await client.RequestCustomDataWithToken<Resources>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(resut, authingErrorBox);
            return resut.Data ?? null;

        }

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="code">资源标识符</param>
        /// <param name="options">资源信息对象
        ///  options.Namespace <String> 资源所在的权限分组唯一标识符
        ///  options.Type 资源类型，可选值为 ResourceType.DATA、ResourceType.API、ResourceType.MENU、ResourceType.UI、ResourceType.BUTTON。
        ///  options.Actions<List<ResourceAction>> 资源操作对象数组。其中 name 为操作名称，填写一个动词，description 为操作描述，填写描述信息。
        ///  options.description<String> 资源描述信息
        ///  ResourceAction : Name<String> 操作名称
        ///  ResourceAction : Description<String> 描述信息
        /// </param>
        /// <returns></returns>
        public async Task<Resources> UpdateResource(string code, ResourceParam options, AuthingErrorBox authingErrorBox = null)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(options.NameSpace)) throw new ArgumentException($"Parameter {nameof(options.NameSpace)} is request!");
            string endPoint = $"api/v2/resources/{code}";
            var result = await client.RequestCustomDataWithToken<Resources>(endPoint,
                new Dictionary<string, object>()
                {
                        {nameof(options.Code).ToLower(),code},
                        {nameof(options.Actions).ToLower(),options.Actions},
                        {nameof(options.ApiIdentifier).ToLower(),options.ApiIdentifier ?? ""},
                        {nameof(options.NameSpace).ToLower(),options.NameSpace},
                        {nameof(options.Type).ToLower(),options.Type.ToString()},
                        {nameof(options.Description).ToLower(),options.Description},
                }.ConvertJson(), contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data ?? null;
        }

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="code">资源标识符</param>
        /// <param name="namespacecode">权限分组 ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteResource(string code, string namespacecode, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/resources/{code}?namespace={namespacecode}";
            //var result = await client.Delete<bool>(endPoint, new GraphQLRequest());
            var result = await client.RequestCustomDataWithToken<bool>(endPoint, method: HttpMethod.Delete).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        /// <summary>
        /// 允许某个用户对某个资源进行某个操作
        /// </summary>
        /// <param name="userid">用户 ID</param>
        /// <param name="action"> 操作名称，推荐使用 <resourceType>:<actionName> 的格式，如 books:edit，books:list</param>
        /// <param name="resource">资源名称，必须为 <resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*</param>
        /// <param name="nameSpace">权限分组唯一标识符</param>
        /// <returns></returns>
        public async Task<CommonMessage> Allow(string userid, string nameSpace, string resource, string action, AuthingErrorBox authingErrorBox = null)
        {
            //action.CheckParameter();
            resource.CheckParameter();
            var param = new AllowParam(resource, action)
            {
                UserId = userid,
                Resource = resource,
                Namespace = nameSpace
            };
            var res = await client.RequestCustomDataWithToken<AllowResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 批量撤回资源
        /// 批量撤回某个授权主体的资源
        /// </summary>
        /// <param name="options">
        /// options.namespace <String> 权限分组唯一标识符，详情请见使用权限分组管理权限资源。
        /// options.resource<String> 资源名称，必须为<resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*。
        /// options.opts<List<RevokeResourceOpt>>
        /// options.opts.targetType. <PolicyAssignmentTargetType> 被授权主体类型
        /// options.opts.targetIdentifier. <String> 被授权主体唯一标识
        /// </param>
        /// <returns></returns>
        public async Task<CommonMessage> RevokeResource(RevokeResourceParams options, AuthingErrorBox authingErrorBox = null)
        {
            options.Resource.CheckParameter();
            string endPoint = "api/v2/acl/revoke-resource";
            //var result = await client.PostRaw<CommonMessage>(endPoint,options.ConvertJson());
            var result = await client.RequestCustomDataWithToken<CommonMessage>(endPoint, options.ConvertJson(),
                contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return new CommonMessage() { Code = result.Code, Message = result.Message };
        }

        /// <summary>
        /// 判断某个用户是否对某个资源有某个操作权限
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="action">操作名称，推荐使用 <resourceType>:<actionName> 的格式，如 books:edit，books:list</param>
        /// <param name="resource">资源名称，必须为 <resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*</param>
        /// <param name="namespacecode">权限分组唯一标识符</param>
        /// <returns></returns>
        public async Task<bool> IsAllowed(string userId, string resource, string action, string namespacecode = "", AuthingErrorBox authingErrorBox = null)
        {
            action.CheckParameter();
            resource.CheckParameter();
            var result = await client.RequestCustomDataWithToken<IsActionAllowedResponse>(new IsActionAllowedParam(resource, action, userId, namespacecode).CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data.Result;
        }

        /// <summary>
        /// 获取用户或角色或分组或部门被授权的所有资源列表
        /// </summary>
        /// <param name="targetType">被授权主体类型</param>
        /// <param name="targetIdentifier">被授权主体唯一标识</param>
        /// <param name="namespacecode">权限分组唯一标识符</param>
        /// <param name="options">
        /// options.resourceType <String> 可选，资源类型，默认会返回所有有权限的资源，现有资源类型如下：
        /// DATA：数据类型；
        /// API：API 类型数据；
        /// MENU：菜单类型数据；
        /// BUTTON：按钮类型数据
        /// </param>
        /// <returns></returns>
        public async Task<PaginatedAuthorizedResources> ListAuthorizedResources(PolicyAssignmentTargetType targetType,
                                                                                string targetIdentifier,
                                                                                string namespacecode,
                                                                                ListAuthorizedResourcesOptions options,
                                                                                AuthingErrorBox authingErrorBox = null)
        {
            var param = new ListAuthorizedResourcesParam(targetType, targetIdentifier, namespacecode,
                options.ResourceType);
            var result = await client.RequestCustomDataWithToken<ListAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data.AuthorizedResources ?? null;
        }

        /// <summary>
        /// 获取具备某些资源操作权限的主体、
        /// 传入权限分组、资源标识、资源类型、操作权限项、主体类型，返回具备资源操作权限的主体标识符
        /// </summary>
        /// <param name="getAuthorizedTargetsOptions">
        /// options.namespace <String> 权限分组唯一标识符，详情请见使用权限分组管理权限资源。
        /// options.resourceType<ResourceType> 资源类型，现有资源类型如下：
        /// DATA：数据类型；
        /// API：API 类型数据；
        /// MENU：菜单类型数据；
        /// BUTTON：按钮类型数据。
        /// options.actions<AuthorizedTargetsActionsInput> 操作
        /// actions.op<String> 可选值为 AND、OR，表示 list 中的操作关系是和还是或。
        /// actions.list<List<String>> 操作，例如['read', 'write']。
        /// options.targetType<PolicyAssignmentTargetType> 主体类型，可选值为 USER、ROLE、ORG、GROUP，含义为用户、角色、组织机构节点、用户分组
        /// </param>
        /// <returns></returns>
        public async Task<PaginatedAuthorizedTargets> GetAuthorizedTargets(GetAuthorizedTargetsOptions getAuthorizedTargetsOptions, AuthingErrorBox authingErrorBox = null)
        {
            if (getAuthorizedTargetsOptions.NameSpace == null)
            {
                throw new ArgumentException("请传入 options.namespace，含义为权限分组标识");
            }
            if (getAuthorizedTargetsOptions.Resource == null)
            {
                throw new ArgumentException("请传入 options.resource，含义为资源标识");
            }
            if (getAuthorizedTargetsOptions.ResourceType == null)
            {
                throw new ArgumentException("请传入 options.resourceType，含义为资源类型");
            }
            var param = new AuthorizedTargetsParam(getAuthorizedTargetsOptions.NameSpace, getAuthorizedTargetsOptions.ResourceType.Value, getAuthorizedTargetsOptions.Resource)
            {
                Actions = getAuthorizedTargetsOptions.Actions,
                TargetType = getAuthorizedTargetsOptions.TargetType
            };
            var res = await client.RequestCustomDataWithToken<AuthorizedTargetsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 资源授权
        /// 将一个（类）资源授权给用户、角色、分组、组织机构，且可以分别指定不同的操作权限
        /// </summary>
        /// <param name="namespacecode">分组唯一标识符</param>
        /// <param name="resource">资源名称</param>
        /// <param name="authorizeResourceOptions">资源授权对象
        /// targetType 主体类型，可选值为 USER、ROLE、GROUP、ORG
        /// targetIdentifier 主体标识符，可以为用户 id、角色标识符、分组标识符、组织机构节点标识符
        /// actions 资源操作对象的名称的集合
        /// </param>
        /// <returns></returns>
        public async Task<CommonMessage> AuthorizeResource(string namespacecode, string resource, IEnumerable<AuthorizeResourceOpt> authorizeResourceOptions, AuthingErrorBox authingErrorBox = null)
        {
            resource.CheckParameter();
            var param = new AuthorizeResourceParam()
            {
                Namespace = namespacecode,
                Resource = resource,
                Opts = authorizeResourceOptions
            };
            var res = await client.RequestCustomDataWithToken<AuthorizeResourceResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result ?? null;
        }

        public async Task<Pagination<ProgrammaticAccessAccount>> ProgrammaticAccessAccountList(ProgrammaticAccessAccountListProps options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/programmatic-access-accounts?limit=${options.Limit}&page=${options.Page}";
            var res = await client.RequestCustomDataWithToken<Pagination<ProgrammaticAccessAccount>>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        public async Task<ProgrammaticAccessAccount> CreateProgrammaticAccessAccount(string appId, CreateProgrammaticAccessAccountParam createProgrammaticAccessAccountParam, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{appId}/programmatic-access-accounts";
            //var res = await client.Post<ProgrammaticAccessAccount>(endPoint,
            //    new Dictionary<string, string>()
            //    {
            //        {
            //            nameof(createProgrammaticAccessAccountParam.AppId).ToLower(),
            //            createProgrammaticAccessAccountParam.AppId
            //        },
            //        {
            //            nameof(createProgrammaticAccessAccountParam.Remarks).ToLower(),
            //            createProgrammaticAccessAccountParam.Remarks
            //        },
            //        {
            //            //token_lifetime
            //            nameof(createProgrammaticAccessAccountParam.Token_lifetime).ToLower(),
            //            createProgrammaticAccessAccountParam.Token_lifetime.ToString()
            //        }
            //    });
            var res = await client.RequestCustomDataWithToken<ProgrammaticAccessAccount>(endPoint,
                new Dictionary<string, string>()
                    {
                            {
                                nameof(createProgrammaticAccessAccountParam.AppId).ToLower(),
                                createProgrammaticAccessAccountParam.AppId
                            },
                            {
                                nameof(createProgrammaticAccessAccountParam.Remarks).ToLower(),
                                createProgrammaticAccessAccountParam.Remarks
                            },
                            {
                                //token_lifetime
                                nameof(createProgrammaticAccessAccountParam.Token_lifetime).ToLower(),
                                createProgrammaticAccessAccountParam.Token_lifetime.ToString()
                            }
                    }.ConvertJson()).ConfigureAwait(false);

            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data ?? null;
        }

        public async Task<bool> DeleteProgrammaticAccessAccount(string programmaticAccessAccountId, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint =
                $"v2/applications/programmatic-access-accounts?id={programmaticAccessAccountId}";

            //var result = await client.Delete<RestfulResponse<bool>>(endPoint, new GraphQLRequest());
            var result = await client.RequestCustomDataWithToken<bool>(endPoint, method: HttpMethod.Delete).ConfigureAwait(false);

            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        public async Task<ProgrammaticAccessAccount> EnableProgrammaticAccessAccount(string programmaticAccessAccountId, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = "v2/applications/programmatic-access-accounts";
            //var result = await client.Patch<ProgrammaticAccessAccount>(endPoint,
            //    new Dictionary<string, string>()
            //    {
            //        {"id",programmaticAccessAccountId},
            //        {"enable","true"}
            //    });
            var result = await client.RequestCustomDataWithToken<ProgrammaticAccessAccount>(endPoint,
                new Dictionary<string, string>()
                {
                        {"id",programmaticAccessAccountId},
                        {"enable","true"}
                }.ConvertJson(), method: new HttpMethod("PATCH")).ConfigureAwait(false);

            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data ?? null;
        }

        public async Task<ProgrammaticAccessAccount> DisableProgrammaticAccessAccount(string programmaticAccessAccountId, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = "v2/applications/programmatic-access-accounts";
            //var result = await client.Post<ProgrammaticAccessAccount>(endPoint,
            //    new Dictionary<string, string>()
            //    {
            //        {"id",programmaticAccessAccountId},
            //        {"enable","false"}
            //    });
            var result = await client.RequestCustomDataWithToken<ProgrammaticAccessAccount>(endPoint,
                new Dictionary<string, string>()
                {
                        { "id", programmaticAccessAccountId },
                        { "enable", "false" }
                }.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data ?? null;
        }

        public async Task<ProgrammaticAccessAccount> RefreshProgrammaticAccessAccountSecret(ProgrammaticAccessAccountProps options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/programmatic-access-accounts";
            options.Secret ??= AuthingUtils.GenerateRandomString(32);
            //var result = await client.Patch<ProgrammaticAccessAccount>(endPoint, new Dictionary<string, string>()
            //{
            //    {nameof(options.Id).ToLower(),options.Id},
            //    {nameof(options.Secret).ToLower(),options.Secret}
            //});
            var result = await client.RequestCustomDataWithToken<ProgrammaticAccessAccount>(endPoint,
                new Dictionary<string, string>()
                {
                        {nameof(options.Id).ToLower(),options.Id},
                        {nameof(options.Secret).ToLower(),options.Secret}
                }.ConvertJson(), method: new HttpMethod("PATCH")).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data ?? null;
        }

        public async Task<Pagination<ApplicationAccessPolicies>> GetApplicationAccessPolicies(AppAccessPolicyQueryFilter options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/authorization/records?limit={options.Limit}&page={options.Page}";
            var result = await client.RequestCustomDataWithToken<Pagination<ApplicationAccessPolicies>>(endPoint, method: HttpMethod.Get).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data ?? null;
        }

        /// <summary>
        /// 启用应用访问控制策略
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// TargetType 对象类型
        /// TartgetIdentifiers 对象 ID 集合
        /// NameSpace 权限分组唯一标识符
        /// InheritByChildren 是否内联子类
        /// </param>
        /// <returns></returns>
        public async Task<bool> EnableApplicationAccessPolicy(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/authorization/enable-effect";
            //var result = await client.PostRaw<RestfulResponse<bool>>(endPoint,options.ConvertJson());
            var result =
                await client.RequestCustomDataWithToken<bool>(endPoint, options.ConvertJson(),
                    contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        /// <summary>
        /// 停用应用访问控制策略
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// TargetType 对象类型
        /// TartgetIdentifiers 对象 ID 集合
        /// NameSpace 权限分组唯一标识符
        /// InheritByChildren 是否内联子类
        /// </param>
        /// <returns></returns>
        public async Task<bool> DisableApplicationAccessPolicy(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/authorization/disable-effect";
            //var result = await client.PostRaw<RestfulResponse<bool>>(endPoint,options.ConvertJson());
            var result =
                await client.RequestCustomDataWithToken<bool>(endPoint, options.ConvertJson(),
                    contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        /// <summary>
        /// 删除应用访问控制策略
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// TargetType 对象类型
        /// TartgetIdentifiers 对象 ID 集合
        /// NameSpace 权限分组唯一标识符
        /// InheritByChildren 是否内联子类
        /// </param>
        /// <returns></returns>
        public async Task<bool> DeleteApplicationAccessPolicy(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/authorization/revoke";
            //var result = await client.PostRaw<RestfulResponse<bool>>(endPoint,options.ConvertJson());
            var result =
                await client.RequestCustomDataWithToken<bool>(endPoint, options.ConvertJson(),
                    contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        /// <summary>
        /// 配置「允许主体（用户、角色、分组、组织机构节点）访问应用」的控制策略
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// TargetType 对象类型
        /// TartgetIdentifiers 对象 ID 集合
        /// NameSpace 权限分组唯一标识符
        /// InheritByChildren 是否内联子类
        /// </param>
        /// <returns></returns>
        public async Task<bool> AllowAccessApplication(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/authorization/allow";
            //var result = await client.PostRaw<RestfulResponse<bool>>(endPoint,options.ConvertJson());
            var result =
                await client.RequestCustomDataWithToken<bool>(endPoint, options.ConvertJson(),
                    contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        /// <summary>
        /// 配置「拒绝主体（用户、角色、分组、组织机构节点）访问应用」的控制策略
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// TargetType 对象类型
        /// TartgetIdentifiers 对象 ID 集合
        /// NameSpace 权限分组唯一标识符
        /// InheritByChildren 是否内联子类
        /// </param>
        /// <returns></returns>
        public async Task<bool> DenyAccessApplication(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}/authorization/deny";
            //var result = await client.PostRaw<RestfulResponse<bool>>(endPoint,options.ConvertJson());
            var result =
                await client.RequestCustomDataWithToken<bool>(endPoint, options.ConvertJson(),
                    contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Code == 200;
        }

        /// <summary>
        /// 更改默认应用访问策略（默认拒绝所有用户访问应用、默认允许所有用户访问应用）
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// defaultStrategy 默认策略 取值范围 ALLOW_ALL,DENY_ALL
        /// </param>
        /// <returns></returns>
        public async Task<Application> UpdateDefaultApplicationAccessPolicy(DefaultAppAccessPolicy options, AuthingErrorBox authingErrorBox = null)
        {
            string endPoint = $"api/v2/applications/{options.AppId}";
            //var result = await client.Post<Application>(endPoint,
            //    new Dictionary<string, string>()
            //    {
            //        {nameof(options.AppId).ToLower(),options.AppId},
            //        {nameof(options.DefaultStrategy).ToLower(),options.DefaultStrategy.ToString()}
            //    });
            var result = await client.RequestCustomDataWithToken<Application>(endPoint,
                new Dictionary<string, string>()
                {
                        {nameof(options.AppId).ToLower(),options.AppId},
                        {nameof(options.DefaultStrategy).ToLower(),options.DefaultStrategy.ToString()}
                }.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data ?? null;
        }
    }
}
