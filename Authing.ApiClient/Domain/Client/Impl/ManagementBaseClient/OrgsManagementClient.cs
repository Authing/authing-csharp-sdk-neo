﻿using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Extensions;


namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class OrgsManagementClient : IOrgsManagementClient
    {
        private ManagementClient client;

        public OrgsManagementClient(ManagementClient management)
        {
            client = management;
        }

        public async Task<Node> AddMembers(string nodeId, IEnumerable<string> userIds)
        {
            var param = new AddMemberParam(userIds)
            {
                NodeId = nodeId
            };
            var res = await client.Post<AddMemberResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> AddNode(string orgId, AddNodeParam addNodeParam)
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
            var res = await client.Post<AddNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> Create(string name, string description = null, string code = null)
        {
            var param = new CreateOrgParam(name)
            {
                Description = description,
                Code = code
            };

            var res = await client.Post<CreateOrgResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteById(string id)
        {
            var param = new DeleteOrgParam(id);
            var res = await client.Post<DeleteOrgResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteNode(string orgId, string nodeId)
        {
            var param = new DeleteNodeParam(orgId, nodeId);
            var res = await client.Post<DeleteNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<IEnumerable<Authing.ApiClient.Domain.Model.Management.Orgs.Org>> ExportAll()
        {
            var result = await client.Get<IEnumerable<Authing.ApiClient.Domain.Model.Management.Orgs.Org>>("api/v2/orgs/export", new ExpnadAllRequest().CreateRequest()).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<Node> ExportByOrgId(string orgId)
        {
            var result = await client.Get<Node>($"api/v2/orgs/export?org_id={orgId}",new ExpnadAllRequest().CreateRequest()).ConfigureAwait(false);

            //var res = await client.Host.AppendPathSegment($"api/v2/orgs/export?org_id={orgId}").WithOAuthBearerToken(client.AccessToken).GetJsonAsync<Node>().ConfigureAwait(false);
            return result.Data;
        }

        public async Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> FindById(string orgId)
        {
            var param = new OrgParam(orgId);
            var res = await client.Post<OrgResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<Node> FindNodeById(string nodeId)
        {
            var param = new NodeByIdParam(nodeId);
            var res = await client.Post<NodeByIdResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> ImportByJson(string json)
        {
            Dictionary<string,string> keyValuePairs = new System.Collections.Generic.Dictionary<string, string>
            {
                {  "filetype","json" },
                { "file",json}
            };
            //var result = await client.Post<Authing.ApiClient.Domain.Model.Management.Orgs.Org>("api/v2/orgs/import", keyValuePairs);

            var result =
                await client.RequestCustomDataWithToken<Authing.ApiClient.Domain.Model.Management.Orgs.Org>("api/v2/orgs/import",
                    keyValuePairs.ConvertJson()).ConfigureAwait(false);

            return null;
        }

        public async Task<bool?> IsRootNode(string orgId, string nodeId)
        {
            var param = new IsRootNodeParam(nodeId, orgId);
            var res = await client.Post<IsRootNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<PaginatedOrgs> List(int page = 1, int limit = 10, SortByEnum sortByEnum = SortByEnum.CREATEDAT_DESC)
        {
            var param = new OrgsParam()
            {
                Limit = limit,
                Page = page
            };

            var res = await client.Post<OrgsResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeCode(string orgId, string code, string nameSpace, ResourceType resourceType = ResourceType.DATA)
        {
            var param = new ListNodeByCodeAuthorizedResourcesParam(orgId, code)
            {
                Namespace = nameSpace,
                ResourceType = resourceType.ToString().ToUpper()
            };
            var res = await client.Post<ListNodeByCodeAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            var node = res.Data.Result;
            if (node == null)
            {
                throw new Exception("组织机构节点不存在");
            }
            return node.AuthorizedResources;
        }

        public async Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeId(string nodeId, string nameSpace, ResourceType resourceType = ResourceType.DATA)
        {
            var param = new ListNodeByIdAuthorizedResourcesParam(nodeId)
            {
                Namespace = nameSpace,
                ResourceType = resourceType.ToString().ToUpper()
            };
            var res = await client.Post<ListNodeByIdAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            var node = res.Data.Result;
            if (node == null)
            {
                throw new Exception("组织机构节点不存在");
            }
            return node.AuthorizedResources;
        }

        public async Task<IEnumerable<Node>> ListChildren(string orgId, string nodeId)
        {
            var param = new ChildrenNodesParam(nodeId);
            var res = await client.Post<ChildrenNodesResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<PaginatedUsers> ListMembers(string nodeId, NodeByIdWithMembersParam nodeByIdWithMembersParam = default)
        {
            nodeByIdWithMembersParam.Id = nodeId;
            var res = await client.Post<NodeByIdWithMembersResponse>(nodeByIdWithMembersParam.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result.Users;
        }

        public async Task<CommonMessage> MoveMembers(string sourceNodeId, string targetNodeId, IEnumerable<string> userIds)
        {
            var moveMembersParam = new MoveMembersParam(userIds, sourceNodeId, targetNodeId) { };

            var res = await client.Post<MoveMembersResponse>(moveMembersParam.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> MoveNode(string orgId, string nodeId, string targetParentId)
        {
            var param = new MoveNodeParam(orgId, nodeId, targetParentId);
            var res = await client.Post<MoveNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<PaginatedUsers> RemoveMembers(string nodeId, IEnumerable<string> userIds)
        {
            var param = new RemoveMemberParam(userIds)
            {
                NodeId = nodeId
            };
            var res = await client.Post<RemoveMemberResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result.Users;
        }

        public async Task<Node> RootNode(string orgId)
        {
            var param = new RootNodeParam(orgId);
            var res = await client.Post<RootNodeResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<IEnumerable<Node>> SearchNodes(string keyword)
        {
            var param = new SearchNodesParam(keyword);
            var res = await client.Post<SearchNodesResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<CommonMessage> SetMainDepartment(string userId, string departmentId)
        {
            var param = new SetMainDepartmentParam(userId)
            {
                DepartmentId = departmentId
            };
            var res = await client.Post<SetMainDepartmentResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        public async Task<bool> StartSync(ProviderTypeEnum providerTypeEnum, string adConnectorId = null)
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

            return result.Data;
        }

        public async Task<Node> UpdateNode(string orgId, UpdateNodeParam updateNodeParam)
        {
            var res = await client.Post<UpdateNodeResponse>(updateNodeParam.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }
    }
}
