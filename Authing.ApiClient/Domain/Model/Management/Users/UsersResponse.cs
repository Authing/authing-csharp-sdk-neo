﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Authing.ApiClient.Domain.Model;

namespace Authing.ApiClient.Domain.Model.Management.Users
{
    //public class User
    //{
    //    /// <summary>
    //    /// 用户 ID
    //    /// </summary>
    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    [JsonProperty("arn")]
    //    public string Arn { get; set; }

    //    /// <summary>
    //    /// 用户在组织机构中的状态
    //    /// </summary>
    //    // TODO: need fix
    //    // [JsonProperty("status")]
    //    // public UserStatus? Status { get; set; }

    //    /// <summary>
    //    /// 用户池 ID
    //    /// </summary>
    //    [JsonProperty("userPoolId")]
    //    public string UserPoolId { get; set; }

    //    /// <summary>
    //    /// 用户名，用户池内唯一
    //    /// </summary>
    //    [JsonProperty("username")]
    //    public string Username { get; set; }

    //    /// <summary>
    //    /// 邮箱，用户池内唯一
    //    /// </summary>
    //    [JsonProperty("email")]
    //    public string Email { get; set; }

    //    /// <summary>
    //    /// 邮箱是否已验证
    //    /// </summary>
    //    [JsonProperty("emailVerified")]
    //    public bool? EmailVerified { get; set; }

    //    /// <summary>
    //    /// 手机号，用户池内唯一
    //    /// </summary>
    //    [JsonProperty("phone")]
    //    public string Phone { get; set; }

    //    /// <summary>
    //    /// 手机号是否已验证
    //    /// </summary>
    //    [JsonProperty("phoneVerified")]
    //    public bool? PhoneVerified { get; set; }

    //    [JsonProperty("unionid")]
    //    public string Unionid { get; set; }

    //    [JsonProperty("openid")]
    //    public string Openid { get; set; }

    //    /// <summary>
    //    /// 用户的身份信息
    //    /// </summary>
    //    // TODO: need fix
    //    // [JsonProperty("identities")]
    //    // public IEnumerable<Identity> Identities { get; set; }

    //    /// <summary>
    //    /// 昵称，该字段不唯一。
    //    /// </summary>
    //    [JsonProperty("nickname")]
    //    public string Nickname { get; set; }

    //    /// <summary>
    //    /// 注册方式
    //    /// </summary>
    //    [JsonProperty("registerSource")]
    //    public IEnumerable<string> RegisterSource { get; set; }

    //    /// <summary>
    //    /// 头像链接，默认为 https://usercontents.authing.cn/authing-avatar.png
    //    /// </summary>
    //    [JsonProperty("photo")]
    //    public string Photo { get; set; }

    //    /// <summary>
    //    /// 用户密码，数据库使用密钥加 salt 进行加密，非原文密码。
    //    /// </summary>
    //    [JsonProperty("password")]
    //    public string Password { get; set; }

    //    /// <summary>
    //    /// 用户社会化登录第三方身份提供商返回的原始用户信息，非社会化登录方式注册的用户此字段为空。
    //    /// </summary>
    //    [JsonProperty("oauth")]
    //    public string Oauth { get; set; }

    //    /// <summary>
    //    /// 用户登录凭证，开发者可以在后端检验该 token 的合法性，从而验证用户身份。详细文档请见：[验证 Token](https://docs.authing.co/advanced/verify-jwt-token.html)
    //    /// </summary>
    //    [JsonProperty("token")]
    //    public string Token { get; set; }

    //    /// <summary>
    //    /// token 过期时间
    //    /// </summary>
    //    [JsonProperty("tokenExpiredAt")]
    //    public string TokenExpiredAt { get; set; }

    //    /// <summary>
    //    /// 用户登录总次数
    //    /// </summary>
    //    [JsonProperty("loginsCount")]
    //    public int? LoginsCount { get; set; }

    //    /// <summary>
    //    /// 用户最近一次登录时间
    //    /// </summary>
    //    [JsonProperty("lastLogin")]
    //    public string LastLogin { get; set; }

    //    /// <summary>
    //    /// 用户上一次登录时使用的 IP
    //    /// </summary>
    //    [JsonProperty("lastIP")]
    //    public string LastIp { get; set; }

    //    /// <summary>
    //    /// 用户注册时间
    //    /// </summary>
    //    [JsonProperty("signedUp")]
    //    public string SignedUp { get; set; }

    //    /// <summary>
    //    /// 该账号是否被禁用
    //    /// </summary>
    //    [JsonProperty("blocked")]
    //    public bool? Blocked { get; set; }

    //    /// <summary>
    //    /// 账号是否被软删除
    //    /// </summary>
    //    [JsonProperty("isDeleted")]
    //    public bool? IsDeleted { get; set; }

    //    [JsonProperty("device")]
    //    public string Device { get; set; }

    //    [JsonProperty("browser")]
    //    public string Browser { get; set; }

    //    [JsonProperty("company")]
    //    public string Company { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("givenName")]
    //    public string GivenName { get; set; }

    //    [JsonProperty("familyName")]
    //    public string FamilyName { get; set; }

    //    [JsonProperty("middleName")]
    //    public string MiddleName { get; set; }

    //    [JsonProperty("profile")]
    //    public string Profile { get; set; }

    //    [JsonProperty("preferredUsername")]
    //    public string PreferredUsername { get; set; }

    //    [JsonProperty("website")]
    //    public string Website { get; set; }

    //    [JsonProperty("gender")]
    //    public string Gender { get; set; }

    //    [JsonProperty("birthdate")]
    //    public string Birthdate { get; set; }

    //    [JsonProperty("zoneinfo")]
    //    public string Zoneinfo { get; set; }

    //    [JsonProperty("locale")]
    //    public string Locale { get; set; }

    //    [JsonProperty("address")]
    //    public string Address { get; set; }

    //    [JsonProperty("formatted")]
    //    public string Formatted { get; set; }

    //    [JsonProperty("streetAddress")]
    //    public string StreetAddress { get; set; }

    //    [JsonProperty("locality")]
    //    public string Locality { get; set; }

    //    [JsonProperty("region")]
    //    public string Region { get; set; }

    //    [JsonProperty("postalCode")]
    //    public string PostalCode { get; set; }

    //    [JsonProperty("city")]
    //    public string City { get; set; }

    //    [JsonProperty("province")]
    //    public string Province { get; set; }

    //    [JsonProperty("country")]
    //    public string Country { get; set; }

    //    [JsonProperty("createdAt")]
    //    public string CreatedAt { get; set; }

    //    [JsonProperty("updatedAt")]
    //    public string UpdatedAt { get; set; }

    //    /// <summary>
    //    /// 用户所在的角色列表
    //    /// </summary>
    //    // TODO: needfix
    //    // [JsonProperty("roles")]
    //    // public PaginatedRoles Roles { get; set; }

    //    /// <summary>
    //    /// 用户所在的分组列表
    //    /// </summary>
    //    // TODO: needfix
    //    // [JsonProperty("groups")]
    //    // public PaginatedGroups Groups { get; set; }

    //    /// <summary>
    //    /// 用户所在的部门列表
    //    /// </summary>
    //    // TODO: needfix
    //    // [JsonProperty("departments")]
    //    // public PaginatedDepartments Departments { get; set; }

    //    /// <summary>
    //    /// 被授权访问的所有资源
    //    /// </summary>
    //    // TODO: needfix
    //    // [JsonProperty("authorizedResources")]
    //    // public PaginatedAuthorizedResources AuthorizedResources { get; set; }

    //    /// <summary>
    //    /// 用户外部 ID
    //    /// </summary>
    //    [JsonProperty("externalId")]
    //    public string ExternalId { get; set; }

    //    /// <summary>
    //    /// 用户自定义数据
    //    /// </summary>
    //    // TODO: needfix
    //    // [JsonProperty("customData")]
    //    // public IEnumerable<UserCustomData> CustomData { get; set; }
    //}

    //#region PaginatedUsers
    //public class PaginatedUsers
    //{
    //    #region members
    //    [JsonProperty("totalCount")]
    //    public int TotalCount { get; set; }

    //    [JsonProperty("list")]
    //    public IEnumerable<User> List { get; set; }
    //    #endregion
    //}
    //#endregion

    //public class UserResponse
    //{

    //    [JsonProperty("user")]
    //    public User Result { get; set; }
    //}

    //#region CommonMessage
    //public class CommonMessage
    //{
    //    #region members
    //    /// <summary>
    //    /// 可读的接口响应说明，请以业务状态码 code 作为判断业务是否成功的标志
    //    /// </summary>
    //    [JsonProperty("message")]
    //    public string Message { get; set; }

    //    /// <summary>
    //    /// 业务状态码（与 HTTP 响应码不同），但且仅当为 200 的时候表示操作成功表示，详细说明请见：
    //    /// [Authing 错误代码列表](https://docs.authing.co/advanced/error-code.html)
    //    /// </summary>
    //    [JsonProperty("code")]
    //    public int? Code { get; set; }
    //    #endregion
    //}
    //#endregion

    public class UsersResponse
    {

        [JsonProperty("users")]
        public PaginatedUsers Result { get; set; }
    }

    //public class UserParam
    //{

    //    /// <summary>
    //    /// Optional
    //    /// </summary>
    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    public UserParam()
    //    {

    //    }
    //    /// <summary>
    //    /// UserParam.Request 
    //    /// <para>Required variables:<br/> {  }</para>
    //    /// <para>Optional variables:<br/> { id=(string) }</para>
    //    /// </summary>
    //    public GraphQLRequest CreateRequest()
    //    {
    //        return new GraphQLRequest
    //        {
    //            Query = UserDocument,
    //            OperationName = "user",
    //            Variables = this
    //        };
    //    }


    //    public static string UserDocument = @"
    //    query user($id: String) {
    //      user(id: $id) {
    //        id
    //        arn
    //        userPoolId
    //        status
    //        username
    //        email
    //        emailVerified
    //        phone
    //        phoneVerified
    //        identities {
    //          openid
    //          userIdInIdp
    //          userId
    //          connectionId
    //          isSocial
    //          provider
    //          userPoolId
    //        }
    //        unionid
    //        openid
    //        nickname
    //        registerSource
    //        photo
    //        password
    //        oauth
    //        token
    //        tokenExpiredAt
    //        loginsCount
    //        lastLogin
    //        lastIP
    //        signedUp
    //        blocked
    //        isDeleted
    //        device
    //        browser
    //        company
    //        name
    //        givenName
    //        familyName
    //        middleName
    //        profile
    //        preferredUsername
    //        website
    //        gender
    //        birthdate
    //        zoneinfo
    //        locale
    //        address
    //        formatted
    //        streetAddress
    //        locality
    //        region
    //        postalCode
    //        city
    //        province
    //        country
    //        createdAt
    //        updatedAt
    //        externalId
    //      }
    //    }
    //    ";
    //}
}
