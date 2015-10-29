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
using XXY.MessageCenter.Queue;
using Microsoft.Practices.ServiceLocation;

namespace XXY.MessageCenter.Biz {

    public abstract class BaseMessageHandler : BaseBiz {

        public static readonly List<Type> SupportDataTypes = new List<Type>() {
            typeof(EMailMessage),
            typeof(SMSMessage),
            typeof(QQMessage),
            typeof(WeChatMessage)
        };

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

        protected async virtual Task<bool> Save() {
            using (var db = new Entities()) {
                db.Set<T>().Add((T)this.Msg);
                this.Errors = db.GetErrors();
                if (!this.HasError) {
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async override Task<bool> Handle() {
            var holder = new QueueHolder(this.Config.Value.MessageMSMQPath, SupportDataTypes);
            this.SetCreateInfo(this.Msg);
            if (await this.Save())
                return holder.Put((T)this.Msg, this.ConvertPriority(this.Msg.PRI));
            else
                return false;
        }

        private Queue.Priorities ConvertPriority(XXY.MessageCenter.DbEntity.Enums.Priorities pri) {
            switch (pri) {
                case DbEntity.Enums.Priorities.Normal:
                    return Queue.Priorities.Normal;
                case DbEntity.Enums.Priorities.Lower:
                    return Queue.Priorities.Lower;
                case DbEntity.Enums.Priorities.Immediately:
                    return Queue.Priorities.Immediately;
                case DbEntity.Enums.Priorities.Higher:
                    return Queue.Priorities.Higher;
                default:
                    return Queue.Priorities.Normal;
            }
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
                case MsgTypes.QQ:
                    handler = new QQMessageHandler();
                    break;
                case MsgTypes.SMS:
                    handler = new SmsMessageHandler();
                    break;
                case MsgTypes.WeChat:
                    handler = new WeChatMessageHandler();
                    break;
            }


            if (handler != null)
                handler.Msg = msg;
            return handler;
        }
    }








    public class EmailMessageHandler : BaseMessageHandler<EMailMessage> {
    }

    public class TxtMessageHandler : BaseMessageHandler<TxtMessage> {
        public override async Task<bool> Handle() {
            return await this.Save();
        }
    }

    public class SmsMessageHandler : BaseMessageHandler<SMSMessage> {
    }

    public class QQMessageHandler : BaseMessageHandler<QQMessage> {
    }

    public class WeChatMessageHandler : BaseMessageHandler<WeChatMessage> {
    }
}
