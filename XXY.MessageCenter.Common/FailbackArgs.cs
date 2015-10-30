using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.Common {
    public class FailbackArgs : EventArgs {

        public MsgTypes MsgType {
            get;
            private set;
        }

        public int ID {
            get;
            private set;
        }

        public Exception Exception {
            get;
            private set;
        }

        public FailbackArgs(MsgTypes msgType, int id, Exception ex) {
            this.MsgType = msgType;
            this.ID = id;
            this.Exception = ex;
        }

    }
}
