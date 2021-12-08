using System.Collections.Generic;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Userpool;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IUserpoolManagement
    {
        /// <summary>
        /// 用户池详情
        /// </summary>
        /// <returns></returns>
        Task<UserPool> Detail();

        /// <summary>
        /// 更新用户池信息
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        Task<UserPool> Update(UpdateUserpoolInput updates);

        /// <summary>
        /// 获取环境变量列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Env>> ListEnv();

        /// <summary>
        /// 添加环境变量
        /// </summary>
        /// <param name="key">环境变量键</param>
        /// <param name="value">环境变量值</param>
        /// <returns></returns>
        Task<int> AddEnv(
            string key,
            object value
        );

        /// <summary>
        /// 删除环境变量
        /// </summary>
        /// <param name="key">环境变量键</param>
        /// <returns></returns>
        Task<int> RemoveEnv(
            string key);
    }
}