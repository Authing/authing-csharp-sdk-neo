using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Userpool;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        /// <summary>
        /// 用户池管理模块
        /// </summary>
        public UserpoolManagement Userpool { get; set; }

        /// <summary>
        /// 用户池管理类
        /// </summary>

        public class UserpoolManagement : IUserpoolManagement
        {
            private readonly ManagementClient client;

            public UserpoolManagement(ManagementClient client)
            {
                this.client = client;
            }

            /// <summary>
            /// 用户池详情
            /// </summary>
            /// <returns></returns>
            public async Task<UserPool> Detail()
            {
                var res = await client.Get<UserPool>("api/v2/userpools/detail", new GraphQLRequest());
                return res.Data;
            }

            /// <summary>
            /// 更新用户池信息
            /// </summary>
            /// <param name="updates"></param>
            /// <returns></returns>
            public async Task<UserPool> Update(UpdateUserpoolInput updates)
            {
                var param = new UpdateUserpoolParam(updates);

                var res = await client.Request<UpdateUserpoolResponse>(param.CreateRequest());
                return res.Data?.Result;
            }

            /// <summary>
            /// 获取环境变量列表
            /// </summary>
            /// <returns></returns>
            public async Task<IEnumerable<Env>> ListEnv()
            {
                var res = await client.Get<IEnumerable<Env>>("api/v2/env", new GraphQLRequest());
                return res.Data;
            }

            /// <summary>
            /// 添加环境变量
            /// </summary>
            /// <param name="key">环境变量键</param>
            /// <param name="value">环境变量值</param>
            /// <returns></returns>
            public async Task<int> AddEnv(string key, object value)
            {

                var result = await client.Post<Env>("api/v2/env", new Dictionary<string, string>
                {
                    { "key", key },
                  { "value",value.ToString()}
                });

                return result.Code;
            }

            /// <summary>
            /// 删除环境变量
            /// </summary>
            /// <param name="key">环境变量键</param>
            /// <returns></returns>
            public async Task<int> RemoveEnv(string key)
            {
                var result = await client.Delete<Env>($"api/v2/env/{key}", null);

                return result.Code;
            }
        }

    }
}