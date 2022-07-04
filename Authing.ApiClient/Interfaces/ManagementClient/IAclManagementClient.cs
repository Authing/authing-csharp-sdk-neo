using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Acl;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;

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
        Task<NameSpace> CreateNamespace(string code, string name, string description, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="page">页码，默认为 1</param>
        /// <param name="limit">每页个数，默认为 10</param>
        /// <returns></returns>
        Task<Namespaces> ListNamespaces(int page = 1, int limit = 10, AuthingErrorBox authingErrorBox = null);

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
        Task<NameSpace> UpdateNamespace(string nameSpaceId, UpdateNamespaceParam updateNamespaceParam, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="code">权限分组 ID</param>
        /// <returns></returns>
        Task<int> DeleteNamespace(int code, AuthingErrorBox authingErrorBox = null);

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
        Task<ListResourcesRes> ListResources(ResourceQueryFilter resourceQueryFilter, AuthingErrorBox authingErrorBox = null);

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
        Task<Resources> CreateResource(ResourceParam createResourceParam, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 根据资源代码查询资源
        /// </summary>
        /// <param name="code">资源名称</param>
        /// <param name="nameSpace">权限分组唯一标识符</param>
        /// <returns></returns>
        Task<Resources> FindResourceByCode(string code, string nameSpace = "", AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 根据资源 ID 查询资源
        /// </summary>
        /// <param name="id">资源 ID</param>
        /// <returns></returns>
        Task<Resources> GetResourceById(string id, AuthingErrorBox authingErrorBox = null);

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
        Task<Resources> UpdateResource(string code, ResourceParam options, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="code">资源标识符</param>
        /// <param name="namespacecode">权限分组 ID</param>
        /// <returns></returns>
        Task<bool> DeleteResource(string code, string namespacecode, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 允许某个用户对某个资源进行某个操作
        /// </summary>
        /// <param name="userid">用户 ID</param>
        /// <param name="action"> 操作名称，推荐使用 <resourceType>:<actionName> 的格式，如 books:edit，books:list</param>
        /// <param name="resource">资源名称，必须为 <resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*</param>
        /// <param name="nameSpace">权限分组唯一标识符</param>
        /// <returns></returns>
        Task<CommonMessage> Allow(string userid, string nameSpace, string resource, string action, AuthingErrorBox authingErrorBox = null);

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
        Task<CommonMessage> RevokeResource(RevokeResourceParams options, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 判断某个用户是否对某个资源有某个操作权限
        /// </summary>
        /// <param name="userId">用户 ID</param>
        /// <param name="action">操作名称，推荐使用 <resourceType>:<actionName> 的格式，如 books:edit，books:list</param>
        /// <param name="resource">资源名称，必须为 <resourceType>:<resourceId> 格式或者为 _，如 _，books:123，books:*</param>
        /// <param name="namespacecode">权限分组唯一标识符</param>
        /// <returns></returns>
        Task<bool> IsAllowed(string userId, string resource, string action, string namespacecode = "", AuthingErrorBox authingErrorBox = null);

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
        Task<PaginatedAuthorizedResources> ListAuthorizedResources(PolicyAssignmentTargetType targetType,
            string targetIdentifier,
            string namespacecode,
            ListAuthorizedResourcesOptions options, AuthingErrorBox authingErrorBox = null);

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
        Task<PaginatedAuthorizedTargets> GetAuthorizedTargets(GetAuthorizedTargetsOptions getAuthorizedTargetsOptions, AuthingErrorBox authingErrorBox = null);

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
        Task<CommonMessage> AuthorizeResource(
            string namespacecode,
            string resource,
            IEnumerable<AuthorizeResourceOpt> authorizeResourceOptions
        , AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 编程访问账号列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<Pagination<ProgrammaticAccessAccount>> ProgrammaticAccessAccountList(ProgrammaticAccessAccountListProps options, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        ///  添加编程访问账号
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="createProgrammaticAccessAccountParam"></param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<ProgrammaticAccessAccount> CreateProgrammaticAccessAccount(string appId, CreateProgrammaticAccessAccountParam createProgrammaticAccessAccountParam, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 删除编程访问账号
        /// </summary>
        /// <param name="programmaticAccessAccountId"></param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<bool> DeleteProgrammaticAccessAccount(string programmaticAccessAccountId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 启用编程访问账号
        /// </summary>
        /// <param name="programmaticAccessAccountId"></param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<ProgrammaticAccessAccount> EnableProgrammaticAccessAccount(
            string programmaticAccessAccountId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 禁用编程访问账号
        /// </summary>
        /// <param name="programmaticAccessAccountId"></param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<ProgrammaticAccessAccount> DisableProgrammaticAccessAccount(
            string programmaticAccessAccountId, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 刷新编程访问账号密钥
        /// </summary>
        /// <param name="options">编程参数</param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<ProgrammaticAccessAccount> RefreshProgrammaticAccessAccountSecret(ProgrammaticAccessAccountProps options, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        ///  获取应用访问控制策略列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="authingErrorBox"></param>
        /// <returns></returns>
        Task<Pagination<ApplicationAccessPolicies>> GetApplicationAccessPolicies(AppAccessPolicyQueryFilter options, AuthingErrorBox authingErrorBox = null);
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
        Task<bool> EnableApplicationAccessPolicy(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null);
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
        Task<bool> DisableApplicationAccessPolicy(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null);
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
        Task<bool> DeleteApplicationAccessPolicy(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null);
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
        Task<bool> AllowAccessApplication(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null);
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
        Task<bool> DenyAccessApplication(AppAccessPolicy options, AuthingErrorBox authingErrorBox = null);
        /// <summary>
        /// 更改默认应用访问策略（默认拒绝所有用户访问应用、默认允许所有用户访问应用）
        /// </summary>
        /// <param name="options"> 策略参数
        /// AppId 应用 ID
        /// defaultStrategy 默认策略 取值范围 ALLOW_ALL,DENY_ALL
        /// </param>
        /// <returns></returns>
        Task<Application> UpdateDefaultApplicationAccessPolicy(DefaultAppAccessPolicy options, AuthingErrorBox authingErrorBox = null);
    }
}