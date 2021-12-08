using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Statistics;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{
    public partial class ManagementClient
    {
        /// <summary>
        /// 日志模块
        /// </summary>
        public StatisticsManagement Statistics { get; set; }

        /// <summary>
        /// 日志模块
        /// </summary>
        public class StatisticsManagement : IStatisticsManagement
        {
            private readonly ManagementClient client;

            public StatisticsManagement(ManagementClient client)
            {
                this.client = client;
            }

            /// <summary>
            /// 用户日志
            /// </summary>
            /// <param name="options">用户日志查询参数</param>
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
                    endPoint += $"&totalCounttotalCount=arn:cn:authing:{client.UserPoolId}:user:{userid}";
                }

                endPoint += $"&page={options.Page}";
                endPoint += $"&limit={options.Limit}";
                var result = await client.Get<UserLogs>(endPoint, new GraphQLRequest());
                return result.Data;
            }

            /// <summary>
            /// 审计日志
            /// </summary>
            /// <param name="options">审计日志参数</param>
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
                var result = await client.Get<AdminLogs>(endPoint, new GraphQLRequest());
                return result.Data;
            }
        }
    }
}
