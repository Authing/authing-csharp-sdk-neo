using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Oidcconfig
    {
        public IEnumerable<string> grant_types { get; set; }
        public IEnumerable<string> response_types { get; set; }
        public string id_token_signed_response_alg { get; set; }
        public string token_endpoint_auth_method { get; set; }
        public int authorization_code_expire { get; set; }
        public int id_token_expire { get; set; }
        public int access_token_expire { get; set; }
        public int refresh_token_expire { get; set; }
        public int cas_expire { get; set; }
        public bool skip_consent { get; set; }
        public IEnumerable<string> redirect_uris { get; set; }
        public object[] post_logout_redirect_uris { get; set; }
        public string client_id { get; set; }
    }
}