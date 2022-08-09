using Authing.ApiClient.Domain.Model.Management.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model.Management.Acl;
using Xunit;

namespace Authing.ApiClient.Framework.Test.Management.Policies
{
    public class PoliciesTest : BaseTest
    {
        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async Task Create_Test()
        {
            var client = managementClient;

            string code = "Book:*";

            List<PolicyStatementInput> inputList = new List<PolicyStatementInput>();

            List<string> action = new List<string>(){"Book:read"};

            PolicyStatementInput input = new PolicyStatementInput(code, action);
            input.Effect = Types.PolicyEffect.ALLOW.ToString();

            List<PolicyStatementConditionInput> listCondtions = new List<PolicyStatementConditionInput>();

            //PolicyStatementConditionInput condition = new PolicyStatementConditionInput("int", "+", new object());
            //listCondtions.Add(condition);

            input.Condition = listCondtions;
            inputList.Add(input);

            var result = await client.Policies.Create(code, inputList, "testdesc", nameSpace: "default");

            var list = await client.Policies.List(1, 10);

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void List_Test()
        {
            var client = managementClient;

            var result = await client.Policies.List(1, 10,nameSpace:"system");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListWithParam_Test()
        {
            var client = managementClient;

            var result = await client.Policies.List(new PoliciesParam { Page = 1, Limit = 10 });

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void Detail_Test()
        {
            var client = managementClient;
            var result = await client.Policies.Detail("Book:*");

            Assert.NotNull(result);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void Update_Test()
        {
            var client = managementClient;

            var list = await client.Policies.List();

            var po = list.List.FirstOrDefault(p => p.Code == "Book:*");

            foreach (var item in po.Statements)
            {
                item.Effect = Domain.Model.Management.Acl.PolicyEffect.DENY;
            }

            UpdatePolicyParam py = new UpdatePolicyParam(po.Code);

            py.Statements = new List<PolicyStatementInput>()
            {
                new PolicyStatementInput(po.Code,po.Statements.First().Actions)
                {
                    Effect = Domain.Model.Management.Acl.PolicyEffect.DENY.ToString()
                }
            };

            var result = await client.Policies.Update(py);
            Assert.True(result.Statements.First().Effect == PolicyEffect.DENY);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async Task Delete_Test()
        {
            var client = managementClient;

            string code = "Book:*"; 
            
            //await Create_Test();
            
            var result = await client.Policies.Delete(code);

            Assert.True(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DeleteMany_Test()
        {
            var client = managementClient;

            List<string> codeList = new List<string>();

            var list = await client.Policies.List();

            foreach (var i in list.List)
            {
                codeList.Add(i.Code);
            }

            var result = await client.Policies.DeleteMany(codeList);
            Assert.True(result.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void AddAssignments_Test()
        {
            var client = managementClient;

            var result = await client.Policies.List(1, 100, "default");

            List<string> poList = result.List.Select(p => p.Code).ToList();

            List<string> targetIden = new List<string>();
            
            targetIden.Add(TestUserId);

            var commonMessage = await client.Policies.AddAssignments(poList, Types.PolicyAssignmentTargetType.USER, targetIden, "default");
           
            Assert.True(commonMessage.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void RemoveAssignments_Test()
        {
            var client = managementClient;

            List<string> poList = new List<string>();
            poList.Add("Book:*");

            List<string> targetIden = new List<string>();
            targetIden.Add(TestUserId);

            var commonMessage = await client.Policies.RemoveAssignments(poList, Types.PolicyAssignmentTargetType.USER, targetIden);
            Assert.True(commonMessage.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void DisableAssignments_Test()
        {
            var client = managementClient;
            var commonMessage = await client.Policies.DisableAssignment("Book:*", Types.PolicyAssignmentTargetType.USER, TestUserId, "default");
            Assert.True(commonMessage.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void EnableAssignments_Test()
        {
            var client = managementClient;
            var commonMessage = await client.Policies.EnableAssignment("Book:*", Types.PolicyAssignmentTargetType.USER, TestUserId, "default");
            Assert.True(commonMessage.Code == 200);
        }

        /// <summary>
        /// 2022-8-9 测试通过
        /// </summary>
        [Fact]
        public async void ListAssignments_Test()
        {
            var client = managementClient;

            var list = await client.Policies.List();

            string code = list.List.First().Code;

            var result = await client.Policies.ListAssignments(code);
            Assert.NotNull(result);
        }
    }
}