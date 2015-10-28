using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.IService {


    [ServiceKnownType(typeof(WeChatMessage))]
    [ServiceKnownType(typeof(QQMessage))]
    [ServiceKnownType(typeof(SMSMessage))]
    [ServiceKnownType(typeof(TxtMessage))]
    [ServiceKnownType(typeof(EMailMessage))]
    [ServiceContract]
    public interface IQueueService {

        [OperationContract]
        Task Put(BaseMessage msg);

    }
}
