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
    public abstract class BaseMessage : BaseEntity {

        [DataMember]
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


        [DataMember]
        public Priorities PRI {
            get;
            set;
        }

        [DataMember]
        public string Ctx {
            get;
            set;
        }


        [DataMember]
        public string Receiver {
            get;
            set;
        }

    }
}
