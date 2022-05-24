using Authing.ApiClient.Domain.Model.Management.Tenant;
using System.Collections.Generic;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Tenant
{
    public class TennatTest : BaseTest
    {
        [Fact]
        public async void Tenant_List()
        {
            var client = managementClient;
            var result = await client.Tennat.List();
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Tenant_Details()
        {
            var client = managementClient;
            var result = await client.Tennat.Details("61c963adcc6da58494a3ef43");
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_Create()
        {
            var client = managementClient;
            var result = await client.Tennat.Create(new CreateTenantOption()
            {
                Name = "测试10-2",
                AppIds = "61c963a1631c60a9a8979bff"
            });
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_Update()
        {
            var client = managementClient;
            var result = await client.Tennat.Update("61c963adcc6da58494a3ef43", new CreateTenantOption()
            {
                Name = "测试10-1",
            });
            Assert.True(result);
        }

        [Fact]
        public async void Tenant_Delete()
        {
            var client = managementClient;
            var result = await client.Tennat.Delete("61c41429d9c21e1218a9bb93");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Tenant_Config()
        {
            var client = managementClient;
            var result = await client.Tennat.Config("61c963adcc6da58494a3ef43", new ConfigTenantOption()
            {
                SsoPageCustomizationSettings = new SsoPageCustomizationSettings()
                {
                    AutoRegisterThenLogin = false
                }
            });
            Assert.True(result);
        }

        [Fact]
        public async void Tenant_Members()
        {
            var client = managementClient;
            var result = await client.Tennat.Members("61c963adcc6da58494a3ef43", new TenantMembersOption() { });
            Assert.NotEmpty(result.List);
        }

        [Fact]
        public async void Tenant_AddMembers()
        {
            var client = managementClient;
            var result = await client.Tennat.AddMembers("61c963adcc6da58494a3ef43", new string[] { "61b1c0794929eb12c163305d" });
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_RemoveMembers()
        {
            var client = managementClient;
            var result = await client.Tennat.RemoveMembers("61c963adcc6da58494a3ef43", "61b1c0794929eb12c163305d");
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_ListExtIdp()
        {
            var client = managementClient;
            var result = await client.Tennat.ListExtIdp("61c963adcc6da58494a3ef43");
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void Tenant_ExtIdpDetail()
        {
            var client = managementClient;
            var result = await client.Tennat.ExtIdpDetail("tennat");
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_CreateExtIdp()
        {
            var client = managementClient;
            var result = await client.Tennat.CreateExtIdp(new CreateExtIdpOption()
            {
                Name = "wechat",
                Type = Types.ExtIdpType.WECHAT,
                TenantId = "61c963adcc6da58494a3ef43",
                Connections = new ExtIdpConnDetailInput[] { new ExtIdpConnDetailInput() {
                    DisplayName = "weixin2",
                    Type = Types.ExtIdpConnType.WECHATPC,
                    Identifier = "weixin2",
                    Fields = new Dictionary<string, object>(){
                        { "clientID", "1234567890" },
                        { "clientSecret", "qwertyuiop" }
                    }
                } }
            });
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_UpdateExtIdp()
        {
            var client = managementClient;
            var result = await client.Tennat.UpdateExtIdp("tennat", new UpdateExtIdpOption()
            {
                Name = "gitlab"
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Tenant_DeleteExtIdp()
        {
            var client = managementClient;
            var result = await client.Tennat.DeleteExtIdp("weixin1");
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_CreateExtIdpConnection()
        {
            var client = managementClient;
            var result = await client.Tennat.CreateExtIdpConnection(new CreateExtIdpConnectionOption()
            {
                ExtIdpId = "61cc12d21769b93fb1f55e56",
                DisplayName = "weixin3",
                Type = Types.ExtIdpConnType.WECHATPC,
                Identifier = "weixin3",
                Fields = new Dictionary<string, object>(){
                    { "clientID", "12345678901" },
                    { "clientSecret", "qwertyuiop1" }
                }
            });
            Assert.NotNull(result);
        }

        [Fact]
        public async void Tenant_UpdateExtIdpConnection()
        {
            var client = managementClient;
            var result = await client.Tennat.UpdateExtIdpConnection("61cbd8194b78e22761c523ca", new UpdateExtIdpConnectionOption()
            {
                DisplayName = "oauth--3",
                Fields = new Dictionary<string, object>() {
                    { "clientID", "123456789012345" },
                    { "clientSecret", "qwertyuiop12345" }
                }
            });
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Tenant_DeleteExtIdpConnection()
        {
            var client = managementClient;
            var result = await client.Tennat.DeleteExtIdpConnection("61cbd7ca95b42360f5374892");
            Assert.Equal(result.Code, 200);
        }

        [Fact]
        public async void Tenant_CheckExtIdpConnectionIdentifierUnique()
        {
            var client = managementClient;
            var result = await client.Tennat.CheckExtIdpConnectionIdentifierUnique("saml1");
            Assert.True(result);
        }

        [Fact]
        public async void Tenant_ChangeExtIdpConnectionState()
        {
            var client = managementClient;
            var result = await client.Tennat.ChangeExtIdpConnectionState("61cbca29206969bf63d38da9", new ChangeExtIdpConnectionStateOption()
            {
                Enabled = false
            });
            Assert.True(result);
        }

        [Fact]
        public async void Tenant_BatchChangeExtIdpConnectionState()
        {
            var client = managementClient;
            var result = await client.Tennat.BatchChangeExtIdpConnectionState("61c963adcc6da58494a3ef43", new ChangeExtIdpConnectionStateOption()
            {
                Enabled = false
            });
            Assert.True(result);
        }
    }
}