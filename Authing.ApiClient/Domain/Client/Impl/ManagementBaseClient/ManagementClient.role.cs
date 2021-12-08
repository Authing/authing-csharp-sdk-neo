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
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    /// <summary>
    /// 角色管理类
    /// </summary>
    public class RolesManagementClient
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

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="description">角色描述</param>
        /// <param name="parentCode">父角色唯一标志</param>
        /// <returns></returns>
        public async Task<Role> Create(
            string code,
            string description = null,
            string parentCode = null)
        {
            var param = new CreateRoleParam(code)
            {
                Description = description,
                Parent = parentCode,
            };
            var res = await client.Post<CreateRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO: 在下一个大版本中去除
        public async Task<CommonMessage> Delete(
            string code)
        {
            var param = new DeleteRoleParam(code);
            var res = await client.Post<DeleteRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> Delete(
            string code,
            string nameSpace = null)
        {
            var param = new DeleteRoleParam(code)
            {
                Namespace = nameSpace
            };
            var res = await client.Post<DeleteRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="codeList">角色 code 列表</param>
        /// <returns></returns>
        /// TODO： 在下一个大版本中去除
        public async Task<CommonMessage> DeleteMany(
            IEnumerable<string> codeList)
        {
            var param = new DeleteRolesParam(codeList);
            var res = await client.Post<DeleteRolesResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> DeleteMany(
            IEnumerable<string> codeList,
            string nameSpace = null)
        {
            var param = new DeleteRolesParam(codeList)
            {
                Namespace = nameSpace
            };
            var res = await client.Post<DeleteRolesResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 修改角色资料
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="description">角色描述</param>
        /// <param name="newCode">新的 code</param>
        /// <returns></returns>
        /// TODO: 下一个大版本中去除
        public async Task<Role> Update(
            string code,
            string description = null,
            string newCode = null)
        {
            var param = new UpdateRoleParam(code)
            {
                Description = description,
                NewCode = newCode,
            };
            var res = await client.Post<UpdateRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Role> Update(
            string code,
            UpdateRoleOptions updateRoleOptions)
        {
            var param = new UpdateRoleParam(code)
            {
                Namespace = updateRoleOptions.NameSpace,
                Description = updateRoleOptions.Description,
                NewCode = updateRoleOptions.NewCode,
            };
            var res = await client.Post<UpdateRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }


        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        public async Task<Role> Detail(
            string code)
        {
            var param = new RoleParam(code);
            var res = await client.Post<RoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Role> Detail(
            string code,
            string nameSpace = null)
        {
            var param = new RoleParam(code)
            {
                Namespace = nameSpace
            };
            var res = await client.Post<RoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Role> FindByCode(
            string code,
            string nameSpace = null)
        {
            var res = await Detail(code, nameSpace);
            return res;
        }

        /// <summary>
        /// 获取用户池角色列表
        /// </summary>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        public async Task<PaginatedRoles> List(
            int page = 1,
            int limit = 10)
        {
            var param = new RolesParam() { Page = page, Limit = limit };
            var res = await client.Post<RolesResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<PaginatedRoles> List(
            string nameSpace,
            int page = 1,
            int limit = 10)
        {
            var param = new RolesParam()
            {
                Page = page,
                Limit = limit,
                Namespace = nameSpace
            };
            var res = await client.Post<RolesResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除            
        public async Task<PaginatedUsers> ListUsers(
            string code,
            CancellationToken cancellationToken = default)
        {
            var param = new RoleWithUsersParam(code);
            var res = await client.Post<RoleWithUsersResponse>(param.CreateRequest());
            return res.Data.Result.Users;
        }

        public async Task<PaginatedUsers> ListUsers(
            string code,
            ListUsersOption listUsersOption)
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
                var _res = await client.Post<RoleWithUsersResponse>(_param.CreateRequest());
                return _res.Data.Result.Users;
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
                var _res = await client.Post<RoleWithUsersWithCustomDataResponse>(_param.CreateRequest());
                return _res.Data.Result.Users;
            }
        }

        /// <summary>
        /// 批量添加用户到角色
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        public async Task<CommonMessage> AddUsers(
            string code,
            IEnumerable<string> userIds)
        {
            var param = new AssignRoleParam()
            {
                UserIds = userIds,
                RoleCode = code
            };
            var res = await client.Post<AssignRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> AddUsers(
            string code,
            IEnumerable<string> userIds,
            string nameSpace = null)
        {
            var param = new AssignRoleParam()
            {
                UserIds = userIds,
                RoleCode = code,
                Namespace = nameSpace
            };
            var res = await client.Post<AssignRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 批量移除角色上的用户
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="userIds">用户 ID 列表</param>
        /// <returns></returns>
        /// TODO：下一个大版本去除
        public async Task<CommonMessage> RemoveUsers(
            string code,
            IEnumerable<string> userIds)
        {
            var param = new RevokeRoleParam()
            {
                UserIds = userIds,
                RoleCode = code
            };
            var res = await client.Post<RevokeRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<CommonMessage> RemoveUsers(
            string code,
            IEnumerable<string> userIds,
            string nameSpace = null
        )
        {
            var param = new RevokeRoleParam()
            {
                UserIds = userIds,
                RoleCode = code,
                Namespace = nameSpace,
            };
            var res = await client.Post<RevokeRoleResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="page">分页页数，默认为 1</param>
        /// <param name="limit">分页大小，默认为 10</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedPolicyAssignments> ListPolicies(
            string code,
            int page = 1,
            int limit = 10
        )
        {
            var param = new PolicyAssignmentsParam()
            {
                TargetType = PolicyAssignmentTargetType.ROLE,
                TargetIdentifier = code,
                Page = page,
                Limit = limit,
            };
            var res = await client.Post<PolicyAssignmentsResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 批量添加策略
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="policies">策略 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonMessage> AddPolicies(
            string code,
            IEnumerable<string> policies)
        {
            var param = new AddPolicyAssignmentsParam(policies, PolicyAssignmentTargetType.ROLE)
            {
                TargetIdentifiers = new string[] { code },
            };

            var res = await client.Post<AddPolicyAssignmentsResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        /// <summary>
        /// 批量移除策略
        /// </summary>
        /// <param name="code">角色唯一标志</param>
        /// <param name="policies">策略 ID 列表</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonMessage> RemovePolicies(
            string code,
            IEnumerable<string> policies)
        {
            var param = new RemovePolicyAssignmentsParam(policies, PolicyAssignmentTargetType.ROLE)
            {
                TargetIdentifiers = new string[] { code },
            };
            var res = await client.Post<RemovePolicyAssignmentsResponse>(param.CreateRequest());
            return res.Data.Result;
        }

        public async Task<Role> ListAuthorizedResources(string code, string nameSpace, ResourceType resourceType = default)
        {
            var param = new ListRoleAuthorizedResourcesParam(code)
            {
                ResourceType = resourceType.ToString().ToUpper(),
                Namespace = nameSpace,
            };
            var res = await client.Post<ListRoleAuthorizedResourcesResponse>(param.CreateRequest());
            if (res.Data.Result == null)
            {
                throw new Exception("角色不存在");
            }
            return res.Data.Result;
        }

        public async Task<List<KeyValuePair<string, object>>> GetUdfValue(string roleId)
        {
            var param = new UdvParam(UdfTargetType.ROLE, roleId);
            var res = await client.Post<UdvResponse>(param.CreateRequest());
            return AuthingUtils.ConverUdvToKeyValuePair(res.Data.Result);
        }

        public async Task<KeyValuePair<string, object>> GetSpecificUdfValue(string roleId, string udfKey)
        {
            var param = new UdvParam(UdfTargetType.ROLE, roleId);
            var res = await client.Post<UdvResponse>(param.CreateRequest());
            var udfList = AuthingUtils.ConverUdvToKeyValuePair(res.Data.Result);
            var keyValuePair = udfList.Where(item => item.Key == udfKey).ToList();
            return keyValuePair[0];
        }

        public async Task<Dictionary<string, List<KeyValuePair<string, object>>>> GetUdfValueBatch(IEnumerable<string> roleIds)
        {
            var param = new UdfValueBatchParam(UdfTargetType.ROLE, roleIds);
            var res = await client.Post<UdfValueBatchResponse>(param.CreateRequest());
            var dic = new Dictionary<string, List<KeyValuePair<string, object>>>();
            res.Data.Result.ToList().ForEach(
                item =>
                    dic.Add(item.TargetId, AuthingUtils.ConverUdvToKeyValuePair(item.Data))
            );
            return dic;
        }

        public async void SetUdfValue(SetUdfValueParam setUdfValueParam)
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
            var res = await client.Post<SetUdvBatchResponse>(param.CreateRequest());

            //TODO: 缺少返回值
        }

        public async void SetUdfValueBatch(IEnumerable<SetUdfValueParam> setUdfValueBatchParam)
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
            var res = await client.Post<SetUdvBatchResponse>(_param.CreateRequest());
            //TODO: 缺少返回值
        }

        public async void RemoveUdfValue(string roleId, string key)
        {
            var param = new RemoveUdvParam(UdfTargetType.ROLE, roleId, key);
            var res = await client.Post<RemoveUdvResponse>(param.CreateRequest());
            // TODO: 缺少返回值
        }

    }
}
