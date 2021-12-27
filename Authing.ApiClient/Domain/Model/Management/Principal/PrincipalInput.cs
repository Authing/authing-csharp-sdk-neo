namespace Authing.ApiClient.Domain.Model.Management.Principal
{
    public class PrincipalInput
    {
        public string Type { get; set; } = "p";
        public string Name { get; set; }
        public string IdCard { get; set; }
        public string BankCard { get; set; }
        public string EnterpriseName { get; set; }
        public string EnterpriseCode { get; set; }
        public string LegalPersonName { get; set; }
    }
}