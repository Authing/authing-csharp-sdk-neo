using Authing.ApiClient.Types;
using Authing.Library.Domain.Model.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Whitelist
{
    public class WhitelistClienTtest : BaseTest
    {
        /// <summary>
        /// 2022-7-27 测试通过
        /// </summary>
        [Fact]
        public async void enable_whitelist_Func()
        {
            var client = managementClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var res = await client.Whitelist.Enable(WhitelistType.EMAIL | WhitelistType.PHONE | WhitelistType.USERNAME,authingErrorBox);
            Assert.True(res.Result.Whitelist.EmailEnabled & res.Result.Whitelist.PhoneEnabled & res.Result.Whitelist.UsernameEnabled);
        }

        /// <summary>
        /// 2022-7-27 测试通过
        /// </summary>
        [Fact]
        public async void disable_whitelist_Func()
        {
            var client = managementClient;

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var res = await client.Whitelist.Disable(WhitelistType.EMAIL | WhitelistType.PHONE | WhitelistType.USERNAME,authingErrorBox);
            Assert.False(res.Result.Whitelist.EmailEnabled & res.Result.Whitelist.PhoneEnabled & res.Result.Whitelist.UsernameEnabled);
        }

        /// <summary>
        /// 2022-7-27 测试通过
        /// </summary>
        [Fact]
        public async void add_whitelist_Func()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            List<string> phones = new List<string>();
            phones.Add("188888888888");

            var client = managementClient;
            var result = await client.Whitelist.Add(WhitelistType.PHONE, phones,authingErrorBox);
            foreach (var phone in phones)
            {
                Assert.NotNull(result.FirstOrDefault(c => c.Value == phone));
            }
            await client.Whitelist.Remove(WhitelistType.PHONE, phones);
        }

        /// <summary>
        /// 2022-7-27 测试通过
        /// </summary>
        [Fact]
        public async void remove_whitelist_Func()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            List<string> phones = new List<string>();
            phones.Add("188888888888");

            var client = managementClient;
            await client.Whitelist.Add(WhitelistType.PHONE, phones);
            var result = await client.Whitelist.Remove(WhitelistType.PHONE, phones,authingErrorBox);
             result = await client.Whitelist.List(WhitelistType.PHONE);
            foreach (var phone in phones)
            {
                Assert.Null(result.FirstOrDefault(c => c.Value == phone));
            }
        }

        /// <summary>
        /// 2022-7-27 测试通过
        /// </summary>
        [Fact]
        public async void get_whitelist_Func()
        {
            List<string> phones = new List<string>();
            phones.Add("188888888888");

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;
            await client.Whitelist.Add(WhitelistType.PHONE, phones);
            var result = await client.Whitelist.List(WhitelistType.PHONE,authingErrorBox);
            Assert.NotEmpty(result);
            await client.Whitelist.Remove(WhitelistType.PHONE, phones);
        }
    }
}