using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {

    /// <summary>
    /// QQ
    /// </summary>

    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    public class QQMessage : BaseMessage {

        public QQMessage()
            : base(Enums.MsgTypes.QQ) {
        }

    }
}
