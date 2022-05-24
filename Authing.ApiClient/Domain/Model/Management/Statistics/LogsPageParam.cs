using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Statistics
{
    public class LogsPageParam
    {
        /// <summary>
        /// 客户端真实 IP，如果你在服务器端调用此接口，请务必将此参数设置为终端用户的真实 IP。
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 操作名称的集合
        /// </summary>
        public IEnumerable<string> OperationNames { get; set; } = new List<string>();

        /// <summary>
        /// 用户唯一标识的集合
        /// </summary>
        public IEnumerable<string> UserId { get; set; } = new List<string>();

        /// <summary>
        /// 每页条目数量，默认为 10 个
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 每页条目数量，默认为 10 个
        /// </summary>
        public int Limit { get; set; } = 10;
    }
}