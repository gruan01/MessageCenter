using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace XXY.MessageCenter.Msmq {
    public class QueueHolder<T> {

        public string QueuePath {
            get;
            private set;
        }

        public event EventHandler<DataReceivedArgs> OnDataReceived = null;

        public QueueHolder(string path) {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");


            this.QueuePath = path;
        }

        private MessageQueue GetQueue() {
            //远程队列无法确定是不是存在。
            //"无法确定具有指定格式名的队列是否存在,
            //只能保证这个队列一定存在。
            var queue = new MessageQueue(this.QueuePath);
            queue.Formatter = new JsonMessageFormater(typeof(T));
            return queue;
        }


        public bool Put(T data) {
            using (var trans = new MessageQueueTransaction())
            using (var mq = this.GetQueue()) {
                trans.Begin();
                try {
                    mq.Send(data, trans);
                    trans.Commit();
                    return true;
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    trans.Abort();
                    return false;
                }
            }
        }

        public void Listen() {
            var mq = this.GetQueue();
            mq.PeekCompleted += mq_PeekCompleted;
            mq.BeginPeek();
        }

        void mq_PeekCompleted(object sender, PeekCompletedEventArgs e) {
            var queue = (MessageQueue)sender;
            using (var transaction = new TransactionScope()) {
                var msg = queue.EndPeek(e.AsyncResult);
                if (this.OnDataReceived != null)
                    this.OnDataReceived(null, new DataReceivedArgs(msg.Body));

                queue.ReceiveById(e.Message.Id, MessageQueueTransactionType.Automatic);
                transaction.Complete();
            }
            queue.BeginPeek();
        }
    }

    public class DataReceivedArgs : EventArgs {
        public object Data {
            get;
            private set;
        }
        public DataReceivedArgs(object mail) {
            this.Data = mail;
        }
    }
}
