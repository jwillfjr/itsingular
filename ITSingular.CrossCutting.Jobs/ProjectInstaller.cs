using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace ITSingular.CrossCutting.Jobs
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            serviceProcessInstaller1.Username = null;
            serviceProcessInstaller1.Password = null;

            serviceInstaller1.Description = "Send machine name";
            serviceInstaller1.DisplayName = "ITSingular - Send Machine Name";
            serviceInstaller1.ServiceName = "ITSingularSendMachineName";
        }
    }
}
