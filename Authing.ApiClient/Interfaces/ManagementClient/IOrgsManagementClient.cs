using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Orgs;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.Library.Domain.Model.V3Model;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IOrgsManagementClient
    {
        /// <summary>
        /// 创建组织机构
        /// </summary>
        /// <param name="name"> 组织机构名称，该名称会作为该组织机构根节点的名称。</param>
        /// <param name="description">根节点描述 </param>
        /// <param name="code">根节点唯一标志，必须为合法的英文字符。</param>
        /// <returns></returns>
        Task<Org> Create(string name,string description=null,string code=null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="id"> 组织机构 ID</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteById(string id,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取用户池组织机构列表
        /// </summary>
        /// <param name="page">页码，默认值：1</param>
        /// <param name="limit">每页展示条数，默认值：10</param>
        /// <param name="sortByEnum">排序规则，默认值：按照创建时间降序</param>
        /// <returns></returns>
        Task<PaginatedOrgs> List(int page = 1, int limit = 10,SortByEnum sortByEnum=SortByEnum.CREATEDAT_DESC,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 根据节点 Id 查询节点
        /// </summary>
        /// <param name="nodeId">节点 ID</param>
        /// <returns></returns>
        Task<Node> FindNodeById(string nodeId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="orgId">组织机构 ID</param>
        /// <param name="addNodeParam">节点信息</param>
        /// <returns></returns>
        Task<Org> AddNode(string orgId,AddNodeParam addNodeParam,AuthingErrorBox authingErrorBox=null);


        /// <summary>
        /// 修改节点
        /// </summary>
        /// <param name="orgId">组织机构 ID</param>
        /// <param name="updateNodeParam">修改节点数据</param>
        /// <returns></returns>
        Task<Node> UpdateNode(string orgId, UpdateNodeParam updateNodeParam,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 通过组织机构 ID 获取组织机构详情
        /// </summary>
        /// <param name="id">组织机构 ID</param>
        /// <returns></returns>
        Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> FindById(string orgId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        ///  删除组织机构树中的某一个节点
        /// </summary>
        /// <param name="orgId">组织机构 ID</param>
        /// <param name="nodeId">节点 ID</param>
        /// <returns></returns>
        Task<CommonMessage> DeleteNode(string orgId,string nodeId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 移动节点
        /// 移动组织机构节点，移动某节点时需要指定该节点新的父节点。注意不能将一个节点移动到自己的子节点下面
        /// </summary>
        /// <param name="orgId">组织机构 ID</param>
        /// <param name="nodeId">需要移动的节点 ID</param>
        /// /// <param name="targetParentId">目标父节点 ID</param>
        /// <returns></returns>
        Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> MoveNode(string orgId, string nodeId,string targetParentId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 判断一个节点是不是组织树的根节点
        /// </summary>
        /// <param name="orgId">节点 ID</param>
        /// <param name="nodeId">组织机构 ID</param>
        /// <returns></returns>
        Task<bool?> IsRootNode(string orgId, string nodeId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取子节点列表,查询一个节点的子节点列表
        /// </summary>
        /// <param name="orgId">组织机构 ID</param>
        /// <param name="nodeId">节点 ID</param>
        /// <returns></returns>
        Task<IEnumerable<Node>> ListChildren(string orgId,string nodeId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 模糊搜索组织节点
        /// </summary>
        /// <param name="keyword">组织机构名称关键字</param>
        /// <returns></returns>
        Task<IEnumerable<Node>> SearchNodes(string keyword,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取一个组织的根节点
        /// </summary>
        /// <param name="orgId">组织机构 ID</param>
        /// <returns></returns>
        Task<Node> RootNode(string orgId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 通过 JSON 导入
        /// </summary>
        /// <param name="json">JSON 格式的树结构</param>
        /// <returns></returns>
        Task<Authing.ApiClient.Domain.Model.Management.Orgs.Org> ImportByJson(string json,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <param name="userIds"> 用户 ID 列表</param>
        /// <returns></returns>
        Task<Node> AddMembers(string nodeId, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 移动成员
        /// </summary>
        /// <param name="sourceNodeId">源节点 ID</param>
        /// <param name="targetNodeId">目标节点 ID</param>
        /// <param name="userIds"> 用户 ID 列表</param>
        /// <returns></returns>
        Task<CommonMessage> MoveMembers(string sourceNodeId, string targetNodeId, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 设置用户主部门
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="departmentId">主部门ID</param>
        /// <returns></returns>
        Task<CommonMessage> SetMainDepartment(string userId, string departmentId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 组织机构同步
        /// </summary>
        /// <param name="providerTypeEnum">可选类型：dingtalk-钉钉 wechatwork-企业微信 ad-AD</param>
        /// <param name="adConnectorId">AD Connector ID，providerType 为 AD 时必传。</param>
        /// <returns></returns>
        Task<bool> StartSync(ProviderTypeEnum providerTypeEnum, string adConnectorId = null,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 获取节点成员，可以获取直接添加到该节点中的用户，也可以获取到该节点子节点的用户。
        /// </summary>
        /// <param name="nodeId">节点 ID</param>
        /// <param name="nodeByIdWithMembersParam">查询参数</param>
        /// <returns></returns>
        Task<PaginatedUsers> ListMembers(string nodeId, NodeByIdWithMembersParam nodeByIdWithMembersParam,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="nodeId">节点 ID</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        Task<PaginatedUsers> RemoveMembers(string nodeId, IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 导出所有组织机构数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Authing.ApiClient.Domain.Model.Management.Orgs.Org>> ExportAll(AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 导出某个组织机构数据
        /// </summary>
        /// <param name="nodeId">组织机构ID</param>
        /// <returns></returns>
        Task<Node> ExportByOrgId(string orgId,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 根据部门 ID 获取被授权的所有资源列表
        /// </summary>
        /// <param name="nodeId"> 部门 ID</param>
        /// <param name="nameSpace">权限分组的 分组ID</param>
        /// <param name="resourceType">可选，资源类型，默认会返回所有有权限的资源</param>
        /// <returns></returns>
        Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeId(string nodeId, string nameSpace, ResourceType resourceType=default,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 根据部门 Code 获取被授权的所有资源列表
        /// </summary>
        /// <param name="orgId">组织结构ID</param>
        /// <param name="code">部门 Code</param>
        /// <param name="nameSpace">权限分组的 分组ID</param>
        /// <param name="resourceType">可选，资源类型，默认会返回所有有权限的资源</param>
        /// <returns></returns>
        Task<PaginatedAuthorizedResources> ListAuthorizedResourcesByNodeCode(string orgId,string code, string nameSpace, ResourceType resourceType = default,AuthingErrorBox authingErrorBox=null);

        /// <summary>
        /// 设置部门自定义字段值
        /// </summary>
        /// <param name="nodeid">部门 ID</param>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        Task<CommonResponse<T>> SetPartMentCustomData<T>(string nodeid, string key, object value);
    }
}
