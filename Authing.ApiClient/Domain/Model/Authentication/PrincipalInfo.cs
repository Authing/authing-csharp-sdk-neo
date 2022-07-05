using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authing.Library.Domain.Model.Authentication
{
    public class PrincipalInfo
    {
        /// <summary>
        /// 认证类型
        /// </summary>
        public PrincipalType Type { set; get; }

        /// <summary>
        /// 个人真实名字
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 个人身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 个人银行卡号
        /// </summary>
        public string BankCard { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 企业统一社会信用代码
        /// </summary>
        public string EnterpriseCode { get; set; }

        /// <summary>
        /// 企业法人名字
        /// </summary>
        public string LegalPersonName { get; set; }
    }

    public enum PrincipalType
    {
        /// <summary>
        /// 个人认证
        /// </summary>
        [EnumMember(Value ="P")]
        P,
        /// <summary>
        /// 企业认证
        /// </summary>
        [EnumMember(Value ="E")]
        E
    }
}
