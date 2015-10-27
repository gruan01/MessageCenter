using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.DbEntity {
    public class Template : BaseEntity {


        [Required]
        [Index]
        [StringLength(30)]
        public string Code {
            get;
            set;
        }

        [Index]
        public Langs Lang {
            get;
            set;
        }

        [Required]
        [Index, StringLength(30)]
        public string AppCode {
            get;
            set;
        }

        public bool IsDefault {
            get;
            set;
        }

        [StringLength(200), Required]
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
