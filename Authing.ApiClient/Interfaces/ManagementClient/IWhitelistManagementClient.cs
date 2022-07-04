using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.WhiteList;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IWhitelistManagementClient
    {
        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        Task<IEnumerable<WhiteList>> List(WhitelistType type, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号</param>
        /// <param name="list">白名单列表，请注意邮箱不区分大小写</param>
        /// <returns></returns>
        Task<IEnumerable<WhiteList>> Add(WhitelistType type, IEnumerable<string> list, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 移除白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <param name="list">白名单列表，请注意邮箱不区分大小写。</param>
        /// <returns></returns>
        Task<IEnumerable<WhiteList>> Remove(WhitelistType type, IEnumerable<string> list, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 开启白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        Task<UpdateUserpoolResponse> Enable(WhitelistType type, AuthingErrorBox authingErrorBox = null);

        /// <summary>
        /// 关闭白名单
        /// </summary>
        /// <param name="type">白名单类型，USERNAME 为用户名、Email 为邮箱、Phone 为手机号。</param>
        /// <returns></returns>
        Task<UpdateUserpoolResponse> Disable(WhitelistType type, AuthingErrorBox authingErrorBox = null);
    }

}
