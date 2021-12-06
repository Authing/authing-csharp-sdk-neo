using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;


namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class ManagementClientOrgs : IManagementClientOrgs
    {
        private ManagementClient client;

        public ManagementClientOrgs(ManagementClient management)
        {
            client = management;
        }

        public async Task<Node> AddMembers(string nodeId, IEnumerable<string> userIds)
        {
            var param = new AddMemberParam(userIds)
            {
                NodeId = nodeId
            };
            var res = await client.Post<GraphQLResponse<AddMemberResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Org> AddNode(string orgId, AddNodeParam addNodeParam)
        {
            var param = new AddNodeParam(orgId, addNodeParam.Name)
            {
                ParentNodeId = addNodeParam.ParentNodeId,
                Code = addNodeParam.Code,
                Order = addNodeParam.Order,
                NameI18n = addNodeParam.NameI18n,
                Description = addNodeParam.Description,
                DescriptionI18n = addNodeParam.DescriptionI18n
            };
            var res = await client.Post<GraphQLResponse<AddNodeResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Org> Create(string name, string description = null, string code = null)
        {
            var param = new CreateOrgParam(name)
            {
                Description = description,
                Code = code
            };

            var res = await client.Post<GraphQLResponse<CreateOrgResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteById(string id)
        {
            var param = new DeleteOrgParam(id);
            var res = await client.Post<GraphQLResponse<DeleteOrgResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteNode(string orgId, string nodeId)
        {
            var param = new DeleteNodeParam(orgId, nodeId);
            var res = await client.Post<GraphQLResponse<DeleteNodeResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<IEnumerable<Node>> ExportAll()
        {
            var res = await client.Host.AppendPathSegment("api/v2/orgs/export").WithOAuthBearerToken(client.AccessToken).GetJsonAsync<IEnumerable<Node>>();
            return res;
        }

        public async Task<Node> ExportByOrgId(string orgId)
        {
            var res = await client.Host.AppendPathSegment($"api/v2/orgs/export?org_id={orgId}").WithOAuthBearerToken(client.AccessToken).GetJsonAsync<Node>();
            return res;
        }

        public async Task<Org> FindById(string id)
        {
            var param = new OrgParam(id);
            var res = await client.Post<GraphQLResponse<OrgResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Node> FindNodeById(string nodeId)
        {
            var param = new NodeByIdParam(nodeId);
            var res = await client.Post<GraphQLResponse<NodeByCodeResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public Task<Org> ImportByJson(string json)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> IsRootNode(string orgId, string nodeId)
        {
            var param = new IsRootNodeParam(nodeId, orgId);
            var res = await client.Post<GraphQLResponse<IsRootNodeResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<PaginatedOrgs> List(int page = 1, int limit = 10, SortByEnum sortByEnum = SortByEnum.CREATEDAT_DESC)
        {
            var param = new OrgsParam()
            {
                Limit = limit,
                Page = page
            };

            var res = await client.Post<GraphQLResponse<OrgsResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeCode(string orgId,string code, string nameSpace, ResourceType resourceType = ResourceType.DATA)
        {
            var param = new ListNodeByCodeAuthorizedResourcesParam(orgId, code)
            {
                Namespace = nameSpace,
                ResourceType = resourceType.ToString().ToUpper()
            };
            var res = await client.Post<GraphQLResponse<ListNodeByCodeAuthorizedResourcesResponse>>(param.CreateRequest());
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
            var res = await client.Post<GraphQLResponse<ListNodeByIdAuthorizedResourcesResponse>>(param.CreateRequest());
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
            var res = await client.Post<GraphQLResponse<ChildrenNodesResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<PaginatedUsers> ListMembers(string nodeId, NodeByIdWithMembersParam nodeByIdWithMembersParam)
        {
            nodeByIdWithMembersParam.Id = nodeId;
            var res = await client.Post<GraphQLResponse<NodeByIdWithMembersResponse>>(nodeByIdWithMembersParam.CreateRequest());
            return res.Data.Result.Users;
        }

        public async Task<CommonMessage> MoveMembers(string sourceNodeId, string targetNodeId, IEnumerable<string> userIds)
        {
            var moveMembersParam = new MoveMembersParam(userIds, sourceNodeId, targetNodeId) { };

            var res = await client.Post<GraphQLResponse<MoveMembersResponse>>(moveMembersParam.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> MoveNode(string orgId, string nodeId, string targetParentId)
        {
            var param = new MoveNodeParam(orgId, nodeId, targetParentId);
            var res = await client.Post<GraphQLResponse<MoveMembersResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<PaginatedUsers> RemoveMembers(string nodeId, IEnumerable<string> userIds)
        {
            var param = new RemoveMemberParam(userIds)
            {
                NodeId = nodeId
            };
            var res = await client.Post<GraphQLResponse<RemoveMemberResponse>>(param.CreateRequest());
            return res.Data.Result.Users;
        }

        public async Task<Node> RootNode(string orgId)
        {
            var param = new RootNodeParam(orgId);
            var res = await client.Post<GraphQLResponse<RootNodeResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<IEnumerable<Node>> SearchNodes(string keyword)
        {
            var param = new SearchNodesParam(keyword);
            var res = await client.Post<GraphQLResponse<SearchNodesResponse>>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> SetMainDepartment(string userId, string departmentId)
        {
            var param = new SetMainDepartmentParam(userId)
            {
                DepartmentId = departmentId
            };
            var res = await client.Post<GraphQLResponse<SetMainDepartmentResponse>>(param.CreateRequest());
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

            var res = await client.Host.AppendPathSegment(path).WithOAuthBearerToken(client.AccessToken).PostJsonAsync(body).ReceiveJson<bool>();
            return res;
        }

        public async Task<Node> UpdateNode(string orgId, UpdateNodeParam updateNodeParam)
        {
            updateNodeParam.Id = orgId;
            var res = await client.Post<GraphQLResponse<UpdateNodeResponse>>(updateNodeParam.CreateRequest());
            return res.Data.Result;
        }
    }
}
