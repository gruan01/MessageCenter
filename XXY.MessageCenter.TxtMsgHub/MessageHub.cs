using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;
using XXY.Common.Extends;

namespace XXY.MessageCenter.TxtMsgHub {

    public class MessageHub : Hub {

        public void UnReadCount() {
            var provider = (IUserIdProvider)GlobalHost.DependencyResolver.GetService(typeof(IUserIdProvider));
            var receiverID = provider.GetUserId(Context.Request).ToDouble(0);
            if (receiverID > 0) {
                
            }
        }

    }

}
