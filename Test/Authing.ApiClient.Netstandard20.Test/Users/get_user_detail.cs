using Xunit;

namespace Authing.ApiClient.Netstandard20.Test.Users
{
    public class get_user_detail : BaseTest
    {
        [Fact]
        public async void should_get_user_detail_correct()
        {
            var client = managementClient = await GetManagementClient();
            var user = await client.Users.Detail(TestUserId, true);
            Assert.NotNull(user);
            Assert.Equal("16666667777", user.Phone);
        }
    }
}