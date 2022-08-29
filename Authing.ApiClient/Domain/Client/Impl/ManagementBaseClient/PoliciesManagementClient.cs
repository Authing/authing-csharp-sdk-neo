using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Policies;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Model.Exceptions;
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

        public async Task<PaginatedPolicies> List(int page = 1, int limit = 10, string nameSpace = null, AuthingErrorBox authingErrorBox = null)
        {
            PoliciesParam param = new PoliciesParam()
            {
                Page = page,
                Limit = limit,
                Namespace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<PoliciesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result; ;
        }

        public async Task<PaginatedPolicies> List(PoliciesParam param, AuthingErrorBox authingErrorBox = null)
        {
            if (param == null)
            {
                throw new ArgumentNullException("参数不能为空");
            }
            if (param.Page <= 0)
            {
                param.Page = 1;
            }
            if (param.Limit <= 0)
            {
                param.Limit = 10;
            }

            var result = await client.RequestCustomDataWithToken<PoliciesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<Policy> Create(string code, List<PolicyStatementInput> statements, string description = null, string nameSpace = null, AuthingErrorBox authingErrorBox=null)
        {
            CreatePolicyParam param = new CreatePolicyParam(code, statements)
            {
                Description = description,
                Namespace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<CreatePolicyResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<Policy> Detail(string code, string nameSpace = null, AuthingErrorBox authingErrorBox = null)
        {
            PolicyParam param = new PolicyParam(code)
            {
                Namespace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<PolicyResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<Policy> Update(string code, List<PolicyStatementInput> statements, string description = null, string newCode = null, string nameSpace = null, AuthingErrorBox authingErrorBox = null)
        {
            UpdatePolicyParam param = new UpdatePolicyParam(code)
            {
                Statements = statements,
                Description = description,
                NewCode = newCode,
                Namespace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<UpdatePolicyResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<Policy> Update(UpdatePolicyParam param,AuthingErrorBox authingErrorBox=null)
        {
            var result = await client.RequestCustomDataWithToken<UpdatePolicyResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<CommonMessage> Delete(string code,AuthingErrorBox authingErrorBox=null)
        {
            DeletePolicyParam param = new DeletePolicyParam(code);

            var result = await client.RequestCustomDataWithToken<DeletePolicyResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<CommonMessage> DeleteMany(List<string> codeList,AuthingErrorBox authingErrorBox=null)
        {
            DeletePoliciesParam param = new DeletePoliciesParam(codeList);

            var result = await client.RequestCustomDataWithToken<DeletePoliciesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<PaginatedPolicyAssignments> ListAssignments(PolicyAssignmentsParam param,AuthingErrorBox authingErrorBox=null)
        {
            var result = await client.RequestCustomDataWithToken<PolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<PaginatedPolicyAssignments> ListAssignments(string code, int page = 1, int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            PolicyAssignmentsParam param = new PolicyAssignmentsParam()
            {
                Code = code,
                Page = page,
                Limit = limit
            };

            var result = await client.RequestCustomDataWithToken<PolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<CommonMessage> AddAssignments(List<string> policies, PolicyAssignmentTargetType targetType, List<string> targetIdentifiers, string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            AddPolicyAssignmentsParam param = new AddPolicyAssignmentsParam(policies, targetType)
            {
                TargetIdentifiers = targetIdentifiers,
                Namespace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<AddPolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<CommonMessage> RemoveAssignments(List<string> policies, PolicyAssignmentTargetType targetType, List<string> targetIdentifiers, string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            RemovePolicyAssignmentsParam param = new RemovePolicyAssignmentsParam(policies, targetType)
            {
                TargetIdentifiers = targetIdentifiers,
                Namespace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<RemovePolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<CommonMessage> DisableAssignment(string policy, PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            DisableAssignmentParam param = new DisableAssignmentParam(policy, targetType)
            {
                TargetIdentifier = targetIdentifier,
                NameSpace = nameSpace
            };

            var result = await client.RequestCustomDataWithToken<DisableAssignmentResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }

        public async Task<CommonMessage> EnableAssignment(string policy, PolicyAssignmentTargetType targetType, string targetIdentifier, string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            EnableAssignmentParam param = new EnableAssignmentParam(policy, targetType, targetIdentifier,nameSpace);

            var result = await client.RequestCustomDataWithToken<EnableAssignmentResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(result, authingErrorBox);
            return result.Data?.Result;
        }
    }
}
