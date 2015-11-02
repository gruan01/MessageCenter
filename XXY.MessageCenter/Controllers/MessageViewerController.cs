using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.Filters;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.Models;

namespace XXY.MessageCenter.Controllers {

    [CheckPower(false, Order = 10)]
    public class MessageViewerController : BaseController {

        [Dependency]
        public Lazy<IMessageViewer> Biz {
            get;
            set;
        }

        public ActionResult Index() {
            var cond = this.GetCondition<MessageSearchCondition>();
            if (cond != null) {
                return Index(cond);
            } else
                return View();
        }

        [HttpPost]
        public ActionResult Index(MessageSearchCondition condition) {
            var data = this.Biz.Value.Search(condition);
            var pdm = PDM.Create(data, condition);
            return View(pdm);
        }

        [Route("{type}/{id}")]
        public ActionResult Detail(MsgTypes type, int id) {
            var data = this.Biz.Value.Get(type, id);
            return View(data);
        }

        public async Task<ActionResult> Delete(MsgTypes type, int id, MessageSearchCondition condition) {
            await this.Biz.Value.Delete(type, id);
            this.SetCondition(condition);
            return RedirectToAction("Index");
        }
    }
}