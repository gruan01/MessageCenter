using AutoMapper;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Attributes;
using XXY.MessageCenter.BizEntity.Dtos;
using XXY.MessageCenter.DbContext;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.Msmq;

namespace XXY.MessageCenter.Biz {

    [AutoInjection(typeof(IQueue))]
    public class QueueBiz : BaseBiz, IQueue {

        public async Task<bool> Put<T>(T msg) where T : BaseMessage {
            var handler = MessageHandlerFactory.GetHandler(msg);
            if (handler != null) {
                return await handler.Handle();
            } else {
                throw new NotSupportedException(string.Format("目前不支持类型为 {0} 的消息", msg.MsgType));
            }
        }
    }
}
