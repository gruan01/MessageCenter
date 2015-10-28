using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.IService;
using XXY.Common.Attributes;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.ServiceImpl {

    [AutoInjection(typeof(ITemplateService))]
    public class TemplateServiceImpl : ITemplateService {

        [Dependency]
        public Lazy<ITemplate> TemplateBiz {
            get;
            set;
        }

        public Task<string> GetByCode(string code, string appCode, MsgTypes msgType, Langs? lang = null) {
            var template = this.TemplateBiz.Value.GetByCode(code, appCode, msgType, lang);
            if (template != null)
                return Task.FromResult(template.Ctx);
            else
                return Task.FromResult("");
        }

        public Task<IEnumerable<Template>> GetTemplates(string code, string appCode, MsgTypes? msgType = null, Langs? lang = null) {
            var cond = new TemplateSearchCondition() {
                AllowPage = false,
                AppCode = appCode,
                Code = code,
                Lang = lang,
                MsgType = msgType
            };

            var datas = this.TemplateBiz.Value.Search(cond);
            return Task.FromResult(datas);
        }
    }
}
