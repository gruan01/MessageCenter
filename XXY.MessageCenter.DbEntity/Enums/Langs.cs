using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity.Enums {
    
    [Flags]
    public enum Langs : byte {
        ZhCn = 1,
        EnUs = 2,
        ZhTw = 4
    }
}
