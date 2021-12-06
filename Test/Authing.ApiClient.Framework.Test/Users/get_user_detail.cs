using Xunit;

namespace Authing.ApiClient.Framework.Test.Users
{
    public class get_user_detail : BaseTest
    {
        [Fact]
        public async void should_get_user_detail_correct()
        {
            var client = managementClient;
            var user = await client.Users.Detail(TestUserId);
            Assert.NotNull(user);
            Assert.Equal("17620671314", user.Phone);
        }
    }
}