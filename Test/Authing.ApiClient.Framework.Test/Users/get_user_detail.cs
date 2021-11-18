using Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient;
using Authing.ApiClient.Test.Base;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Users
{
    public class get_user_detail : TestBase
    {
        [Fact]
        public async void should_get_user_detail_correct()
        {
            var client =
                await ManagementClient.InitManagementClient(UserPoolId, Secret);
            var user = await client.Users.Detail(TestUserId);
            Assert.NotNull(user);
            Assert.Equal("16666667777", user.Phone);
        }
    }
}