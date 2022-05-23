using System;
using System.Threading.Tasks;
using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Authentication;
using Authing.ApiClient.Domain.Model.Management.Users;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Interfaces;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient

{
    public partial class ManagementClient : BaseManagementClient
    {

        public IUdfManagementClient Udf { get; private set; }

        public IOrgsManagementClient Orgs { get; private set; }

        public IUsersManagementClient Users { get; private set; }
        public IRolesManagementClient Roles { get; private set; }

        public IApplicationsManagementClient Applications { get; private set; }
        public ITenantManagementClient Tennat { get; private set; }

        public IPoliciesManagementClient Policies { get; private set; }

        public IMFAManagementClient Mfa { get; private set; }

        public IWhitelistManagementClient Whitelist { get; private set; }

        public IGroupsManagementClient Groups { get; private set; }

        public IUserpoolManagement Userpool { get; private set; }

        public IStatisticsManagement Statistics { get; private set; }

        public IAclManagementClient Acl { get; private set; }

        public IPrincipalManagementClient Principal { get; private set; }

        public Action<InitAuthenticationClientOptions> Init { get; }

        public ManagementClient(string userPoolId, string secret) : base(userPoolId, secret)
        {

        }

        public ManagementClient(Action<InitAuthenticationClientOptions> init) : base(init)
        {
            Users = new UsersManagementClient(this);
            Applications = new ApplicationsManagementClient(this);
            Tennat = new TenantManagementClient(this);
            Udf = new UdfManagementClient(this);
            Orgs = new OrgsManagementClient(this);
            Roles = new RolesManagementClient(this);
            Whitelist = new WhitelistManagementClient(this);
            Groups = new GroupsManagementClient(this);
            Userpool = new UserpoolManagement(this);
            Statistics = new StatisticsManagement(this);
            Acl = new AclManagementClient(this);
            Policies = new PoliciesManagementClient(this);
            Mfa = new MFAManagementClient(this);
            Init = init ?? throw new ArgumentNullException(nameof(init));
        }

        public static async Task<ManagementClient> InitManagementClient(string userPoolId, string secret)
        {
            var manageClient = new ManagementClient(userPoolId, secret);
            await manageClient.GetAccessToken().ConfigureAwait(false);
            manageClient.Users = new UsersManagementClient(manageClient);
            manageClient.Applications = new ApplicationsManagementClient(manageClient);
            manageClient.Tennat = new TenantManagementClient(manageClient);
            manageClient.Udf = new UdfManagementClient(manageClient);
            manageClient.Orgs = new OrgsManagementClient(manageClient);
            manageClient.Roles = new RolesManagementClient(manageClient);
            manageClient.Whitelist = new WhitelistManagementClient(manageClient);
            manageClient.Groups = new GroupsManagementClient(manageClient);
            manageClient.Acl = new AclManagementClient(manageClient);
            manageClient.Userpool = new UserpoolManagement(manageClient);
            manageClient.Statistics = new StatisticsManagement(manageClient);
            manageClient.Policies = new PoliciesManagementClient(manageClient);
            manageClient.Mfa = new MFAManagementClient(manageClient);
            return manageClient;
        }


        public static async Task<ManagementClient> InitManagementClient(Action<InitAuthenticationClientOptions> init)
        {
            var manageClient = new ManagementClient(init);
            await manageClient.GetAccessToken().ConfigureAwait(false);
            manageClient.Users = new UsersManagementClient(manageClient);
            manageClient.Applications = new ApplicationsManagementClient(manageClient);
            manageClient.Tennat = new TenantManagementClient(manageClient);
            manageClient.Udf = new UdfManagementClient(manageClient);
            manageClient.Orgs = new OrgsManagementClient(manageClient);
            manageClient.Roles = new RolesManagementClient(manageClient);
            manageClient.Whitelist = new WhitelistManagementClient(manageClient);
            manageClient.Groups = new GroupsManagementClient(manageClient);
            manageClient.Acl = new AclManagementClient(manageClient);
            manageClient.Userpool = new UserpoolManagement(manageClient);
            manageClient.Statistics = new StatisticsManagement(manageClient);
            manageClient.Policies = new PoliciesManagementClient(manageClient);
            manageClient.Mfa = new MFAManagementClient(manageClient);
            return manageClient;
        }

        public async Task<string> RequestToken()
        {
            var result = await Get<AccessTokenRes>($"api/v2/userpools/accessToken?userPoolId=${UserPoolId}&secret=${Secret}", null).ConfigureAwait(false);
            return result.Data.AccessToken;
        }

        /// <summary>
        /// ��������Ƿ�Ϸ�
        /// </summary>
        /// <param name="password">��Ҫ��������</param>
        /// <returns></returns>
        public async Task<CommonMessage> isPasswordValid(string password)
        {
            var result = await Get<CommonMessage>($"api/v2/users/password/check?password={EncryptHelper.RsaEncryptWithPublic(password, PublicKey)}", null).ConfigureAwait(false);
            return result.Data;
        }
		
		/// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="email">�ʼ�</param>
        /// <param name="scene">����</param>
        /// <param name="cancellationToken"></param>
        /// <returns>CommonMessage</returns>
        public async Task<CommonMessage> SendEmail(string email,
                                                   EmailScene scene)
        {
            var param = new SendEmailParam(email, scene);
            var res = await Request<SendEmailResponse>(param.CreateRequest()).ConfigureAwait(false);
            return res.Data.Result;
        }

        /// <summary>
        /// ��ѯ�û��ĵ�¼״̬
        /// </summary>
        /// <param name="userId">�û� ID</param>
        /// <param name="appId">Ӧ�� ID</param>
        /// <param name="devicdId">ѡ��</param>
        /// <returns></returns>
        public async Task<CheckLoginStatusRes> CheckLoginStatus(string userId, string appId = null, string devicdId = null)
        {
            return await this.Users.CheckLoginStatus(userId, appId, devicdId).ConfigureAwait(false);
        }
    }
}
