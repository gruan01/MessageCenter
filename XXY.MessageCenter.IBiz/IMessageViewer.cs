using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.IBiz {
    public interface IMessageViewer {

        Task<IEnumerable<BaseMessage>> Search(MessageSearchCondition cond);

        Task<BaseMessage> Get(MsgTypes type, int id);

        Task<bool> Delete(MsgTypes type, int id);





        Task<TxtMessage> GetTxtMsg(int msgID, double receiverID);

        Task<IEnumerable<TxtMessage>> GetTxtMsg(double receiverID, Pager pager = null, bool onlyUnread = true);

        Task<int> GetUnReadTxtMsgCount(double receiverID);

        Task<bool> SetTxtMsgReaded(int msgID);
    }
}
