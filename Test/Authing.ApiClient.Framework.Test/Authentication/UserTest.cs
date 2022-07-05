using Authing.ApiClient.Domain.Utils;
using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class UserTest : BaseTest
    {
        [Fact]
        public async void GetCurrentUser_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);
            var user = await client.GetCurrentUser();

            Assert.NotNull(user);
        }

        [Fact]
        public async void User_GetCurrentUserWithToken()
        {
            var client = authenticationClient;
            await client.LoginByUsername("13348926753", "3866364", null);
            var user = await client.GetCurrentUser(client.AccessToken);

            Assert.NotNull(user);
        }

        [Fact]
        public async void Logout_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);
            client.CheckLoggedIn();

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var msg = await client.Logout(authingErrorBox);

            Assert.True(msg.Code == 200);
        }

        [Fact]
        public async void SetUdfValue_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var kic = new Types.KeyValueDictionary { };

            kic.Add("key1", "value1");

            var result = await client.SetUdfValue(kic);

            Assert.NotNull(result);
        }

        [Fact]
        public async void SetUdv_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var dics = await client.ListUdv();

            var result = await client.SetUdv("UserInfo", "value11");

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetUdfValue_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var dic = new Types.KeyValueDictionary { };

            dic.Add("22222", "222");

            await client.SetUdfValue(dic);

            var result = await client.GetUdfValue();

            Assert.NotNull(result);
        }

        [Fact]
        public async void RemoveUdfValue_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var dic = new Types.KeyValueDictionary { };

            dic.Add("22222", "222");

            await client.SetUdfValue(dic);

            var result = await client.RemoveUdfValue("UserInfo");

            Assert.NotNull(result);
        }

        [Fact]
        public async void RemoveUdv_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var dics = await client.ListUdv();

            var kic = new Types.KeyValueDictionary { };

            kic.Add("key1", "value1");

            await client.SetUdfValue(kic);

            var result = await client.RemoveUdv("key1");

            Assert.NotNull(result);
        }

        [Fact]
        public async void ListOrg_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong9999", "3866364", null);

            var result = await client.ListOrgs();

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetSecurityLevel_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.GetSecurityLevel(authingErrorBox);

            Assert.True(result.Password);
        }

        [Fact]
        public async void listAuthorizedResources_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.ListAuthorizedResources("default", Types.ResourceType.DATA,authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public void computedPasswordSecurityLevel_Test()
        {
            var client = authenticationClient;

            var result = client.ComputedPasswordSecurityLevel("qd,,.");

            Assert.NotNull(result == Types.PasswordSecurityLevel.HIGH);
        }

        [Fact]
        public async void HasRole_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.HasRole("admin", authingErrorBox: authingErrorBox);

            Assert.False(result);
        }

        [Fact]
        public async void LoginSubAccount_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.LoginBySubAccount("qidong6655", "qd3866364", null, authingErrorBox);

            Assert.NotNull(result);
        }

        [Fact]
        public async void LinkAccount_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.LinkAccount(user.Token, "11233");

            Assert.NotNull(result);
        }

        [Fact]
        public async void UnLinkAccount_Test()
        {
            var client = authenticationClient;
            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.UnLinkAccount("qidong5566", Types.ProviderType.ALIPAY);
        }

        [Fact]
        public async void ListApplications_Text()
        {
            var client = authenticationClient;

            var result = await client.LoginByUsername("qidong6655", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var apps = await client.ListApplications(authingErrorBox: authingErrorBox);

            Assert.NotNull(apps);
        }

        [Fact]
        public async void ResetPasswordByFirstLoginToken_Test()
        {
            var client = authenticationClient;

            await client.LoginBySubAccount("qidong6655", "qd3866364", null);

            var result = await client.ListDepartment();

            Assert.NotNull(result);
        }

        [Fact]
        public async void IsUserExist_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.IsUserExists("qidong556", null, null, null);

            Assert.True(result);
        }

        [Fact]
        public async void isUserExitFalse_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.IsUserExists("1231231", null, null, null);

            Assert.False(result);
        }

        [Fact]
        public async void IsPasswordValid_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.isPasswordValid("1",authingErrorBox);

            Assert.False(result.Message == "");
        }

        [Fact]
        public async void ResetPasswordByFirstLoginToken()
        {
            var client = authenticationClient;
            var result = await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var message = await client.ResetPasswordByFirstLoginToken(client.AccessToken, "3866364",authingErrorBox);

            Assert.True(message.Code == 200);
        }

        [Fact]
        public async void ResetPasswordByForceResetToken()
        {
            var client = authenticationClient;
            var result = await client.LoginByUsername("qidong5566", "12345678", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var message = await client.ResetPasswordByForceResetToken(client.AccessToken, "12345678", "3866364", authingErrorBox);

            Assert.True(message.Code == 200);
        }

        [Fact]
        public async void GetToken_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.GetToken();

            Assert.True(!string.IsNullOrEmpty(result));
        }

        [Fact]
        public async void ClearUser_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("qidong5566", "12345678", null);

            client.ClearUser();

            Assert.Null(client.User);
        }

        [Fact]
        public void EncryptTest()
        {
            string PublicKey  = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4xKeUgQ+Aoz7TLfAfs9+paePb
5KIofVthEopwrXFkp8OCeocaTHt9ICjTT2QeJh6cZaDaArfZ873GPUn00eOIZ7Ae
+TiA2BKHbCvloW3w5Lnqm70iSsUi5Fmu9/2+68GZRH9L7Mlh8cFksCicW2Y2W2uM
GKl64GDcIq3au+aqJQIDAQAB
-----END PUBLIC KEY-----";

            EncryptHelper.RsaEncryptWithPublic("3866364", PublicKey);
        }
    }
}