using System;
using System.Net.Http;
using Authing.ApiClient.Mgmt;
using Xunit;

namespace Authing.ApiClient.Netstandard20.Test.SDKInit
{
    public class Class1
    {
        [Fact]
        public async void should_init_authing_sdk_with_userpool_and_secret()
        {
            var client = await ManagementClient.InitManagementClient("61797ac183d49f46bcd3574a", "c9dbab9f4dd0547768a58c5c8a5a83ea");
            Assert.NotEmpty(client.AccessToken); 
            Assert.NotNull(client.Users);
            Assert.NotNull(client.Roles);
            Assert.NotNull(client.Acl);
            Assert.NotNull(client.Groups);
            Assert.NotNull(client.Orgs);
            Assert.NotNull(client.Udf);
            Assert.NotNull(client.Whitelist);
            Assert.NotNull(client.Userpool);
            Assert.NotNull(client.Policies);

        }
        
        [Fact]
        public async void should_init_authing_sdk_with_init_option()
        {
            var client = await ManagementClient.InitManagementClient(init: opt =>
            {
                opt.UserPoolId = "61797ac183d49f46bcd3574a";
                opt.Secret = "c9dbab9f4dd0547768a58c5c8a5a83ea";
            });
            Assert.NotEmpty(client.AccessToken); 
            Assert.NotNull(client.Users);
            Assert.NotNull(client.Roles);
            Assert.NotNull(client.Acl);
            Assert.NotNull(client.Groups);
            Assert.NotNull(client.Orgs);
            Assert.NotNull(client.Udf);
            Assert.NotNull(client.Whitelist);
            Assert.NotNull(client.Userpool);
            Assert.NotNull(client.Policies);

        }
        
    }
}
