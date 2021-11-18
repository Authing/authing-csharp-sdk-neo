using Authing.ApiClient.Domain.Client;
using Authing.ApiClient.Test.Base;
using Xunit;

namespace Authing.ApiClient.Netstandard20_up.Test.Users
{
    public class get_user_detail : TestBase
    {
        [Fact]
        public async void should_get_user_detail_correct()
        {
            var client =
                await ManagementClient.InitManagementClient(UserPoolId,
                    Secret);
            var user = await client.Users.Detail(TestUserId, true);
            Assert.NotNull(user);
            Assert.Equal("16666667777", user.Phone);
        }
    }
}