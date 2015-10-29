using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.WeChat {

    [Export(typeof(IMessageClient))]
    //[ExportMetadata("MsgType", MsgTypes.WeChat)]
    public class WeChatClient : BaseMessageClient, IMessageClient {

        public override void Init() {
            throw new NotImplementedException();
        }

        public Task Send(BaseMessage msg) {
            throw new NotImplementedException();
        }


        public Type AcceptMessageType {
            get {
                return typeof(WeChatMessage);
            }
        }
    }
}
