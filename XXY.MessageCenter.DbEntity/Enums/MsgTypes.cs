using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {

    [Flags]
    public enum MsgTypes : byte {
        Email = 1,
        SMS = 2,
        WeChat = 4,
        QQ = 8,
        Txt = 16
    }
}
