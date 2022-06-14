using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Authing.Library.Domain.Model.Authentication
{
    public class GeneQrCodeParam
    {
        public GeneQrCodeParam()
        {
           
        }

        [JsonProperty("autoMergeQrCode")]
        public bool AutoMergeQrCode { get; set; }

        /// <summary>
        /// 场景
        /// </summary>
        [JsonProperty("scene")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QrCodeScene Scene { get; set; }

        ///// <summary>
        ///// 自定义信息
        ///// </summary>
        //[JsonProperty("params")]
        //public string Params { get; set; }

        //[JsonProperty("context")]
        //public string Context { get; set; }
    }

    public enum QrCodeScene
    { 
        /// <summary>
        /// 微信小程序扫码
        /// </summary>
       [EnumMember(Value = "WXAPP_AUTH")]
        WXAPP_AUTH,
        /// <summary>
        /// APP 扫码
        /// </summary>
        [EnumMember(Value = "APP_AUTH")]
        APP_AUTH,
        /// <summary>
        /// 微信公众号扫码
        /// </summary>
        [EnumMember(Value = "WECHATMP_AUTH")]
        WECHATMP_AUTH
    }
}
