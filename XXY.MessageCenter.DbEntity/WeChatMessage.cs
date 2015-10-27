using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {

    /// <summary>
    /// 微信消息
    /// </summary>
    public class WeChatMessage : BaseMessage {

        public WeChatMessage()
            : base(Enums.MsgTypes.WeChat) {

        }

    }
}
