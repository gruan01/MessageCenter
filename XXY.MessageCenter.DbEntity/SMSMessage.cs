using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {

    /// <summary>
    /// 短信
    /// </summary>
    public class SMSMessage : BaseMessage {

        public SMSMessage()
            : base(Enums.MsgTypes.SMS) {
        }
    }
}
