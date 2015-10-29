using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbContext;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.Common.Extends;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.Msmq;
using Microsoft.Practices.ServiceLocation;

namespace XXY.MessageCenter.Biz {

    public abstract class BaseMessageHandler : BaseBiz {

        public Lazy<IConfig> Config {
            get;
            set;
        }

        public BaseMessage Msg {
            get;
            set;
        }

        public BaseMessageHandler() {
            this.Config = ServiceLocator.Current.GetInstance<Lazy<IConfig>>();
        }

        public virtual Task<bool> Handle() {
            return Task.FromResult(true);
        }
    }









    public abstract class BaseMessageHandler<T> : BaseMessageHandler where T : BaseMessage {

        protected abstract Task<bool> Save();

        public async override Task<bool> Handle() {
            var holder = new QueueHolder<T>(this.Config.Value.MessageMSMQPath);
            this.SetCreateInfo(this.Msg);
            if (await this.Save())
                return holder.Put((T)this.Msg);
            else
                return false;
        }
    }








    public static class MessageHandlerFactory {

        public static BaseMessageHandler GetHandler(BaseMessage msg) {
            BaseMessageHandler handler = null;
            switch (msg.MsgType) {
                case MsgTypes.Email:
                    handler = new EmailMessageHandler();
                    break;
                case MsgTypes.Txt:
                    handler = new TxtMessageHandler();
                    break;
            }

            handler.Msg = msg;
            return handler;
        }
    }








    public class EmailMessageHandler : BaseMessageHandler<EMailMessage> {
        protected async override Task<bool> Save() {
            using (var db = new Entities()) {
                db.EmailMessages.Add((EMailMessage)this.Msg);
                this.Errors = db.GetErrors();
                if (!this.HasError) {
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
    }







    public class TxtMessageHandler : BaseMessageHandler<TxtMessage> {

        protected async override Task<bool> Save() {
            using (var db = new Entities()) {
                db.TxtMessages.Add((TxtMessage)this.Msg);
                this.Errors = db.GetErrors();
                if (!this.HasError) {
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public override async Task<bool> Handle() {
            return await this.Save();
        }
    }
}
