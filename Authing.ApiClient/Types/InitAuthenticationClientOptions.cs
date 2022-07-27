namespace Authing.ApiClient.Types
{
    public class InitAuthenticationClientOptions
    {
        /// <summary>
        /// 应用 ID 
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 租户 ID 
        /// </summary>
        public string TenantId { get; set; }
        /// <summary>
        /// 用户池 ID
        /// </summary>
        public string UserPoolId { get; set; }
        /// <summary>
        /// 应用完整域名，如 https://sample-app.authing.cn，不带最后的斜线 '/'。
        /// </summary>
        public string Host { get; set; }
        public string Authorization { get; set; }
        /// <summary>
        /// 应用池密钥
        /// </summary>
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string RequestFrom { get; set; }
        public LangEnum Lang { get; set; } = LangEnum.ZH_CN;
        public string WebsocketHost { get; set; }

        /// <summary>
        /// 密码传输加密公钥
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// 应用身份协议
        /// </summary>
        public Protocol Protocol { get; set; } = Protocol.OIDC;

        public TokenEndPointAuthMethod TokenEndPointAuthMethod { get; set; } =
            TokenEndPointAuthMethod.CLIENT_SECRET_POST;

        public TokenEndPointAuthMethod IntrospectionEndPointAuthMethod { get; set; } =
            TokenEndPointAuthMethod.CLIENT_SECRET_POST;

        public TokenEndPointAuthMethod RevocationEndPointAuthMethod { get; set; } =
            TokenEndPointAuthMethod.CLIENT_SECRET_POST;
    }
}