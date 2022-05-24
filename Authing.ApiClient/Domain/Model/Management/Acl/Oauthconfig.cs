using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Acl
{
    public class Oauthconfig
    {
        public string id { get; set; }
        public IEnumerable<string> redirect_uris { get; set; }
        public IEnumerable<string> grants { get; set; }
        public int access_token_lifetime { get; set; }
        public int refresh_token_lifetime { get; set; }
        public string introspection_endpoint_auth_method { get; set; }
        public string revocation_endpoint_auth_method { get; set; }
    }
}