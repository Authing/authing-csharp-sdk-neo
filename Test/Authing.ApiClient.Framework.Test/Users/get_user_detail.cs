using Authing.Library.Domain.Model.Exceptions;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Users
{
    public class get_user_detail : BaseTest
    {
        [Fact]
        public async void should_get_user_detail_correct()
        {
            var client = managementClient;
            var error = new AuthingErrorBox();
            var user = await client.Users.Detail(TestUserId, authingErrorBox: error);
            Assert.NotNull(user);
            Assert.Equal("1800000000", user.Phone);
        }
    }
}