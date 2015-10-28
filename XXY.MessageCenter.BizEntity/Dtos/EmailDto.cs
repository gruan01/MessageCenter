using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.BizEntity.Dtos {
    public class EmailDto {

        public string Receiver {
            get;
            set;
        }

        public string Cc {
            get;
            set;
        }

        public string Bcc {
            get;
            set;
        }

        public string Subject {
            get;
            set;
        }

        public string Ctx {
            get;
            set;
        }

    }
}
