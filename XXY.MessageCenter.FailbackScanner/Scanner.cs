using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XXY.Common.Attributes;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.Queue;

namespace XXY.MessageCenter.ProcessedScanner {


    [Synchronization(SynchronizationAttribute.REQUIRED, true)]
    public class Scanner {
        private static QueueHolder Holder = null;

        /// <summary>
        /// 最大临时保存数
        /// </summary>
        private static int MaxStoreCount = 1000;

        /// <summary>
        /// 最大临时保存时间
        /// </summary>
        private static TimeSpan MaxStoreTime = TimeSpan.FromMinutes(2);

        public static Lazy<Scanner> Instance = new Lazy<Scanner>(() => {
            return new Scanner();
        });


        /// <summary>
        /// 未保存的数据
        /// </summary>
        private List<ProcessedMsg> StoredList = new List<ProcessedMsg>();

        /// <summary>
        /// 最后一次保存时间
        /// </summary>
        private DateTime LastSaveTime = DateTime.Now;

        private IQueue QueieBiz = null;

        static Scanner() {
            var config = ServiceLocator.Current.GetInstance<IConfig>();
            Holder = new QueueHolder(config.ProcessedMessageMSMQPath, typeof(ProcessedMsg));
        }

        private Scanner() {
        }

        public void Start() {
            Holder.OnDataReceived += holder_OnDataReceived;
            Holder.Listen();
        }

        public async void Stop() {
            Holder.OnDataReceived -= holder_OnDataReceived;
            await this.Save();
        }

        async void holder_OnDataReceived(object sender, DataReceivedArgs e) {
            var data = (ProcessedMsg)e.Data;
            this.StoredList.Add(data);
            await this.DetectSave();
        }

        private async Task DetectSave() {
            if (this.StoredList.Count >= MaxStoreCount || (DateTime.Now - this.LastSaveTime) >= MaxStoreTime) {
                await this.Save();
            }
        }


        //[MethodImplAttribute(MethodImplOptions.Synchronized)]
        private async Task Save() {

            var biz = ServiceLocator.Current.GetInstance<IQueue>();
            await biz.Update(this.StoredList);

            this.StoredList.Clear();
            this.LastSaveTime = DateTime.Now;
        }
    }
}
