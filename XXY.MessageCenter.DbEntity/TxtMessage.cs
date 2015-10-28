using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {

    /// <summary>
    /// 文本消息
    /// </summary>
    public class TxtMessage : BaseMessage {

        public TxtMessage()
            : base(Enums.MsgTypes.Txt, true) {
        }


        [DataMember]
        [Required, StringLength(100)]
        public string Subject {
            get;
            set;
        }

        public bool Readed {
            get;
            set;
        }

        [DataMember]
        [Required, StringLength(20)]
        public string Sender {
            get;
            set;
        }

        public decimal SenderID {
            get;
            set;
        }

        [DataMember]
        public decimal ReceiverID {
            get;
            set;
        }
    }
}
