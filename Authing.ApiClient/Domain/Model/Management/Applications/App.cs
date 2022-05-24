using System.Collections.Generic;

namespace Authing.ApiClient.Domain.Model.Management.Applications
{
    public class App
    {
        public KeyValuePair<string, object> QrcodeScanning { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public object? Description { get; set; }

        public string Identifier { get; set; }

        public string Logo { get; set; }

        public string[] LoginTabs { get; set; }

        public string[] RegisterTabs { get; set; }

        public object[] AdConnections { get; set; }

        public object[] DisabledOidcConnections { get; set; }

        public object[] DisabledSamlConnections { get; set; }

        public object[] ExtendsFields { get; set; }

        public object[] DisabledAzureAdConnections { get; set; }

        public object[] DisabledOauth2Connections { get; set; }

        public object[] DisabledCasConnections { get; set; }
    }
}