using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Interfaces
{
    public interface IWhitelistManagementClient
    {
        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        Task<IEnumerable<WhiteList>> List(WhitelistType type);

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号</param>
        /// <param name="list">白名单列表，请注意邮箱不区分大小写</param>
        /// <returns></returns>
        Task<IEnumerable<WhiteList>> Add(WhitelistType type, IEnumerable<string> list);

        /// <summary>
        /// 移除白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <param name="list">白名单列表，请注意邮箱不区分大小写。</param>
        /// <returns></returns>
        Task<IEnumerable<WhiteList>> Remove(WhitelistType type, IEnumerable<string> list);

        /// <summary>
        /// 开启白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        Task<UpdateUserpoolResponse> Enable(WhitelistType type);

        /// <summary>
        /// 关闭白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        Task<UpdateUserpoolResponse> Disable(WhitelistType type);
    }

}
