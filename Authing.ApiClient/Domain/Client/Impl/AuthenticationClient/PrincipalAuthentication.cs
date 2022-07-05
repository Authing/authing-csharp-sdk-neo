using Authing.ApiClient.Domain.Client.Impl.AuthenticationClient;
using Authing.ApiClient.Extensions;
using Authing.ApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authing.Library.Domain.Model.Authentication;
using Authing.Library.Domain.Model.Exceptions;

namespace Authing.Library.Domain.Client.Impl.AuthenticationClient
{
    public class PrincipalAuthentication : BaseAuthenticationClient
    {
        public PrincipalAuthentication(Action<InitAuthenticationClientOptions> init) : base(init)
        {

        }

        /// <summary>
        ///  获取主体认证详情
        /// </summary>
        /// <returns></returns>
        public async Task<PrincipalDetail> Detail(AuthingErrorBox authingErrorBox=null)
        {
            var result = await RequestCustomDataWithOutToken<object>("/api/v2/users/principal_authentication",method:System.Net.Http.HttpMethod.Get);
            ErrorHelper.LoadError(result, authingErrorBox);
            return (PrincipalDetail)result.Data  ;
        }

        /// <summary>
        /// 进行主体认证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<bool> Authenticate(PrincipalInfo info,AuthingErrorBox authingErrorBox=null)
        {
            Dictionary<string, string> dic = new Dictionary<string, string> { };

            if (info.Type == PrincipalType.P)
            {
                dic.Add("type", info.Type.ToString());
                dic.Add("name", info.Name);
                dic.Add("idCard", info.IdCard);
                dic.Add("ext", info.BankCard);
            }
            else if (info.Type == PrincipalType.E)
            {
                dic.Add("type", info.Type.ToString());
                dic.Add("name", info.EnterpriseName);
                dic.Add("idCard", info.EnterpriseCode);
                dic.Add("ext", info.LegalPersonName);
            }

            var result = await RequestCustomDataWithOutToken<bool>("/api/v2/users/principal_authentication",info.ConvertJson(),method:System.Net.Http.HttpMethod.Post,contenttype:ContentType.JSON);

            ErrorHelper.LoadError(result, authingErrorBox);

            return result.Data;
        }
    }
}
