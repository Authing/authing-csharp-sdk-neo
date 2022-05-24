namespace Authing.ApiClient.Types
{
    public class InitAuthenticationClientOptions
    {
        public string AppId { get; set; }
        public string UserPoolId { get; set; }
        public string Host { get; set; }
        public string Authorization { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string RequestFrom { get; set; }
        public LangEnum Lang { get; set; } = LangEnum.ZH_CN;
        public string WebsocketHost { get; set; }

        public Protocol Protocol { get; set; } = Protocol.OIDC;

        public TokenEndPointAuthMethod TokenEndPointAuthMethod { get; set; } =
            TokenEndPointAuthMethod.CLIENT_SECRET_POST;

        public TokenEndPointAuthMethod IntrospectionEndPointAuthMethod { get; set; } =
            TokenEndPointAuthMethod.CLIENT_SECRET_POST;

        public TokenEndPointAuthMethod RevocationEndPointAuthMethod { get; set; } =
            TokenEndPointAuthMethod.CLIENT_SECRET_POST;
    }
}