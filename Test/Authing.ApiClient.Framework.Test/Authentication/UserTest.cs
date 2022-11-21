using Authing.ApiClient.Domain.Utils;
using Authing.Library.Domain.Model.Authentication;
using Authing.Library.Domain.Model.Exceptions;
using System;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Authentication
{
    public class UserTest : BaseTest
    {
        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void GetCurrentUser_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);
            var user = await client.GetCurrentUser();

            Assert.NotNull(user);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void User_GetCurrentUserWithToken()
        {
            var client = authenticationClient;
            await client.LoginByUsername("tmgg", "88886666", null);
            var user = await client.GetCurrentUser(client.AccessToken);

            Assert.NotNull(user);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void Logout_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);
            client.CheckLoggedIn();

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var msg = await client.Logout(authingErrorBox);

            Assert.True(msg.Code == 200);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void SetUdfValue_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var kic = new Types.KeyValueDictionary { };

            kic.Add("testprop", "123456");

            var result = await client.SetUdfValue(kic);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void SetUdv_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var dics = await client.ListUdv();

            var result = await client.SetUdv("testprop", "00000000");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void GetUdfValue_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var dic = new Types.KeyValueDictionary { };

            dic.Add("testprop", "12345678");

            await client.SetUdfValue(dic);

            var result = await client.GetUdfValue();

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void RemoveUdfValue_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var dic = new Types.KeyValueDictionary { };

            dic.Add("testprop", "12345678");

            await client.SetUdfValue(dic);

            var result = await client.RemoveUdfValue("testprop");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void RemoveUdv_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var dics = await client.ListUdv();

            var kic = new Types.KeyValueDictionary { };

            kic.Add("testprop", "99999999");

            await client.SetUdfValue(kic);

            var result = await client.RemoveUdv("testprop");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void ListOrg_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var result = await client.ListOrgs();

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void GetSecurityLevel_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.GetSecurityLevel(authingErrorBox);

            Assert.True(result.Password);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void listAuthorizedResources_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.ListAuthorizedResources("default", Types.ResourceType.DATA, authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public void computedPasswordSecurityLevel_Test()
        {
            var client = authenticationClient;

            var result = client.ComputedPasswordSecurityLevel("qd,,.");

            Assert.NotNull(result == Types.PasswordSecurityLevel.HIGH);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void HasRole_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.HasRole("admin", authingErrorBox: authingErrorBox);

            Assert.False(result);
        }

        /// <summary>
        /// 2022-11-2 子账号功能已下线 
        /// </summary>
        [Fact]
        public async void LoginSubAccount_Test()
        {
            var client = authenticationClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.LoginBySubAccount("tmgg", "88886666", null, authingErrorBox);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试不通过
        /// </summary>
        [Fact]
        public async void LinkAccount_Test()
        {
            var client = authenticationClient;

            var user = await client.LoginByUsername("tmgg", "88886666", null);
            var user2 = await client.LoginByUsername("test", "88886666", null);

            var result = await client.LinkAccount(user.Token, user2.Token);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试不通过
        /// </summary>
        [Fact]
        public async void UnLinkAccount_Test()
        {
            var client = authenticationClient;
            var user = await client.LoginByUsername("qidong5566", "12345678", null);

            var result = await client.UnLinkAccount("qidong5566", Types.ProviderType.ALIPAY);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void ListApplications_Text()
        {
            var client = authenticationClient;

            var result = await client.LoginByUsername("tmgg", "88886666", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var apps = await client.ListApplications(authingErrorBox: authingErrorBox);

            Assert.NotNull(apps);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void ListUserDepartment_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var result = await client.ListDepartment();

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void IsUserExist_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var result = await client.IsUserExists("tmgg", null, null, null);

            Assert.True(result);
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void isUserExitFalse_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var result = await client.IsUserExists("1231231", null, null, null);

            Assert.False(result);
        }

        /// <summary>
        /// 2022-8-11 测试不通过
        /// </summary>
        [Fact]
        public async void IsPasswordValid_Test()
        {
            var client = authenticationClient;

            var re=await client.LoginByUsername("qidong5566", "3866364", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.isPasswordValid("3866364", authingErrorBox);

            Assert.False(result.Message == "");
        }

        /// <summary>
        /// 2022-8-11 测试不通过
        /// 
        /// 2022-09-01 测试通过
        /// 传入的 token 为登录失败后获取到的 token
        /// </summary>
        [Fact]
        public async void ResetPasswordByFirstLoginToken()
        {
            var client = authenticationClient;
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var result = await client.LoginByUsername("qidong11233", "3866364", null,authingErrorBox);



            if (!string.IsNullOrWhiteSpace(authingErrorBox.Value.First().Message.Data.ToString()))
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ForceLoginResponse>(authingErrorBox.Value.First().Message.Data.ToString());

                var message = await client.ResetPasswordByFirstLoginToken(res.Token, "88886666", authingErrorBox);

                Assert.True(message.Code == 200);
            }
        }

        /// <summary>
        /// 2022-8-11 测试不通过
        /// 
        /// 2022-09-01 测试通过
        /// 传入的 token 为登录失败后获取到的 token
        /// </summary>
        [Fact]
        public async void ResetPasswordByForceResetToken()
        {
            var client = authenticationClient;
            //var result = await client.LoginByUsername("qidong11233", "3866364", null);

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            string oldPassword = "88886666";

            var result = await client.LoginByUsername("qidong11233", oldPassword, null, authingErrorBox);

            if (!string.IsNullOrWhiteSpace(authingErrorBox.Value.First().Message.Data.ToString()))
            {
                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ForceLoginResponse>(authingErrorBox.Value.First().Message.Data.ToString());

                var message = await client.ResetPasswordByForceResetToken(res.Token, oldPassword, "88886666", authingErrorBox);

                Assert.True(message.Code == 200);
            }
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
        [Fact]
        public async void GetToken_Test()
        {
            var client = authenticationClient;

            await client.LoginByUsername("tmgg", "88886666", null);

            var result = await client.GetToken();

            Assert.True(!string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// 2022-8-11 测试通过
        /// </summary>
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
            string PublicKey = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC4xKeUgQ+Aoz7TLfAfs9+paePb
5KIofVthEopwrXFkp8OCeocaTHt9ICjTT2QeJh6cZaDaArfZ873GPUn00eOIZ7Ae
+TiA2BKHbCvloW3w5Lnqm70iSsUi5Fmu9/2+68GZRH9L7Mlh8cFksCicW2Y2W2uM
GKl64GDcIq3au+aqJQIDAQAB
-----END PUBLIC KEY-----";

            EncryptHelper.RsaEncryptWithPublic("3866364", PublicKey);
        }
    }
}