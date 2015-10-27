using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.IService {
    [ServiceContract]
    public interface ITemplateService {

        /// <summary>
        /// 获取邮件模板
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lang"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        [OperationContract]
        Task<string> GetByCode(string code, string appCode, Langs? lang = null);

    }
}
