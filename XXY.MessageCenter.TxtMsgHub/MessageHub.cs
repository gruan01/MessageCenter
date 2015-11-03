using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.TxtMsgHub {

    public class MessageHub : Hub {

        public void NewMsg(TxtMessage msg) {
            
        }

    }

}
