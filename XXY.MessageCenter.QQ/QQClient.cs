using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.QQ {

    [Export(typeof(IMessageClient))]
    [ExportMetadata("MsgType", MsgTypes.QQ)]
    public class QQClient : BaseMessageClient, IMessageClient<QQMessage> {
        public Task Send(QQMessage msg) {
            throw new NotImplementedException();
        }

        public override void Init() {
            throw new NotImplementedException();
        }
    }
}
