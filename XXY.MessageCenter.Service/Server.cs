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

        private QueueHolder<EMailMessage> Holder = new QueueHolder<EMailMessage>(QueuePath);

        [ImportMany]
        public IEnumerable<Lazy<IMessageClient>> Clients {
            get;
            set;
        }

        public bool Start(HostControl hostControl) {
            this.Holder.OnDataReceived += Holder_OnDataReceived;
            this.Holder.Listen();
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
                    var client = this.Clients.FirstOrDefault(c => c.Value.AcceptMessageType.Equals(msg.GetType()));
                    if (client != null) {
                        try {
                            await client.Value.Send(msg);
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
