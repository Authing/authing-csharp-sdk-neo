using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.UserPool;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{

    /// <summary>
    /// 用户池管理类
    /// </summary>
    public class UserpoolManagement : IUserpoolManagement
    {
        private readonly ManagementClient _client;

        public UserpoolManagement(ManagementClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// 用户池详情
        /// </summary>
        /// <returns></returns>
        public async Task<UserPool> Detail()
        {
            //var res = await _client.Get<UserPool>("api/v2/userpools/detail", new GraphQLRequest());
            var res = await _client.RequestCustomDataWithToken<UserPool>("api/v2/userpools/detail", method: HttpMethod.Get).ConfigureAwait(false);
            return res.Data ?? null;
        }

        /// <summary>
        /// 更新用户池信息
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        public async Task<UserPool> Update(UpdateUserpoolInput updates)
        {
            var param = new UpdateUserpoolParam(updates);

            var res = await _client.Request<UpdateUserpoolResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 获取环境变量列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Env>> ListEnv()
        {
            //var res = await _client.Get<IEnumerable<Env>>("api/v2/env", new GraphQLRequest());
            var res = await _client.RequestCustomDataWithToken<IEnumerable<Env>>("api/v2/env", method: HttpMethod.Get).ConfigureAwait(false);
            return res.Data ?? null;
        }

        /// <summary>
        /// 添加环境变量
        /// </summary>
        /// <param name="key">环境变量键</param>
        /// <param name="value">环境变量值</param>
        /// <returns></returns>
        public async Task<int> AddEnv(string key, object value)
        {

            //var result = await _client.Post<Env>("api/v2/env", new Dictionary<string, string>
            //{
            //    { "key", key },
            //  { "value",value.ToString()}
            //});

            var result = await _client.RequestCustomDataWithToken<Env>("api/v2/env", new Dictionary<string, string>()
                {
                    { "key", key },
                    { "value", value.ToString() }
                }.ConvertJson()).ConfigureAwait(false);

            return result.Code;
        }

        /// <summary>
        /// 删除环境变量
        /// </summary>
        /// <param name="key">环境变量键</param>
        /// <returns></returns>
        public async Task<int> RemoveEnv(string key)
        {
            //var result = await _client.Delete<Env>($"api/v2/env/{key}", null);
            var result = await _client.RequestCustomDataWithToken<Env>($"api/v2/env/{key}", method: HttpMethod.Delete).ConfigureAwait(false);
            return result.Code;
        }
    }

}