using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.Sms {

    [Export(typeof(IMessageClient))]
    [ExportMetadata("MsgType", MsgTypes.SMS)]
    public class SMSClient : BaseMessageClient, IMessageClient<SMSMessage> {
        public Task Send(SMSMessage msg) {
            throw new NotImplementedException();
        }

        public override void Init() {
            throw new NotImplementedException();
        }
    }
}
