using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class QrcodeLoginStrategy
    {
        #region members
        [JsonProperty("qrcodeExpiresAfter")]
        public int? QrcodeExpiresAfter { get; set; }

        [JsonProperty("returnFullUserInfo")]
        public bool? ReturnFullUserInfo { get; set; }

        [JsonProperty("allowExchangeUserInfoFromBrowser")]
        public bool? AllowExchangeUserInfoFromBrowser { get; set; }

        [JsonProperty("ticketExpiresAfter")]
        public int? TicketExpiresAfter { get; set; }
        #endregion
    }

}
