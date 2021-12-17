using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.ApiClient.Domain.Model.Authentication
{
    public class OidcConfig
    {
        public int Id { get; set; }
        public string ClientSecret { get; set; }

        public string[] RedirectUris { get; set; }

        public string[] Grants { get; set; }

        public int AccessTokenLifeTime { get; set; }

        public int RefreshTokenLifetime { get; set; }

        public string IntrospectionEndpointAuthMethod { get; set; }

        public string RevocationEndpointAuthMethod { get; set; }
    }
}
