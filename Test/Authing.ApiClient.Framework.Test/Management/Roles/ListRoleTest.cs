﻿using Authing.Library.Domain.Model.Exceptions;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Roles
{
    public class ListRoleTest : BaseTest
    {
        [Fact]
        public async void ListRole_Test()
        {
            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            var client = managementClient;

            for (int i = 0; i < 10; i++)
            {
                string roleCode = i.ToString();

                await client.Roles.Create(roleCode,authingErrorBox:authingErrorBox);
            }

           

            var result = await client.Roles.List();

            Assert.True(result.List.Count() == 10);
        }

        [Fact]
        public async void ListRoleWithNameSpace_Test()
        {
            var client = managementClient;

            string nameSpace = "613189b38b6c66cac1d211bd";

            AuthingErrorBox authingErrorBox = new AuthingErrorBox();

            for (int i = 0; i < 10; i++)
            {
                string roleCode = i.ToString();

                await client.Roles.Create(roleCode, null, null, nameSpace,authingErrorBox);
            }

            var result = await client.Roles.List(nameSpace,authingErrorBox:authingErrorBox);

            Assert.True(result.List.Count() == 10);
        }

        [Fact]
        public async void ListRoleWithNameSpaceAndPage_Test()
        {
            var client = managementClient;

            string nameSpace = "613189b38b6c66cac1d211bd";
            for (int i = 0; i < 10; i++)
            {
                string roleCode = i.ToString();

                await client.Roles.Create(roleCode, null, null, nameSpace);
            }

            var result = await client.Roles.List(nameSpace, 1, 5);

            Assert.True(result.List.Count() == 5);
        }
    }
}