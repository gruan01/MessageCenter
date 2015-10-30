using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.Queue;

namespace XXY.MessageCenter.Service {

    public class Server : ServiceControl {

        private QueueHolder Holder = null;
        private QueueHolder FailbackHolder = null;


        [ImportMany]
        public IEnumerable<Lazy<IMessageClient>> Clients {
            get;
            set;
        }

        public Server(string queuePath, string failbackQueuePath, IEnumerable<Type> supportDataTypes) {
            this.Holder = new QueueHolder(queuePath, supportDataTypes);
            this.FailbackHolder = new QueueHolder(failbackQueuePath, typeof(Tuple<MsgTypes, int, string>));
        }

        public bool Start(HostControl hostControl) {
            foreach (var c in this.Clients) {
                c.Value.OnFailback += OnFailback;
            }
            this.Holder.OnDataReceived += Holder_OnDataReceived;
            this.Holder.Listen();
            return true;
        }


        void OnFailback(object sender, FailbackArgs e) {
            try {
                this.FailbackHolder.Put(new Tuple<MsgTypes, int, string>(e.MsgType, e.ID, e.Exception.Message));
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Stop(HostControl hostControl) {
            foreach (var c in this.Clients) {
                c.Value.OnFailback -= OnFailback;
            }
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
