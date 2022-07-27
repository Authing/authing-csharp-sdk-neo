namespace Authing.ApiClient.Types
{
    public class InitAuthenticationClientOptions
    {
        /// <summary>
        /// Ӧ�� ID 
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// �⻧ ID 
        /// </summary>
        public string TenantId { get; set; }
        /// <summary>
        /// �û��� ID
        /// </summary>
        public string UserPoolId { get; set; }
        /// <summary>
        /// Ӧ�������������� https://sample-app.authing.cn����������б�� '/'��
        /// </summary>
        public string Host { get; set; }
        public string Authorization { get; set; }
        /// <summary>
        /// Ӧ�ó���Կ
        /// </summary>
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string RequestFrom { get; set; }
        public LangEnum Lang { get; set; } = LangEnum.ZH_CN;
        public string WebsocketHost { get; set; }

        /// <summary>
        /// ���봫����ܹ�Կ
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Ӧ�����Э��
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