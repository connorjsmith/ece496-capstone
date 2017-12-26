using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillDispenserWeb.Controllers
{
    public class TwilioController : Controller
    {
        
        // GET: /<controller>/
        [Route("/twilio/")]
        public IActionResult Index()
        {
            //Our Account Sid and Auth Token at twilio.com/console
            const string accountSid = "AC36c72ac486918a5bb537d9e47d0051e0";
            const string authToken = "f3848665dd4cf439fcf7f8cb3b8b4706";
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+16478865218");//Shreyas's phone number for testing
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+12898064098"),//Assigned number
                body: "It works! - Shreyas");

            Console.WriteLine(message.Sid);
            return View("/Views/Twilio/Index.cshtml"); //Temporary for testing/learning purposes
        }

    }
}
