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

namespace XXY.MessageCenter.ServiceImpl {

    [AutoInjection(typeof(ITemplateService))]
    public class TemplateServiceImpl : ITemplateService {

        [Dependency]
        public Lazy<ITemplate> TemplateBiz {
            get;
            set;
        }

        public Task<string> GetByCode(string code, string appCode, Langs? lang = null) {
            var template = this.TemplateBiz.Value.GetByCode(code, appCode, lang);
            if (template != null)
                return Task.FromResult(template.Ctx);
            else
                return Task.FromResult("");
        }
    }
}
