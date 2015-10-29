using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.DbEntity {

    [Serializable, DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
    public /*abstract*/ class BaseMessage : BaseEntity {

        [NotMapped]
        public MsgTypes MsgType {
            get;
            private set;
        }

        [NotMapped]
        public bool AllowHtml {
            get;
            set;
        }


        public BaseMessage(MsgTypes type, bool allowHtml = false) {
            this.MsgType = type;
            this.AllowHtml = allowHtml;
        }


        public Priorities PRI {
            get;
            set;
        }

        public string Ctx {
            get;
            set;
        }


        public string Receiver {
            get;
            set;
        }

    }
}
