using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Statistics;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.ApiClient.Interfaces.ManagementClient
{
    public interface IStatisticsManagement
    {
        /// <summary>
        /// 用户操作日志
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        Task<UserLogs> listUserActions(LogsPageParam options, AuthingErrorBox authingErrorBox = null);
        /// <summary>
        /// 审计日志
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<AdminLogs> listAuditLogs(AuditLogPageParam options, AuthingErrorBox authingErrorBox = null);
    }
}