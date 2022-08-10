using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Extensions;
using Authing.Library.Domain.Model.Exceptions;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Model.V3Model;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class OrgsManagementClient : IOrgsManagementClient
    {
        private ManagementClient client;

        public OrgsManagementClient(ManagementClient management)
        {
            client = management;
        }

        public async Task<Node> AddMembers(string nodeId, IEnumerable<string> userIds, AuthingErrorBox authingErrorBox = null)
        {
            var param = new AddMemberParam(userIds)
            {
                NodeId = nodeId
            };
            var res = await client.RequestCustomDataWithToken<AddMemberResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<Org> AddNode(string orgId, AddNodeParam addNodeParam, AuthingErrorBox authingErrorBox = null)
        {
            var param = new AddNodeParam(orgId, addNodeParam.ParentNodeId, addNodeParam.Name)
            {
                ParentNodeId = addNodeParam.ParentNodeId,
                Code = addNodeParam.Code,
                Order = addNodeParam.Order,
                NameI18n = addNodeParam.NameI18n,
                Description = addNodeParam.Description,
                DescriptionI18n = addNodeParam.DescriptionI18n
            };
            var res = await client.RequestCustomDataWithToken<AddNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);

            if (res.Data != null)
            {
                return res.Data.Result;
            }
            return null;
        }

        public async Task<Org> Create(string name, string description = null, string code = null, AuthingErrorBox authingErrorBox = null)
        {
            var param = new CreateOrgParam(name)
            {
                Description = description,
                Code = code
            };

            var res = await client.RequestCustomDataWithToken<CreateOrgResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteById(string id, AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteOrgParam(id);
            var res = await client.RequestCustomDataWithToken<DeleteOrgResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteNode(string orgId, string nodeId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteNodeParam(orgId, nodeId);
            var res = await client.RequestCustomDataWithToken<DeleteNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<IEnumerable<Org>> ExportAll(AuthingErrorBox authingErrorBox = null)
        {
            var result = await client.RequestCustomDataWithToken<IEnumerable<Org>>("api/v2/orgs/export", new ExpnadAllRequest().CreateRequest().ConvertJson()
                , method: HttpMethod.Get
                , contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        public async Task<Node> ExportByOrgId(string orgId, AuthingErrorBox authingErrorBox = null)
        {
            var result = await client.RequestCustomDataWithToken<Node>($"api/v2/orgs/export?org_id={orgId}", new ExpnadAllRequest().CreateRequest().ConvertJson()
                , method: HttpMethod.Get, contenttype: ContentType.JSON).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            //var res = await client.Host.AppendPathSegment($"api/v2/orgs/export?org_id={orgId}").WithOAuthBearerToken(client.AccessToken).GetJsonAsync<Node>().ConfigureAwait(false);
            return result.Data;
        }

        public async Task<Org> FindById(string orgId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new OrgParam(orgId);
            var res = await client.RequestCustomDataWithToken<OrgResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<Node> FindNodeById(string nodeId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new NodeByIdParam(nodeId);
            var res = await client.RequestCustomDataWithToken<NodeByIdResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<Org> ImportByJson(string json, AuthingErrorBox authingErrorBox = null)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"filetype","json"},
                {"file",json}
            };
            var result =
                await client.RequestCustomDataWithToken<Org>("api/v2/orgs/import",
                    keyValuePairs.ConvertJson()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        public async Task<bool?> IsRootNode(string orgId, string nodeId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new IsRootNodeParam(nodeId, orgId);
            var res = await client.RequestCustomDataWithToken<IsRootNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<PaginatedOrgs> List(int page = 1, int limit = 10, SortByEnum sortByEnum = SortByEnum.CREATEDAT_DESC, AuthingErrorBox authingErrorBox = null)
        {
            var param = new OrgsParam()
            {
                Limit = limit,
                Page = page
            };

            var res = await client.RequestCustomDataWithToken<OrgsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data.Result;
        }

        public async Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeCode(string orgId,
                                                                                          string code,
                                                                                          string nameSpace,
                                                                                          ResourceType resourceType = ResourceType.DATA,
                                                                                          AuthingErrorBox authingErrorBox = null)
        {
            var param = new ListNodeByCodeAuthorizedResourcesParam(orgId, code)
            {
                Namespace = nameSpace,
                ResourceType = resourceType.ToString().ToUpper()
            };
            var res = await client.RequestCustomDataWithToken<ListNodeByCodeAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var node = res.Data.Result;
            if (node == null)
            {
                throw new Exception("组织机构节点不存在");
            }
            return node.AuthorizedResources;
        }

        public async Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeId(string nodeId, string nameSpace, ResourceType resourceType = ResourceType.DATA, AuthingErrorBox authingErrorBox = null)
        {
            var param = new ListNodeByIdAuthorizedResourcesParam(nodeId)
            {
                Namespace = nameSpace,
                ResourceType = resourceType.ToString().ToUpper()
            };
            var res = await client.RequestCustomDataWithToken<ListNodeByIdAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var node = res.Data.Result;
            if (node == null)
            {
                throw new Exception("组织机构节点不存在");
            }
            return node.AuthorizedResources;
        }

        public async Task<IEnumerable<Node>> ListChildren(string orgId, string nodeId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new ChildrenNodesParam(nodeId);
            var res = await client.RequestCustomDataWithToken<ChildrenNodesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<PaginatedUsers> ListMembers(string nodeId, NodeByIdWithMembersParam nodeByIdWithMembersParam = default, AuthingErrorBox authingErrorBox = null)
        {
            nodeByIdWithMembersParam.Id = nodeId;
            var res = await client.RequestCustomDataWithToken<NodeByIdWithMembersResponse>(nodeByIdWithMembersParam.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result.Users;
        }

        public async Task<CommonMessage> MoveMembers(string sourceNodeId, string targetNodeId, IEnumerable<string> userIds, AuthingErrorBox authingErrorBox = null)
        {
            var moveMembersParam = new MoveMembersParam(userIds, sourceNodeId, targetNodeId);

            var res = await client.RequestCustomDataWithToken<MoveMembersResponse>(moveMembersParam.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Org> MoveNode(string orgId, string nodeId, string targetParentId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new MoveNodeParam(orgId, nodeId, targetParentId);
            var res = await client.RequestCustomDataWithToken<MoveNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<PaginatedUsers> RemoveMembers(string nodeId, IEnumerable<string> userIds, AuthingErrorBox authingErrorBox = null)
        {
            var param = new RemoveMemberParam(userIds)
            {
                NodeId = nodeId
            };
            var res = await client.RequestCustomDataWithToken<RemoveMemberResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result.Users;
        }

        public async Task<Node> RootNode(string orgId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new RootNodeParam(orgId);
            var res = await client.RequestCustomDataWithToken<RootNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<IEnumerable<Node>> SearchNodes(string keyword, AuthingErrorBox authingErrorBox = null)
        {
            var param = new SearchNodesParam(keyword);
            var res = await client.RequestCustomDataWithToken<SearchNodesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> SetMainDepartment(string userId, string departmentId, AuthingErrorBox authingErrorBox = null)
        {
            var param = new SetMainDepartmentParam(userId)
            {
                DepartmentId = departmentId
            };
            var res = await client.RequestCustomDataWithToken<SetMainDepartmentResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<bool> StartSync(ProviderTypeEnum providerTypeEnum, string adConnectorId = null, AuthingErrorBox authingErrorBox = null)
        {
            var body = new
            {
                connectionId = adConnectorId
            };
            var path = providerTypeEnum switch
            {
                ProviderTypeEnum.WECHATWORK => "connections/enterprise/wechatwork/start-sync",
                ProviderTypeEnum.DINGTALK => "connections/enterprise/dingtalk/start-sync",
                ProviderTypeEnum.AD => "api/v2/ad/sync",
                _ => throw new Exception("请检查输入参数providerTypeEnum")
            };
            if (body.connectionId == null && providerTypeEnum == ProviderTypeEnum.AD)
            {
                throw new Exception("must provider adConnectorId");
            }

            //var res = await client.Host.AppendPathSegment(path).WithOAuthBearerToken(client.AccessToken).PostJsonAsync(body).ReceiveJson<bool>().ConfigureAwait(false);

            var result = await client.Get<bool>(path, null).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data;
        }

        public async Task<Node> UpdateNode(string orgId, UpdateNodeParam updateNodeParam, AuthingErrorBox authingErrorBox = null)
        {
            var res = await client.RequestCustomDataWithToken<UpdateNodeResponse>(updateNodeParam.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonResponse<T>> SetPartMentCustomData<T>(string nodeid, string key, object value)
        {
            var param = new SetCustomDataParam()
            {
                _namespace = "",
                list = new List<Dic>() { new Dic() { key = key, value = value } },
                targetIdentifier = nodeid,
                targetType = TargetType.DEPARTMENT
            };
            var res = await client.RequestCustomDataWithTokenV3<T>("api/v3/set-custom-data",
                param.ConvertJson(), contenttype: ContentType.JSON);
            return res;
        }
    }
}
