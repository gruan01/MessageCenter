using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.DbEntity {
    public abstract class BaseMessage : BaseEntity {

        [NotMapped]
        public MsgTypes MsgType {
            get;
            set;
        }

        [NotMapped]
        public bool AllowHtml {
            get;
            set;
        }

        public BaseMessage(MsgTypes type, bool allowHtml = false) {
            this.MsgType = type;
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

        public Langs Lang {
            get;
            set;
        }
    }
}
