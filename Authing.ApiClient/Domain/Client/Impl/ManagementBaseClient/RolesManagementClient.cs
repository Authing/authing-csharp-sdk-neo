using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management;
using Authing.ApiClient.Domain.Model.Management.Groups;
using Authing.ApiClient.Domain.Model.Management.Roles;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    /// <summary>
    /// 角色管理类
    /// </summary>
    public class RolesManagementClient:IRolesManagementClient
    {
        private readonly ManagementClient client;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client"></param>
        public RolesManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        public async Task<Role> Create(string code,string description = null,string parentCode = null,string nameSpace=null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new CreateRoleParam(code)
            {
                Description = description,
                Parent = parentCode,
                Namespace=nameSpace
            };
            var res = await client.RequestCustomDataWithToken<CreateRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> Delete(string code,AuthingErrorBox authingErrorBox=null)
        {
            var param = new DeleteRoleParam(code);
            var res = await client.RequestCustomDataWithToken<DeleteRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> Delete(string code,
                                                string nameSpace = null,
                                                AuthingErrorBox authingErrorBox = null)
        {
            var param = new DeleteRoleParam(code)
            {
                Namespace = nameSpace
            };
            var res = await client.RequestCustomDataWithToken<DeleteRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> DeleteMany(IEnumerable<string> codeList,AuthingErrorBox authingErrorBox=null)
        {
            var param = new DeleteRolesParam(codeList);
            var res = await client.RequestCustomDataWithToken<DeleteRolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> DeleteMany(IEnumerable<string> codeList,string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new DeleteRolesParam(codeList)
            {
                Namespace = nameSpace
            };
            var res = await client.RequestCustomDataWithToken<DeleteRolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Role> Update(string code,string description = null,string newCode = null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UpdateRoleParam(code)
            {
                Description = description,
                NewCode = newCode,
            };
            var res = await client.RequestCustomDataWithToken<UpdateRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Role> Update(UpdateRoleOptions updateRoleOptions,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UpdateRoleParam(updateRoleOptions.Code)
            {
                Namespace = updateRoleOptions.NameSpace,
                Description = updateRoleOptions.Description,
                NewCode = updateRoleOptions.NewCode,
            };
            var res = await client.RequestCustomDataWithToken<UpdateRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Role> Detail(string code,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RoleParam(code);
            var res = await client.RequestCustomDataWithToken<RoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Role> Detail(string code,string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RoleParam(code)
            {
                Namespace = nameSpace
            };
            var res = await client.RequestCustomDataWithToken<RoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Role> FindByCode(string code,string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            var res = await Detail(code, nameSpace,authingErrorBox).ConfigureAwait(false);
            return res;
        }

        public async Task<PaginatedRoles> List(int page = 1,int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RolesParam() { Page = page, Limit = limit };
            var res = await client.RequestCustomDataWithToken<RolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<PaginatedRoles> List(string nameSpace,int page = 1,int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RolesParam()
            {
                Page = page,
                Limit = limit,
                Namespace = nameSpace
            };
            var res = await client.RequestCustomDataWithToken<RolesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

                 
        public async Task<PaginatedUsers> ListUsers(string code,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RoleWithUsersParam(code);
            var res = await client.RequestCustomDataWithToken<RoleWithUsersResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result.Users;
        }

        public async Task<PaginatedUsers> ListUsers(string code,ListUsersOption listUsersOption,AuthingErrorBox authingErrorBox=null)
        {
            if (!listUsersOption.WithCustomData)
            {
                var _param = new RoleWithUsersParam(code)
                {
                    Code = code,
                    Limit = listUsersOption.Limit,
                    Page = listUsersOption.Page,
                    Namespace = listUsersOption.NameSpace
                };
                var _res = await client.RequestCustomDataWithToken<RoleWithUsersResponse>(_param.CreateRequest()).ConfigureAwait(false);
                ErrorHelper.LoadError(_res, authingErrorBox);
                return _res.Data?.Result.Users;
            }
            else
            {
                var _param = new RoleWithUsersWithCustomDataParam(code)
                {
                    Code = code,
                    Namespace = listUsersOption.NameSpace,
                    Page = listUsersOption.Page,
                    Limit = listUsersOption.Limit
                };
                var _res = await client.RequestCustomDataWithToken<RoleWithUsersWithCustomDataResponse>(_param.CreateRequest()).ConfigureAwait(false);
                ErrorHelper.LoadError(_res, authingErrorBox);
                return _res.Data?.Result.Users;
            }
        }

        public async Task<CommonMessage> AddUsers(string code,IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null)
        {
            var param = new AssignRoleParam()
            {
                UserIds = userIds,
                RoleCode = code
            };
            var res = await client.RequestCustomDataWithToken<AssignRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> AddUsers(string code,IEnumerable<string> userIds,string nameSpace = null,AuthingErrorBox authingErrorBox=null)
        {
            var param = new AssignRoleParam()
            {
                UserIds = userIds,
                RoleCode = code,
                Namespace = nameSpace
            };
            var res = await client.RequestCustomDataWithToken<AssignRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

      
        public async Task<CommonMessage> RemoveUsers(string code,IEnumerable<string> userIds,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RevokeRoleParam()
            {
                UserIds = userIds,
                RoleCode = code
            };
            var res = await client.RequestCustomDataWithToken<RevokeRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> RemoveUsers(string code,
                                                     IEnumerable<string> userIds,
                                                     string nameSpace = null,
                                                     AuthingErrorBox authingErrorBox = null)
        {
            var param = new RevokeRoleParam()
            {
                UserIds = userIds,
                RoleCode = code,
                Namespace = nameSpace,
            };
            var res = await client.RequestCustomDataWithToken<RevokeRoleResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data?.Result;
        }

        public async Task<PaginatedPolicyAssignments> ListPolicies(string code,int page = 1,int limit = 10,AuthingErrorBox authingErrorBox=null)
        {
            var param = new PolicyAssignmentsParam()
            {
                TargetType = PolicyAssignmentTargetType.ROLE,
                TargetIdentifier = code,
                Page = page,
                Limit = limit,
            };
            var res = await client.RequestCustomDataWithToken<PolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> AddPolicies(string code,IEnumerable<string> policies,AuthingErrorBox authingErrorBox=null)
        {
            var param = new AddPolicyAssignmentsParam(policies, PolicyAssignmentTargetType.ROLE)
            {
                TargetIdentifiers = new string[] { code },
            };

            var res = await client.RequestCustomDataWithToken<AddPolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<CommonMessage> RemovePolicies(string code,IEnumerable<string> policies,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RemovePolicyAssignmentsParam(policies, PolicyAssignmentTargetType.ROLE)
            {
                TargetIdentifiers = new string[] { code },
            };
            var res = await client.RequestCustomDataWithToken<RemovePolicyAssignmentsResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<Role> ListAuthorizedResources(string code, string nameSpace, ResourceType resourceType = default,AuthingErrorBox authingErrorBox=null)
        {
            var param = new ListRoleAuthorizedResourcesParam(code)
            {
                ResourceType = resourceType.ToString().ToUpper(),
                Namespace = nameSpace,
            };
            var res = await client.RequestCustomDataWithToken<ListRoleAuthorizedResourcesResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            if (res.Data?.Result == null)
            {
                throw new Exception("角色不存在");
            }
            return res.Data?.Result;
        }

        public async Task<List<KeyValuePair<string, object>>> GetUdfValue(string roleCode,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UdvParam(UdfTargetType.ROLE, roleCode);
            var res = await client.RequestCustomDataWithToken<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return AuthingUtils.ConverUdvToKeyValuePair(res.Data?.Result);
        }

        public async Task<KeyValuePair<string, object>> GetSpecificUdfValue(string roleId, string udfKey,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UdvParam(UdfTargetType.ROLE, roleId);
            var res = await client.RequestCustomDataWithToken<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var udfList = AuthingUtils.ConverUdvToKeyValuePair(res.Data?.Result);
            var keyValuePair = udfList.Where(item => item.Key == udfKey).ToList();
            return keyValuePair[0];
        }

        public async Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(IEnumerable<string> roleIds,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UdfValueBatchParam(UdfTargetType.ROLE, roleIds);
            var res = await client.RequestCustomDataWithToken<UdfValueBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            var dic = new Dictionary<string, List<KeyValuePair<string, object>>>();
            res.Data?.Result.ToList().ForEach(
                item =>
                    dic.Add(item.TargetId, AuthingUtils.ConverUdvToKeyValuePair(item.Data))
            );
            return dic;
        }

        public async Task<IEnumerable<UserDefinedData>> SetUdfValue(SetUdfValueParam setUdfValueParam,AuthingErrorBox authingErrorBox=null)
        {
            if (setUdfValueParam.UdvList?.Count < 1)
            {
                throw new Exception("empty udf value list");
            }
            var _udvList = new List<UserDefinedDataInput>();
            setUdfValueParam.UdvList.ToList().ForEach(udv => _udvList.Add(
            new UserDefinedDataInput(udv.Key)
            {
                Value = udv.Value.ConvertJson()
            }));
            var param = new SetUdvBatchParam(UdfTargetType.ROLE, setUdfValueParam.RoleId)
            {
                UdvList = _udvList
            };
            var res = await client.RequestCustomDataWithToken<SetUdvBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<IEnumerable<UserDefinedData>> SetUdfValueBatch(IEnumerable<SetUdfValueParam> setUdfValueBatchParam,AuthingErrorBox authingErrorBox=null)
        {
            if (setUdfValueBatchParam.ToList().Count < 1)
            {
                throw new Exception("empty input list");
            }
            var param = new List<SetUdfValueBatchInput>();
            setUdfValueBatchParam.ToList().ForEach(
                setUdfValueBatch =>
                    setUdfValueBatch.UdvList.ToList().ForEach(udf =>
                    param.Add(new SetUdfValueBatchInput(setUdfValueBatch.RoleId, udf.Key, udf.Value))
            ));
            var _param = new SetUdfValueBatchParam(UdfTargetType.ROLE, param);
            var res = await client.RequestCustomDataWithToken<SetUdvBatchResponse>(_param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        public async Task<IEnumerable<UserDefinedData>> RemoveUdfValue(string roleId, string key,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RemoveUdvParam(UdfTargetType.ROLE, roleId, key);
            var res = await client.RequestCustomDataWithToken<RemoveUdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

    }
}
