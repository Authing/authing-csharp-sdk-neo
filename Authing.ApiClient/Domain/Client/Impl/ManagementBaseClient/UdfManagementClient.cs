using Authing.ApiClient.Domain.Model;
using Authing.ApiClient.Domain.Model.Management.Udf;
using Authing.ApiClient.Domain.Utils;
using Authing.ApiClient.Infrastructure.GraphQL;
using Authing.ApiClient.Interfaces.ManagementClient;
using Authing.ApiClient.Types;
using Authing.Library.Domain.Client.Impl;
using Authing.Library.Domain.Model.Exceptions;
using System;
using System.Collections.Generic;


using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authing.ApiClient.Domain.Client.Impl.ManagementBaseClient
{

    public class UdfManagementClient : IUdfManagementClient
    {
        private readonly ManagementClient client;

        public UdfManagementClient(ManagementClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// 设置自定义字段元数据，如果字段不存在则会创建，存在会更新
        /// </summary>
        /// <param name="type">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
        /// <param name="key">字段 key</param>
        /// <param name="dataType">数据类型，目前共支持五种数据类型。STRING 为字符串、NUMBER 为数字、DATETIME 为日期、BOOLEAN 为 boolean 值、OBJECT 为对象。</param>
        /// <param name="label">字段 Label，一般是一个 Human Readable 字符串。</param>
        /// <returns></returns>
        public async Task<UserDefinedField> Set(UdfTargetType type,
                                                string key,
                                                UdfDataType dataType,
                                                string label,
                                                AuthingErrorBox authingErrorBox = null)
        {
            var param = new SetUdfParam(type, key, dataType, label);

            var res = await client.RequestCustomDataWithToken<SetUdfResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="type">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
        /// <param name="key">字段 key</param>
        /// <returns></returns>
        public async Task<CommonMessage> Remove(UdfTargetType type, string key,AuthingErrorBox authingErrorBox=null)
        {
            var param = new RemoveUdfParam(type, key);

            var res = await client.RequestCustomDataWithToken<RemoveUdfResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取自定义字段列表
        /// </summary>
        /// <param name="type">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserDefinedField>> List(UdfTargetType type,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UdfParam(type);

            var res = await client.RequestCustomDataWithToken<UdfResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return res.Data?.Result;
        }

        /// <summary>
        /// 获取某一实体的自定义字段数据列表
        /// </summary>
        /// <param name="targetType">自定义字段目标类型， USER 表示用户、ROLE 表示角色。</param>
        /// <param name="targetId"> 自定义字段目标类型的主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<ResUdv>> ListUdv(UdfTargetType targetType, string targetId,AuthingErrorBox authingErrorBox=null)
        {
            var param = new UdvParam(targetType, targetId);

            var res = await client.RequestCustomDataWithToken<UdvResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return AuthingUtils.ConvertUdv(res.Data?.Result);
        }

        /// <summary>
        /// 批量添加自定义数据
        /// </summary>
        /// <param name="udfTargetType">自定义字段目标类型，USER 表示用户、ROLE 表示角色。</param>
        /// <param name="targetId"> 自定义字段目标类型的主键</param>
        /// <param name="udvList">自定义数据键值对集合</param>
        /// <returns></returns>
        public async Task<IEnumerable<ResUdv>> SetUdvBatch(UdfTargetType udfTargetType, string targetId, KeyValueDictionary udvList,AuthingErrorBox authingErrorBox=null)
        {
            var _udvList = new List<UserDefinedDataInput>();
            udvList.ToList().ForEach(udv => _udvList.Add(new UserDefinedDataInput(udv.Key)
            {
                Value = udv.Value
            }
            ));

            var param = new SetUdvBatchParam(udfTargetType, targetId)
            {
                UdvList = _udvList
            };

            var res = await client.RequestCustomDataWithToken<SetUdvBatchResponse>(param.CreateRequest()).ConfigureAwait(false);
            ErrorHelper.LoadError(res, authingErrorBox);
            return AuthingUtils.ConvertUdv(res.Data?.Result);
        }
    }


}
