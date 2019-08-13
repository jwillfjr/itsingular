using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ITSingular.CrossCutting.Jobs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (0 == 0)
            {

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new SendMachineName()
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                var serv = new SendMachineName();

                serv.TestStartupAndStop(args);

            }

        }
    }
}
