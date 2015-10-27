using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.DbContext {
    public class Entities : System.Data.Entity.DbContext {

        public Entities()
            : base("Entities") {

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("MAIL");

            modelBuilder.Conventions.Add(new OracleConversion());
            base.OnModelCreating(modelBuilder);
        }






        public DbSet<Template> Templates {
            get;
            set;
        }

        public DbSet<EMailMessage> EmailMessages {
            get;
            set;
        }

        public DbSet<QQMessage> QqMessages {
            get;
            set;
        }

        public DbSet<SMSMessage> SmsMessages {
            get;
            set;
        }

        public DbSet<WeChatMessage> WeChatMessages {
            get;
            set;
        }
    }
}
