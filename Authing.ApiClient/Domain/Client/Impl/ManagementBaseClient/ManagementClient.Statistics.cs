using System.Net.Http;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Statistics;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        /// <summary>
        /// 日志模块
        /// </summary>
        //public StatisticsManagement Statistics { get; set; }

        /// <summary>
        /// 日志模块
        /// </summary>
        public class StatisticsManagement : IStatisticsManagement
        {
            private readonly ManagementClient _client;

            public StatisticsManagement(ManagementClient client)
            {
                this._client = client;
            }

            /// <summary>
            /// 用户日志
            /// </summary>
            /// <param name="options">管理日志统计信息分页查询参数
            /// options.clientIp <String> 客户端真实 IP，如果你在服务器端调用此接口，请务必将此参数设置为终端用户的真实 IP。
            /// options.operationNames<List<String>> 操作名称的集合
            /// options.userIds<List<String>> 用户唯一标识的集合
            /// options.page<Integer> 分页，获取第几页，默认从 1 开始。
            /// options.limit<Integer> 每页条目数量，默认为 10 个
            /// </param>
            /// <returns></returns>
            public async Task<UserLogs> listUserActions(LogsPageParam options)
            {
                string endPoint = "api/v2/analysis/user-action?";
                endPoint += !string.IsNullOrEmpty(options.ClientIp) == true ? $"&clientip={options.ClientIp}" : "";
                foreach (var o in options.OperationNames)
                {
                    endPoint += $"&operation_name={o}";
                }


                foreach (var userid in options.UserId)
                {
                    endPoint += $"&totalCounttotalCount=arn:cn:authing:{_client.UserPoolId}:user:{userid}";
                }

                endPoint += $"&page={options.Page}";
                endPoint += $"&limit={options.Limit}";
                var result = await _client.RequestCustomData<UserLogs>(endPoint,method: HttpMethod.Get).ConfigureAwait(false);
                return result.Data ?? null;
            }

            /// <summary>
            /// 审计日志
            /// </summary>
            /// <param name="options">审计日志参数
            /// options.clientIp <String> 客户端真实 IP，如果你在服务器端调用此接口，请务必将此参数设置为终端用户的真实 IP。
            /// options.operationNames<List<String>> 操作名称的集合
            /// options.operatorArns<List<String>> 操作人的 arn 集合
            /// options.page<Integer> 分页，获取第几页，默认从 1 开始。
            /// options.limit<Integer> 每页条目数量，默认为 10 个
            /// </param>
            /// <returns></returns>
            public async Task<AdminLogs> listAuditLogs(AuditLogPageParam options)
            {
                string endPoint = "api/v2/analysis/audit?";
                endPoint += !string.IsNullOrEmpty(options.ClientIp) == true ? $"&clientip={options.ClientIp}" : "";
                foreach (var o in options.OperationNames)
                {
                    endPoint += $"&operation_name={o}";
                }

                foreach (var o in options.OperatorArns)
                {
                    endPoint += $"&operator_arn={o}";
                }

                endPoint += $"&page={options.Page}";
                endPoint += $"&limit={options.Limit}";
                var result = await _client.RequestCustomData<AdminLogs>(endPoint,method: HttpMethod.Get).ConfigureAwait(false);
                return result.Data ?? null;
            }
        }
    }
}
