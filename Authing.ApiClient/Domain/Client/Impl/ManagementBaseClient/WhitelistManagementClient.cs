using Authing.ApiClient.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Interfaces;
using Authing.ApiClient.Interfaces.ManagementClient;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{

    /// <summary>
    /// 注册白名单管理类
    /// </summary>
    public class WhitelistManagementClient : IWhitelistManagementClient
    {
        private readonly ManagementClient _client;

        /// <summary>
        /// 白名单管理模块构造器
        /// </summary>
        /// <param name="client"></param>
        public WhitelistManagementClient(ManagementClient client)
        {
            this._client = client;
        }

        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        public async Task<IEnumerable<WhiteList>> List(WhitelistType type)
        {
            var param = new WhitelistParam(type);
            var result = await _client.Request<WhitelistResponse>(param.CreateRequest()).ConfigureAwait(false);
            return result.Data?.Result ?? null;
        }

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号</param>
        /// <param name="list">白名单列表，请注意邮箱不区分大小写</param>
        /// <returns></returns>
        public async Task<IEnumerable<WhiteList>> Add(WhitelistType type, IEnumerable<string> list)
        {
            var param = new AddWhitelistParam(type, list);
            var result = await _client.Request<AddWhitelistResponse>(param.CreateRequest()).ConfigureAwait(false);
            return result.Data?.Result ?? null;
        }

        /// <summary>
        /// 移除白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <param name="list">白名单列表，请注意邮箱不区分大小写。</param>
        /// <returns></returns>
        public async Task<IEnumerable<WhiteList>> Remove(WhitelistType type, IEnumerable<string> list)
        {
            var param = new RemoveWhitelistParam(type, list);

            var res = await _client.Request<RemoveWhitelistResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data?.Result ?? null;
        }

        /// <summary>
        /// 开启白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        public async Task<UpdateUserpoolResponse> Enable(WhitelistType type)
        {
            var config = new RegisterWhiteListConfigInput
            {
                UsernameEnabled = (type & WhitelistType.USERNAME) == WhitelistType.USERNAME,
                EmailEnabled = (type & WhitelistType.EMAIL) == WhitelistType.EMAIL,
                PhoneEnabled = (type & WhitelistType.PHONE) == WhitelistType.PHONE,
            };
            var param = new UpdateUserpoolParam(new UpdateUserpoolInput()
            {
                Whitelist = config,
            });
            var res = await _client.Request<UpdateUserpoolResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data ?? null;
        }

        /// <summary>
        /// 关闭白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        public async Task<UpdateUserpoolResponse> Disable(WhitelistType type)
        {
            var config = new RegisterWhiteListConfigInput
            {
                UsernameEnabled = (type & WhitelistType.USERNAME) != WhitelistType.USERNAME,
                EmailEnabled = (type & WhitelistType.EMAIL) != WhitelistType.EMAIL,
                PhoneEnabled = (type & WhitelistType.PHONE) != WhitelistType.PHONE,
            };
            var param = new UpdateUserpoolParam(new UpdateUserpoolInput()
            {
                Whitelist = config,
            });
            var res = await _client.Request<UpdateUserpoolResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data ?? null;
        }
    }
}
