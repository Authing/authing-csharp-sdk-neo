using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class App2WxappLoginStrategy
    {
        #region members
        [JsonProperty("ticketExpriresAfter")]
        public int? TicketExpriresAfter { get; set; }

        [JsonProperty("ticketExchangeUserInfoNeedSecret")]
        public bool? TicketExchangeUserInfoNeedSecret { get; set; }
        #endregion
    }
}
