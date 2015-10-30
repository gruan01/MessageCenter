using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {

    [Flags]
    public enum MsgStatus : byte {
        New = 1,
        Processing = 2,
        Processed = 4,
        Failed = 8
    }
}
