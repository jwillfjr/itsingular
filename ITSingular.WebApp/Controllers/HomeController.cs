using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ITSingular.WebApp.Controllers
{
    public class HomeController : Controller
    {

        string apiurl = ConfigurationManager.AppSettings["api:url"] + "/communication";

        public ActionResult Index()
        {
            return View();
        }

        internal JsonResult GetApi(string method)
        {
            var client = new RestClient(string.Concat(apiurl, "/", method));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            IRestResponse response = client.Execute(request);

            return Json(response.Content);
        }

        [HttpPost]
        public JsonResult GetCountMachineOffline()
        {
            try
            {
                return GetApi("getcountmachineoffline");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetCountMachineOnline()
        {
            try
            {
                return GetApi("getcountmachineonline");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult GetCountMachineAlert()
        {
            try
            {
                return GetApi("getcountmachinealert");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult SendEmail(string email, string status, string count)
        {
            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["email:from"], "iT Singular");
                var toAddress = new MailAddress(email, email);
                string fromPassword = ConfigurationManager.AppSettings["email:password"].ToString();
                const string subject = "Subject";
                var statusDesc = "";
                switch (status)
                {
                    case "0":
                        statusDesc = "Off-line";
                        break;
                    case"1":
                        statusDesc = "Alert";
                        break;
                    case "2":
                        statusDesc = "On-line";
                        break;
                }

                string body = string.Format("Count: {0} Status: {1}", count, statusDesc);

                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["email:host"].ToString(),
                    Port = int.Parse(ConfigurationManager.AppSettings["email:port"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return Json("Submit successfully!");
            }
            catch (Exception)
            {
                throw;
            }


        }

    }
}