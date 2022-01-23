using System;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DoorbellDinger
{
    /// <summary>
    /// Proof of concept console app
    /// 
    /// Created following Twilio's tutorial here: https://www.twilio.com/docs/sms/quickstart/csharp-dotnet-core
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("INFO: Getting configuration values");
                var config = BuildConfig();

                var accountSid = config.GetSection("TwilioSid").Value;
                var accountAuthToken = config.GetSection("TwilioAuthToken").Value;
                var fromNumber = config.GetSection("FromNumber").Value;
                var toNumber = config.GetSection("ToNumber").Value;

                Console.WriteLine("INFO: Building TwilioClient");
                TwilioClient.Init(accountSid, accountAuthToken);

                Console.WriteLine("INFO: Sending text message");
                MessageResource.Create(
                    body: "Does this work?",
                    from: new Twilio.Types.PhoneNumber(fromNumber),
                    to: new Twilio.Types.PhoneNumber(toNumber)
                );
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: Error occured");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private static IConfigurationRoot BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();
        }
    }
}
