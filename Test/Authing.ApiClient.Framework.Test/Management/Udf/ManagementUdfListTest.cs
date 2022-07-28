﻿using System.Collections.Generic;
using System.Linq;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Udf
{
    public class ManagementUdfListTest : BaseTest
    {
        [Fact]
        public async void ListUserDefinedField_User()
        {
            var client = managementClient;

            await client.Udf.Set(UdfTargetType.USER, "user", UdfDataType.STRING, "userString");

            IEnumerable<UserDefinedField> result = await client.Udf.List(UdfTargetType.USER);

            Assert.NotNull(result.Count() > 0);
        }
    }
}