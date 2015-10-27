using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.UC.BizEntity;

namespace XXY.MessageCenter.Biz {
    public interface ICurrentUser {
        User GetUser();

        bool IsLogined {
            get;
        }
    }
}
