using Authing.ApiClient.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Interfaces;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        /// <summary>
        /// 注册白名单管理模块
        /// </summary>
        public WhitelistManagementClient Whitelist { get; private set; }

        /// <summary>
        /// 注册白名单管理类
        /// </summary>
        public class WhitelistManagementClient : IWhitelistManagementClient
        {
            private readonly ManagementClient client;

            /// <summary>
            /// 白名单管理模块构造器
            /// </summary>
            /// <param name="client"></param>
            public WhitelistManagementClient(ManagementClient client)
            {
                this.client = client;
            }

            /// <summary>
            /// 获取白名单
            /// </summary>
            /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
            /// <returns></returns>
            public async Task<IEnumerable<WhiteList>> List(WhitelistType type)
            {
                var param = new WhitelistParam(type);
                var result = await client.Request<WhitelistResponse>(param.CreateRequest());
                return result.Data?.Result;
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
                var result = await client.Request<AddWhitelistResponse>(param.CreateRequest());
                return result.Data?.Result;
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

                var res = await client.Request<RemoveWhitelistResponse>(param.CreateRequest());
                return res.Data?.Result;
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

                var res = await client.Request<UpdateUserpoolResponse>(param.CreateRequest());
                return res.Data;

                // TODO: 缺少返回类型
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

                var res = await client.Request<UpdateUserpoolResponse>(param.CreateRequest());
                return res.Data;
            }
        }
    }

}
