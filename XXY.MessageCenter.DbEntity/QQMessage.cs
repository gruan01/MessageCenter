using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {

    /// <summary>
    /// QQ
    /// </summary>
    public class QQMessage : BaseMessage {

        public QQMessage()
            : base(Enums.MsgTypes.QQ) {
        }

    }
}
