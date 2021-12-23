using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Policies;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public class PoliciesManagementClient : IPoliciesManagementClient
    {
        private ManagementClient client;

        public PoliciesManagementClient(ManagementClient client)
        {
            this.client = client;
        }
        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<PaginatedPolicies> List(int page = 1, int limit = 10, string nameSpace = null)
        {
            PoliciesParam param = new PoliciesParam()
            {
                Page = page,
                Limit = limit,
                Namespace = nameSpace
            };

            var result = await client.Request<PoliciesResponse>(param.CreateRequest()).ConfigureAwait(false);

            return result.Data.Result; ;
        }

        public async Task<PaginatedPolicies> List(PoliciesParam param)
        {
            return null;
            //var result=await client.Request<Policy>
        }

        /// <summary>
        /// 创建策略
        /// </summary>
        /// <param name="code"></param>
        /// <param name="statements"></param>
        /// <param name="description"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<Policy> Create(string code, List<PolicyStatementInput> statements, string description = null, string nameSpace = null)
        {
            CreatePolicyParam param = new CreatePolicyParam(code, statements)
            {
                Description = description,
                Namespace = nameSpace
            };

            var result = await client.Request<CreatePolicyResponse>(param.CreateRequest()).ConfigureAwait(false);
            return result.Data.Result;
        }

        /// <summary>
        /// 获取策略详情
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<Policy> Detail(string code, string nameSpace = null)
        {
            PolicyParam param = new PolicyParam(code) 
            {
                Namespace=nameSpace
            };

            var result = await client.Request<PolicyResponse>(param.CreateRequest()).ConfigureAwait(false);

            return result.Data.Result;
        }

        /// <summary>
        /// 修改策略
        /// </summary>
        /// <param name="code"></param>
        /// <param name="statements"></param>
        /// <param name="description"></param>
        /// <param name="newCode"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<Policy> Update(string code, List<PolicyStatementInput> statements, string description = null, string newCode = null, string nameSpace = null)
        {
            UpdatePolicyParam param = new UpdatePolicyParam(code) 
            {
                Statements=statements,
                Description=description,
                NewCode=newCode,
                Namespace=nameSpace
            };

            var result = await client.Request<UpdatePolicyResponse>(param.CreateRequest());

            return result.Data.Result;
        }

        /// <summary>
        /// 删除策略
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<CommonMessage> Delete(string code)
        {
            DeletePolicyParam param = new DeletePolicyParam(code);

            var result = await client.Request<DeletePolicyResponse>(param.CreateRequest());
            return result.Data.Result;
        }

        /// <summary>
        /// 批量删除策略
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        public async Task<CommonMessage> DeleteMany(List<string> codeList)
        {
            DeletePoliciesParam param = new DeletePoliciesParam(codeList);

            var result = await client.Request<DeletePoliciesResponse>(param.CreateRequest());
            return result.Data.Result;
        }

        /// <summary>
        /// 获取策略授权记录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<PaginatedPolicyAssignments> ListAssignments(string code, int page = 1, int limit = 10)
        {
            PolicyAssignmentsParam param = new PolicyAssignmentsParam() 
            { 
                Code = code, 
                Page = page,
                Limit=limit 
            };

            var result = await client.Request<PolicyAssignmentsResponse>(param.CreateRequest());
            return result.Data.Result;
        }

        /// <summary>
        /// 添加策略授权
        /// </summary>
        /// <param name="policies"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifiers"></param>
        /// <returns></returns>
        public async Task<CommonMessage> AddAssignments(List<string> policies, PolicyAssignmentTargetType targetType, List<string> targetIdentifiers)
        {
            AddPolicyAssignmentsParam param = new AddPolicyAssignmentsParam(policies, targetType) 
            {
                TargetIdentifiers=targetIdentifiers
            };

            var result = await client.Request<AddPolicyAssignmentsResponse>(param.CreateRequest());
            return result.Data.Result;
        }

        /// <summary>
        /// 撤销策略授权
        /// </summary>
        /// <param name="policies"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifiers"></param>
        /// <returns></returns>
        public async Task<CommonMessage> RemoveAssignments(List<string> policies, PolicyAssignmentTargetType targetType, List<string> targetIdentifiers)
        {
            RemovePolicyAssignmentsParam param = new RemovePolicyAssignmentsParam(policies, targetType)
            { 
                TargetIdentifiers=targetIdentifiers
            };

            var result = await client.Request<RemovePolicyAssignmentsResponse>(param.CreateRequest());

            return result.Data.Result;
        }

        /// <summary>
        /// 设置策略授权状态为关闭
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifier"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<CommonMessage> DisableAssignment(string policy, PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace = null)
        {
            DisableAssignmentParam param = new DisableAssignmentParam(policy, targetType)
            {
                TargetIdentifier=targetIdentifier,
                NameSpace=nameSpace
            };

            var result = await client.Request<DisableAssignmentResponse>(param.CreateRequest());
            return result.Data.Result;
        }

        /// <summary>
        /// 设置策略授权状态为开启
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="targetType"></param>
        /// <param name="targetIdentifier"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<CommonMessage> EnableAssignment(string policy, PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace = null)
        {
            EnableAssignmentParam param = new EnableAssignmentParam(policy, targetType, targetIdentifier);

            var result = await client.Request<EnableAssignmentResponse>(param.CreateRequest());

            return result.Data.Result;
        }



    }
}
