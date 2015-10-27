using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using System.ComponentModel.Composition;
using XXY.MessageCenter.Common;


namespace XXY.MessageCenter.Service {
    class Program {
        static void Main(string[] args) {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            HostFactory.Run((x) => {
                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
                x.SetDescription("XXY Message Center Service");
                x.SetDisplayName("XXY.MessageCenter");
                x.SetInstanceName("XXY.MessageCenter");
                x.SetServiceName("XXY.MessageCenter");

                x.Service(s => {
                    var server = new Server();
                    var catalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
                    var container = new CompositionContainer(catalog);
                    container.ComposeParts(server);
                    return server;
                });
            });
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            var ex = (Exception)e.ExceptionObject;
        }

    }
}
