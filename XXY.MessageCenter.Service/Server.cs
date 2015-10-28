using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.Msmq;

namespace XXY.MessageCenter.Service {

    public class Server : ServiceControl {

        public static readonly string QueuePath = @"FormatName:DIRECT=OS:xling\Private$\XXY.Mail";

        private QueueHolder<BaseEntity> Holder = new QueueHolder<BaseEntity>(QueuePath);

        [ImportMany]
        public IEnumerable<Lazy<IMessageClient, IMessageClientMetadata>> Clients {
            get;
            set;
        }

        public bool Start(HostControl hostControl) {
            this.Holder.OnDataReceived += Holder_OnDataReceived;
            this.Holder.Listen(typeof(BaseEntity));
            return true;
        }

        public bool Stop(HostControl hostControl) {
            this.Holder.OnDataReceived -= Holder_OnDataReceived;
            return true;
        }

        async void Holder_OnDataReceived(object sender, DataReceivedArgs e) {
            if (e.Data != null) {
                var msg = (BaseMessage)e.Data;
                if (msg != null) {
                    var client = this.Clients.FirstOrDefault(c => c.Metadata.MsgType == msg.MsgType);
                    if (client != null) {
                        //await client.Value.Send(msg);
                        dynamic c = client.Value;
                        try {
                            await c.Send(msg);
                        } catch (Exception ex) {

                        }
                    }
                }
            }
        }
    }
}
