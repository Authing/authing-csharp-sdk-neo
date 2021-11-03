using Authing.ApiClient.Mgmt;
using Xunit;

namespace Authing.ApiClient.Netstandard20.Test.Users
{
    public class get_user_detail
    {
        [Fact]
        public async void should_get_user_detail_correct()
        {
            var client =
                await ManagementClient.InitManagementClient("61797ac183d49f46bcd3574a",
                    "c9dbab9f4dd0547768a58c5c8a5a83ea");
            var user = await client.Users.Detail("61797b1465176060e42f6d01", true);
            Assert.NotNull(user);
            Assert.Equal("16666667777", user.Phone);
        }
    }
}