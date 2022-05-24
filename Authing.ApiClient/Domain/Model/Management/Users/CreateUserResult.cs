using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    public class CreateUserResult
    {
        #region members
        /// <summary>
        /// 判断用户创建是否成功的标志
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errMsg")]
        public string? ErrMsg { get; set; }

        /// <summary>
        /// 用户对象
        /// </summary>
        [JsonProperty("user")]
        public User? User { get; set; }
        #endregion
    }
}