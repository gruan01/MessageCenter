using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.IBiz {
    public interface IMessageViewer {

        IEnumerable<BaseMessage> Search(MessageSearchCondition cond);

        BaseMessage Get(MsgTypes type, int id);

        Task<bool> Delete(MsgTypes type, int id);
    }
}
