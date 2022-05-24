using Authing.ApiClient.Domain.Model.Management.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Policies
{
    public class PoliciesTest : BaseTest
    {
        [Fact]
        public async void Create_Test()
        {
            var client = managementClient;

            string code = new Random().Next(1, 5000).ToString();

            List<PolicyStatementInput> inputList = new List<PolicyStatementInput>();
            List<string> actions = new List<string>();
            for (int i = 0; i < 1; i++)
            {
                List<string> action = new List<string>();
                if (i == 0)
                {
                    action.Add($"{code}:read");
                }
                else
                {
                    action.Add($"{code}:write");
                }

                PolicyStatementInput input = new PolicyStatementInput($"{code}:delete", action);
                input.Effect = Types.PolicyEffect.ALLOW.ToString();
                List<PolicyStatementConditionInput> listCondtions = new List<PolicyStatementConditionInput>();

                PolicyStatementConditionInput condition = new PolicyStatementConditionInput("int", "+", new object());
                listCondtions.Add(condition);
                input.Condition = listCondtions;
                inputList.Add(input);
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(inputList);

            var result = await client.Policies.Create(code, inputList, "diescadsada");

            var list = await client.Policies.List(1, 10);

            Assert.NotNull(result);
        }

        [Fact]
        public async void List_Test()
        {
            var client = managementClient;

            var result = await client.Policies.List(1, 10);

            Assert.NotNull(result);
        }

        [Fact]
        public async void ListWithParam_Test()
        {
            var client = managementClient;

            var result = await client.Policies.List(new PoliciesParam { Page = 1, Limit = 10 });

            Assert.NotNull(result);
        }

        [Fact]
        public async void Detail_Test()
        {
            var client = managementClient;
            var result = await client.Policies.Detail("order");

            Assert.NotNull(result);
        }

        [Fact]
        public async void Update_Test()
        {
            var client = managementClient;

            var list = await client.Policies.List();

            var po = list.List.Where(p => p.Code == "order").FirstOrDefault();

            foreach (var item in po.Statements)
            {
                item.Effect = Domain.Model.Management.Acl.PolicyEffect.ALLOW;
            }

            // var result = await client.Policies.Update("order",);
        }

        [Fact]
        public async void Delete_Test()
        {
            var client = managementClient;

            string code = "test_Code";

            CreatePolicy(code);

            var result = await client.Policies.Delete(code);

            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteMany_Test()
        {
            var client = managementClient;

            List<string> codeList = new List<string>();

            var list = await client.Policies.List();

            for (int i = 0; i < 5; i++)
            {
                CreatePolicy(i.ToString());

                codeList.Add(i.ToString());
            }

            var result = await client.Policies.DeleteMany(codeList);
            Assert.True(result.Code == 200);
        }

        [Fact]
        public async void AddAssignments_Test()
        {
            var client = managementClient;

            for (int i = 0; i < 10; i++)
            {
                CreatePolicy(i.ToString(), "613189b38b6c66cac1d211bd");
            }

            var result = await client.Policies.List(1, 100, "613189b38b6c66cac1d211bd");

            List<string> poList = result.List.Select(p => p.Code).ToList();
            poList.Add("order");

            List<string> targetIden = new List<string>();
            targetIden.Add("qidong5566");

            var commonMessage = await client.Policies.AddAssignments(poList, Types.PolicyAssignmentTargetType.USER, targetIden, "613189b38b6c66cac1d211bd");
            Assert.True(commonMessage.Code == 200);
        }

        [Fact]
        public async void RemoveAssignments_Test()
        {
            var client = managementClient;

            //var poList = await client.Policies.List();

            List<string> poList = new List<string>();
            poList.Add("order");

            List<string> targetIden = new List<string>();
            targetIden.Add("qidong5566");

            var commonMessage = await client.Policies.RemoveAssignments(poList, Types.PolicyAssignmentTargetType.USER, targetIden);
            Assert.True(commonMessage.Code == 200);
        }

        [Fact]
        public async void DisableAssignments_Test()
        {
            var client = managementClient;

            List<string> poList = new List<string>();
            poList.Add("order");

            List<string> targetIden = new List<string>();
            targetIden.Add("qidong5566");

            var commonMessage = await client.Policies.DisableAssignment("1", Types.PolicyAssignmentTargetType.USER, "qidong5566", "613189b38b6c66cac1d211bd");
            Assert.True(commonMessage.Code == 200);
        }

        [Fact]
        public async void EnableAssignments_Test()
        {
            var client = managementClient;

            List<string> poList = new List<string>();
            poList.Add("order");

            List<string> targetIden = new List<string>();
            targetIden.Add("qidong5566");

            var commonMessage = await client.Policies.EnableAssignment("1", Types.PolicyAssignmentTargetType.USER, "qidong5566", "613189b38b6c66cac1d211bd");
            Assert.True(commonMessage.Code == 200);
        }

        [Fact]
        public async void ListAssignments_Test()
        {
            var client = managementClient;

            var list = await client.Policies.List();

            string code = list.List.First().Code;

            var result = await client.Policies.ListAssignments(code);
            Assert.NotNull(result);
        }

        private async void CreatePolicy(string createCode, string nameSpace = null)
        {
            var client = managementClient;

            string code = createCode;

            List<PolicyStatementInput> inputList = new List<PolicyStatementInput>();
            List<string> actions = new List<string>();
            for (int i = 0; i < 1; i++)
            {
                List<string> action = new List<string>();
                if (i == 0)
                {
                    action.Add($"{code}:read");
                }
                else
                {
                    action.Add($"{code}:write");
                }

                PolicyStatementInput input = new PolicyStatementInput($"{code}:delete", action);
                input.Effect = Types.PolicyEffect.ALLOW.ToString();
                List<PolicyStatementConditionInput> listCondtions = new List<PolicyStatementConditionInput>();

                PolicyStatementConditionInput condition = new PolicyStatementConditionInput("int", "+", new object());
                listCondtions.Add(condition);
                input.Condition = listCondtions;
                inputList.Add(input);
            }

            await client.Policies.Create(code, inputList, "diescadsada", nameSpace);
        }
    }
}