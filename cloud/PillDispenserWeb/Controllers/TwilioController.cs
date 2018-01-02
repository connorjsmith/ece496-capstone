using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PillDispenserWeb.Models.DataTypes;
using PillDispenserWeb.Models;

using Microsoft.AspNetCore.Authorization;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillDispenserWeb.Controllers
{
    public class TwilioController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        private readonly AppDataContext appDataContext;

        //Our Account Sid and Auth Token at twilio.com/console
        private const string accountSid = "AC36c72ac486918a5bb537d9e47d0051e0";
        private const string authToken = "f3848665dd4cf439fcf7f8cb3b8b4706";

        public TwilioController(IAuthorizationService _authorizationService, AppDataContext _appDataContext)
        {
            authorizationService = _authorizationService;
            appDataContext = _appDataContext;
        }
        
        public void SendMissedNotificationUsingPatientObject (Patient patientObj)
        {
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(patientObj.PhoneNumber);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+12898064098"),//Assigned number from twilio
                body: "It works! - Shreyas");

            Console.WriteLine(message.Sid);
        }

        // GET: /<controller>/
        public void SendMissedNotificationUsingPatientID (long patientID)
        {
            //Look up patient object
            var patient = appDataContext.Patients.Find(patientID);

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(patient.PhoneNumber);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+12898064098"),//Assigned number from twilio
                body: "It works! - Shreyas");

            Console.WriteLine(message.Sid);
        }
    }
}
