﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.DbEntity {
    public abstract class BaseEntity {

        [Key, Column("ID", Order = 0)]
        public int ID {
            get;
            set;
        }


        
        public DateTime CreateOn {
            get;
            set;
        }

        [StringLength(20)]
        public string CreateByUserName {
            get;
            set;
        }

        public decimal CreateByUserID {
            get;
            set;
        }

        public DateTime? ModifyOn {
            get;
            set;
        }

        [StringLength(20)]
        public string ModifyByUserName {
            get;
            set;
        }

        public decimal? ModifyByUserID {
            get;
            set;
        }

        public bool IsDeleted {
            get;
            set;
        }
    }
}