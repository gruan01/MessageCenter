using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using XXY.Common.Attributes;
using XXY.MessageCenter.IBiz;

namespace XXY.MessageCenter {

    [AutoInjection(typeof(IConfig))]
    public class Config : IConfig {
        public string MessageMSMQPath {
            get {
                return ConfigurationManager.AppSettings.Get("MessageMSMQPath");
            }
        }


        public string FailedMessageMSMQPath {
            get {
                return ConfigurationManager.AppSettings.Get("FailedMessageMSMQPath");
            }
        }
    }
}