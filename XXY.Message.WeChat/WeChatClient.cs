using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Extends;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.WxApi;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Messages;
using XXY.WxApi.Methods;

namespace XXY.MessageCenter.WeChat {

    [Export(typeof(IMessageClient))]
    //[ExportMetadata("MsgType", MsgTypes.WeChat)]
    public class WeChatClient : BaseMessageClient, IMessageClient {

        public event EventHandler<FailbackArgs> OnFailback;

        static WeChatClient() {
            var cfg = ConfigurationHelper.GetSection<WeChatConfig>();
            var configs = new List<ApiConfig>() {
                new ApiConfig("xxy", cfg.AppID, cfg.SecretKey, cfg.AesKey , cfg.Token)
            };

            ApiClient.Init(configs);
        }

        public override void Init() {
            throw new NotImplementedException();
        }

        public Task Send(BaseMessage msg) {
            var tcs = new TaskCompletionSource<object>();
            var data = (WeChatMessage)msg;
            var api = ApiClient.GetInstance("xxy");

            //接口限制: 用户不先在WX上发起对话,就没办法使用该功能!
            //{"errcode":45015,"errmsg":"response out of time limit or subscription is canceled hint: [h6kfKa0794age8]"}
            var method = new MessageSend() {
                OpenID = data.Receiver,
                Message = new TextMessage() {
                    Content = data.Ctx
                }
            };
            var result = api.Execute(method);
            tcs.SetResult(result);
            return tcs.Task;
        }


        public Type AcceptMessageType {
            get {
                return typeof(WeChatMessage);
            }
        }
    }
}
