using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DoorbellDingerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DoorbellController : ControllerBase
    {
        private readonly ILogger<DoorbellController> _logger;

        private readonly IConfiguration _config;

        public DoorbellController(ILogger<DoorbellController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        public void Ding()
        {
            var accountSid = _config.GetSection("TwilioSid").Value;
            var accountAuthToken = _config.GetSection("TwilioAuthToken").Value;
            var fromNumber = _config.GetSection("FromNumber").Value;
            var toNumber = _config.GetSection("ToNumber").Value;

            TwilioClient.Init(accountSid, accountAuthToken);

            MessageResource.Create(
                body: "Does this work?",
                from: new Twilio.Types.PhoneNumber(fromNumber),
                to: new Twilio.Types.PhoneNumber(toNumber)
            );
        }
    }
}
