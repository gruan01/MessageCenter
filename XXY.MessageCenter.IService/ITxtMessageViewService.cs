using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.ServiceImpl {
    public interface ITxtMessageViewService {

        Task<int> GetUnReadCount(double receiverID);

        Task<TxtMessage> GetMessage(int msgID, double receiverID);

        Task<bool> SetReaded(int msgID);
    }
}
