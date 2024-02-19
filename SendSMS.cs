using Microsoft.AspNetCore.Mvc;
using Notify.Client;
using Notify.Models;
using Notify.Models.Responses;
using System.Net;

namespace YourNameSpace.Helpers
{
    public class GovUKNotifyHelper
    {
        //GovUKNotify API Key
        public string apiKey()
        {
            return "GovUKNotify-APIKey";
        }

        //Send SMS by calling SendSMS("Mobile Number", "Message")
        public void SendSMS(string mobileNumber, string message)
        {
            var proxy = new WebProxy
            {
                Address = new Uri("http://proxy.ip.address:8080"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,
            };
            var puser = "domain\\proxyUSER";
            var ppass = "proxyPassword";
            if (puser != null && ppass != null)
                proxy.Credentials = new NetworkCredential(puser, ppass);

            var handler = new HttpClientHandler
            {
                Proxy = proxy,
            };

            var httpClientWithProxy = new HttpClientWrapper(new HttpClient(handler));

            var client = new NotificationClient(httpClientWithProxy, apiKey());

            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
              {"details", message},
            };

            SmsNotificationResponse response = client.SendSms(
            mobileNumber: mobileNumber,
            templateId: "template-id-string-from-govUKNotify",
            personalisation);
        }

    }
}
