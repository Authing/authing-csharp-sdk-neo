using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Types;
using Flurl;
using Flurl.Http;

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

        public class UserpoolManagement
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
                // var param = new UserpoolParam();

                // await client.GetAccessToken();
                // var res = await client.Request<UserpoolResponse>(param.CreateRequest(), cancellationToken);
                // return res.Result;
                var res = await client.Host.AppendPathSegment("/api/v2/userpools/detail").WithOAuthBearerToken(client.AccessToken).GetJsonAsync<UserPool>();
                return res;
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
                return res.Data.Result;
            }

            /// <summary>
            /// 获取环境变量列表
            /// </summary>
            /// <returns></returns>
            public async Task<IEnumerable<Env>> ListEnv()
            {
                var res = await client.Host.AppendPathSegment("/api/v2/env").WithOAuthBearerToken(client.AccessToken).GetJsonAsync<RestfulResponse<IEnumerable<Env>>>();
                return res.Data;
            }

            /// <summary>
            /// 添加环境变量
            /// </summary>
            /// <param name="key">环境变量键</param>
            /// <param name="value">环境变量值</param>
            /// <returns></returns>
            public async Task<Env> AddEnv(
                string key,
                object value
                )
            {
                var res = await client.Host.AppendPathSegment("/api/v2/env").WithOAuthBearerToken(client.AccessToken).PostJsonAsync(new
                {
                    key,
                    value
                }).ReceiveJson<RestfulResponse<Env>>();
                return res.Data;
            }

            /// <summary>
            /// 删除环境变量
            /// </summary>
            /// <param name="key">环境变量键</param>
            /// <returns></returns>
            public async Task<Dictionary<string, object>> RemoveEnv(
                string key)
            {
                var res = await client.Host.AppendPathSegment($"api/v2/env/{key}").WithOAuthBearerToken(client.AccessToken).DeleteAsync().ReceiveJson<Dictionary<string, object>>();
                return res;
            }


        }

    }
    public static class FlurlAuthingExt
    {
        public static IFlurlRequest WithAuthingHeader(
            this Url url, ManagementClient client)
        {
            return (IFlurlRequest)new FlurlRequest(url)
                .WithHeader("Authorization", client.AccessToken)
                .WithHeader("x-authing-userpool-id", client.UserPoolId)
                .WithHeader("x-authing-request-from", client.type)
                .WithHeader("x-authing-sdk-version", client.version);
        }
    }
}