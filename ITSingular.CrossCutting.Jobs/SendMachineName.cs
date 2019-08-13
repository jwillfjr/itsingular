using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ITSingular.CrossCutting.Jobs
{
    public partial class SendMachineName : ServiceBase
    {
        public SendMachineName()
        {
            InitializeComponent();
        }

        bool started = false;

        protected override void OnStart(string[] args)
        {
            started = true;
            Send();
        }

        protected override void OnStop()
        {
            started = false;
        }

        public void Send()
        {
            try
            {
                var address = ConfigurationManager.AppSettings["api:url"];
                address = address + "/communication/setmachinestate";
                var machineName = string.Concat("\"", System.Environment.MachineName, "\"");
                
                var client = new RestClient(address);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("content-length", "6");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", machineName, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (started)
            {
                Task.Delay(TimeSpan.FromMinutes(1)).ContinueWith(c =>
                {
                    Send();
                });
            }
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
