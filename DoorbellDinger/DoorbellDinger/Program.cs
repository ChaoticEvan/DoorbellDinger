using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DoorbellDinger
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();

            var accountSid = config.GetSection("TwilioSid").Value;
            var accountAuthToken = config.GetSection("TwilioAuthToken").Value;
            var fromNumber = config.GetSection("FromNumber").Value;
            var toNumber = config.GetSection("ToNumber").Value;

            TwilioClient.Init(accountSid, accountAuthToken);

            var message = MessageResource.Create(
                body: "Does this work?",
                from: new Twilio.Types.PhoneNumber(fromNumber),
                to: new Twilio.Types.PhoneNumber(toNumber)
            );

            Console.WriteLine(message.Sid);
        }
    }
}
