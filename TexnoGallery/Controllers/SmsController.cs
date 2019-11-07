using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;

namespace TexnoGallery.Controllers
{
    public class SmsController : TwilioController
    {
        public ActionResult SendSms(SmsRequest incomingMessage)
        {
            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            var authToken= ConfigurationManager.AppSettings["TwilioAuthToken"];
            TwilioClient.Init(accountSid, authToken);
            var from = new PhoneNumber("+1 831 888 4874");
            var to = new PhoneNumber("+994558744364");
            var message = MessageResource.Create(
                to:to,
                from:from,
                body:"Test Texno Gallery"
                );
            return Content(message.Sid);
        }

        [HttpPost]
        public ActionResult ReceiveSms(SmsRequest request)
        {
           
            return Content(request.Body);
        }
    }
}