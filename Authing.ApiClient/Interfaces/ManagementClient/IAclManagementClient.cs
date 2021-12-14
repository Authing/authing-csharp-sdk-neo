using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Acl;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IAclManagementClient
    {
        /// <summary>
        /// 创建权限分组
        /// </summary>
        /// <param name="code">权限分组唯一标识符</param>
        /// <param name="name">权限分组名</param>
        /// <param name="description">可选，权限分组描述</param>
        /// <returns></returns>
        Task<NameSpace> CreateNamespace(string code, string name, string description);

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="page">页码，默认为 1</param>
        /// <param name="limit">每页个数，默认为 10</param>
        /// <returns></returns>
        Task<Namespaces> ListNamespaces(int page = 1, int limit = 10);

        /// <summary>
        /// 更新权限分组
        /// </summary>
        /// <param name="code">权限分组 Code</param>
        /// <param name="updateNamespaceParam">需要更新的数据
        /// updates.code <String> 可选，权限分组唯一标识符。
        /// updates.name<String> 可选，权限分组名称。
        /// updates.description<String> 可选，权限分组描述。
        /// </param>
        /// <returns></returns>
        Task<NameSpace> UpdateNamespace(string nameSpaceId, UpdateNamespaceParam updateNamespaceParam);

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="code">权限分组 ID</param>
        /// <returns></returns>
        Task<int> DeleteNamespace(int code);

        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="resourceQueryFilter">过滤参数类</param>
        /// <returns></returns>
        Task<ListResourcesRes> ListResources(ResourceQueryFilter resourceQueryFilter);

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="createResourceParam">创建资源参数类</param>
        /// <returns></returns>
        Task<Resources> CreateResource(ResourceParam createResourceParam);

        /// <summary>
        /// 根据资源代码查询资源
        /// </summary>
        /// <param name="code">资源标识符</param>
        /// <param name="nameSpace">权限分组命名空间</param>
        /// <returns></returns>
        Task<Resources> FindResourceByCode(string code, string nameSpace = "");

        /// <summary>
        /// 根据资源 ID 查询资源
        /// </summary>
        /// <param name="id">资源 ID</param>
        /// <returns></returns>
        Task<Resources> GetResourceById(string id);

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="code">资源标识符</param>
        /// <param name="options">资源信息对象
        ///  options.Namespace <String> 资源所在的权限分组标识
        ///  options.Type 资源类型，可选值为 ResourceType.DATA、ResourceType.API、ResourceType.MENU、ResourceType.UI、ResourceType.BUTTON。
        ///  options.Actions<List<ResourceAction>> 资源操作对象数组。其中 name 为操作名称，填写一个动词，description 为操作描述，填写描述信息。
        ///  options.description<String> 资源描述信息
        ///  ResourceAction : Name<String> 操作名称
        ///  ResourceAction : Description<String> 描述信息
        /// </param>
        /// <returns></returns>
        Task<Resources> UpdateResource(string code, ResourceParam options);

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="code">资源标识符</param>
        /// <param name="namespacecode">资源所在的权限分组标识</param>
        /// <returns></returns>
        Task<bool> DeleteResource(string code, string namespacecode);

        /// <summary>
        /// 允许某个用户对某个资源进行某个操作
        /// </summary>
        /// <param name="userid">用户 ID</param>
        /// <param name="action"> 操作名称，推荐使用 <resourceType>:<actionName> 的格式，如 books:edit，books:list</param>
        /// <param name="resource">资源名称，必须为 <resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*</param>
        /// <param name="nameSpace">权限分组命名空间</param>
        /// <returns></returns>
        Task<CommonMessage> Allow(string userid, string nameSpace, string resource, string action);

        /// <summary>
        /// 批量撤回资源
        /// </summary>
        /// <param name="options">
        /// options.namespace <String> 权限分组的 Code，详情请见使用权限分组管理权限资源。
        /// options.resource<String> 资源名称，必须为<resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*。
        /// options.opts<List<RevokeResourceOpt>>
        /// options.opts.targetType. <PolicyAssignmentTargetType> 被授权主体类型
        /// options.opts.targetIdentifier. <String> 被授权主体唯一标识
        /// </param>
        /// <returns></returns>
        Task<CommonMessage> RevokeResource(RevokeResourceParams options);

        /// <summary>
        /// 判断某个用户是否对某个资源有某个操作权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="action">操作名称，推荐使用 <resourceType>:<actionName> 的格式，如 books:edit，books:list</param>
        /// <param name="resource">资源名称，必须为 <resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*</param>
        /// <param name="namespacecode">资源组CODE</param>
        /// <returns></returns>
        Task<bool> IsAllowed(string userId, string resource, string action, string namespacecode = "");

        /// <summary>
        /// 获取用户被授权的所有资源列表
        /// </summary>
        /// <param name="targetType">被授权主体类型</param>
        /// <param name="targetIdentifier">被授权主体唯一标识</param>
        /// <param name="namespacecode">权限分组的 Code</param>
        /// <param name="options">
        /// options.resourceType <String> 可选，资源类型，默认会返回所有有权限的资源，现有资源类型如下：
        /// DATA：数据类型；
        /// API：API 类型数据；
        /// MENU：菜单类型数据；
        /// BUTTON：按钮类型数据
        /// </param>
        /// <returns></returns>
        Task<PaginatedAuthorizedResources> ListAuthorizedResources(PolicyAssignmentTargetType targetType,
            string targetIdentifier,
            string namespacecode,
            ListAuthorizedResourcesOptions options);

        /// <summary>
        /// 获取具备某些资源操作权限的主体
        /// </summary>
        /// <param name="getAuthorizedTargetsOptions">
        /// options.namespace <String> 权限分组的 Code，详情请见使用权限分组管理权限资源。
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
        Task<PaginatedAuthorizedTargets> GetAuthorizedTargets(GetAuthorizedTargetsOptions getAuthorizedTargetsOptions);

        Task<CommonMessage> AuthorizeResource(
            string namespacecode,
            string resource,
            IEnumerable<AuthorizeResourceOpt> authorizeResourceOptions
        );

        Task<Pagination<ProgrammaticAccessAccount>> ProgrammaticAccessAccountList(ProgrammaticAccessAccountListProps options);
        Task<ProgrammaticAccessAccount> CreateProgrammaticAccessAccount(string appId, CreateProgrammaticAccessAccountParam createProgrammaticAccessAccountParam);
        Task<bool> DeleteProgrammaticAccessAccount(string programmaticAccessAccountId);

        Task<ProgrammaticAccessAccount> EnableProgrammaticAccessAccount(
            string programmaticAccessAccountId);

        Task<ProgrammaticAccessAccount> DisableProgrammaticAccessAccount(
            string programmaticAccessAccountId);
    }
}