using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.Common.Attributes;
using XXY.MessageCenter.IBiz;
using XXY.UC.BizEntity;

namespace XXY.MessageCenter {
    /// <summary>
    /// 
    /// </summary>
    [AutoInjection(typeof(ICurrentUser))]
    public class CurrentUserBiz : ICurrentUser {

        private User DefaultUser = new User() {
            CompanyCode = "System",
            CompanyNameCN = "System",
            CompanyNameEn = "System",
            FullName = "System",
            RootCompanyNameCN = "System",
            RootCompanyNameEn = "System",
            UserName = "System"
        };

        /// <summary>
        /// 获取当前登陆的用户, 如果没有登陆, 返回一个默认的值, 用以避免 null 判断
        /// </summary>
        /// <returns></returns>
        public User GetUser() {
            var user = SessionHelper.Get<User>(SessionKeys.User.ToString());
            if (user == null)
                user = this.DefaultUser;
            return user;
        }

        public bool IsLogined {
            get {
                return SessionHelper.Get<User>(SessionKeys.User.ToString()) != null;
            }
        }
    }
}
