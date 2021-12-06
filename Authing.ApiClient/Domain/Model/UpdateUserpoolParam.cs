using Authing.ApiClient.Infrastructure.GraphQL;
using Newtonsoft.Json;

namespace Authing.ApiClient.Domain.Model
{
    public class UpdateUserpoolParam
    {

        /// <summary>
        /// Required
        /// </summary>
        [JsonProperty("input")]
        public UpdateUserpoolInput Input { get; set; }

        public UpdateUserpoolParam(UpdateUserpoolInput input)
        {
            this.Input = input;
        }

        /// <summary>
        /// UpdateUserpoolParam.Request 
        /// <para>Required variables:<br/> { input=(UpdateUserpoolInput) }</para>
        /// <para>Optional variables:<br/> {  }</para>
        /// </summary>
        public GraphQLRequest CreateRequest()
        {
            return new GraphQLRequest
            {
                Query = UpdateUserpoolDocument,
                OperationName = "updateUserpool",
                Variables = this
            };
        }


        public static string UpdateUserpoolDocument = @"
        mutation updateUserpool($input: UpdateUserpoolInput!) {
          updateUserpool(input: $input) {
            id
            name
            domain
            description
            secret
            jwtSecret
            userpoolTypes {
              code
              name
              description
              image
              sdks
            }
            logo
            createdAt
            updatedAt
            emailVerifiedDefault
            sendWelcomeEmail
            registerDisabled
            appSsoEnabled
            showWxQRCodeWhenRegisterDisabled
            allowedOrigins
            tokenExpiresAfter
            isDeleted
            frequentRegisterCheck {
              timeInterval
              limit
              enabled
            }
            loginFailCheck {
              timeInterval
              limit
              enabled
            }
            loginFailStrategy
            loginPasswordFailCheck {
              timeInterval
              limit
              enabled
            }
            changePhoneStrategy {
              verifyOldPhone
            }
            changeEmailStrategy {
              verifyOldEmail
            }
            qrcodeLoginStrategy {
              qrcodeExpiresAfter
              returnFullUserInfo
              allowExchangeUserInfoFromBrowser
              ticketExpiresAfter
            }
            app2WxappLoginStrategy {
              ticketExpriresAfter
              ticketExchangeUserInfoNeedSecret
            }
            whitelist {
              phoneEnabled
              emailEnabled
              usernameEnabled
            }
            customSMSProvider {
              enabled
              provider
              config
            }
            packageType
            useCustomUserStore
            loginRequireEmailVerified
            verifyCodeLength
          }
        }
        ";
    }
}