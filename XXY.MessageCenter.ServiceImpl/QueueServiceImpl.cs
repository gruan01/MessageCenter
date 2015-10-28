using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.IService;

namespace XXY.MessageCenter.ServiceImpl {

    [AutoInjection(typeof(IQueueService))]
    public class QueueServiceImpl : IQueueService {

        [Dependency]
        public Lazy<IQueue> QueueBiz {
            get;
            set;
        }


        public async Task Put(BaseMessage msg) {
            await this.QueueBiz.Value.Put(msg);
        }
    }
}
