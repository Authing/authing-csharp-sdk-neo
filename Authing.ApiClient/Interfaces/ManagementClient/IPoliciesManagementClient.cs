using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Policies;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IPoliciesManagementClient 
    {

        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<PaginatedPolicies> List(int page = 1, int limit = 10, string nameSpace = null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PaginatedPolicies> List(PoliciesParam param, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 创建策略
        /// </summary>
        /// <param name="code"></param>
        /// <param name="statements"></param>
        /// <param name="description"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<Policy> Create(string code, List<PolicyStatementInput> statements, string description = null, string nameSpace = null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 获取策略详情
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<Policy> Detail(string code, string nameSpace = null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 修改策略
        /// </summary>
        /// <param name="code"></param>
        /// <param name="statements"></param>
        /// <param name="description"></param>
        /// <param name="newCode"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<Policy> Update(string code, List<PolicyStatementInput> statements, string description = null, string newCode = null, string nameSpace = null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        ///修改策略
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Policy> Update(UpdatePolicyParam param, AuthingErrorBox authingErrorBox = null);



        /// <summary>
        /// 删除策略
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<CommonMessage> Delete(string code, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 批量删除策略
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        Task<CommonMessage> DeleteMany(List<string> codeList, AuthingErrorBox authingErrorBox = null);



        Task<PaginatedPolicyAssignments> ListAssignments(PolicyAssignmentsParam param, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 获取策略授权记录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<PaginatedPolicyAssignments> ListAssignments(string code, int page = 1, int limit = 10, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 添加策略授权
        /// </summary>
        /// <param name="policies"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifiers"></param>
        /// <returns></returns>
        Task<CommonMessage> AddAssignments(List<string> policies, PolicyAssignmentTargetType targetType, List<string> targetIdentifiers,string nameSpace=null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 撤销策略授权
        /// </summary>
        /// <param name="policies"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifiers"></param>
        /// <returns></returns>
        Task<CommonMessage> RemoveAssignments(List<string> policies, PolicyAssignmentTargetType targetType, List<string> targetIdentifiers,string nameSpace=null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 设置策略授权状态为关闭
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifier"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<CommonMessage> DisableAssignment(string policy, PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace = null, AuthingErrorBox authingErrorBox = null);


        /// <summary>
        /// 设置策略授权状态为开启
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifier"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        Task<CommonMessage> EnableAssignment(string policy, PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace = null, AuthingErrorBox authingErrorBox = null);
       



    }
}
