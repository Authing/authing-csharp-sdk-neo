using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Client.Impl.AuthenticationClient
{
    public class QrCodeAuthenticationClient : BaseAuthenticationClient
    {
        public QrCodeAuthenticationClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {

        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <returns></returns>
        public async Task<GeneQrCodeResponse> GeneCode(GeneQrCodeParam geneQrCodeParam)
        {
            //var result = await PostRaw<object>("api/v2/qrcode/gene",).ConfigureAwait(false);
            GraphQLResponse<GeneQrCodeResponse> result = await 
                RequestCustomDataWithToken<GeneQrCodeResponse>("api/v2/qrcode/gene", Newtonsoft.Json.JsonConvert.SerializeObject(geneQrCodeParam), method: System.Net.Http.HttpMethod.Post).
                ConfigureAwait(false);
            return result.Data;
        }


        /// <summary>
        /// 检测扫码状态
        /// </summary>
        /// <param name="id">二维码唯一 ID</param>
        /// <returns></returns>
        public async Task<QrCodeCheckStatusResponse> CheckStatus(string id)
        {
            var result = await RequestCustomDataWithToken<QrCodeCheckStatusResponse>($"api/v2/qrcode/check?random={id}", null,method:System.Net.Http.HttpMethod.Get).ConfigureAwait(false);
            return result.Data;
        }

        /// <summary>
        /// 使用 ticket 交换用户信息
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns>完整的用户信息，其中 user.token 为用户的登录凭证。</returns>
        public async Task<UserInfo> ExchangeUserInfo(string ticket)
        {
            var result = await RequestCustomDataWithOutToken<UserInfo>($"api/v2/qrcode/userinfo",JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {  "ticket",ticket}
            }),method:System.Net.Http.HttpMethod.Post).ConfigureAwait(false);

            return result.Data;
        }
    }
}
