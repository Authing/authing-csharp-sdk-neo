using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Client.Impl.AuthenticationClient
{
   public class QrCodeAuthenticationClient:BaseAuthenticationClient
    {
        public QrCodeAuthenticationClient(Action<InitAuthenticationClientOptions> init):base(init)
        {

        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <returns></returns>
        public async Task<object> GeneCode(GeneQrCodeParam geneQrCodeParam)
        {
            var result = await PostRaw<object>("api/v2/qrcode/gene", Newtonsoft.Json.JsonConvert.SerializeObject(geneQrCodeParam)).ConfigureAwait(false);

            return null;
        }

        public async Task<object> CheckStatus(string id)
        {
            var result = await Get<object>($"api/v2/qrcode/check?random={id}", null).ConfigureAwait(false);
            return null;
        }

        public async Task<object> ExchangeUserInfo(string ticket)
        {
            var result = await Post<object>($"api/v2/qrcode/userinfo", new Dictionary<string, string>
            {
                {  "ticket",ticket}
            }).ConfigureAwait(false);

            return null;
        }
    }
}
