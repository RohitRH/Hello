using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPush;

namespace WebPushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebPushController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PushSubscription pushSubscription)
        {
            //var pushSubscription = new PushSubscription(subscription.Endpoint, subscription.Key, subscription.AuthSecret);
            var vapidDetails = new VapidDetails("subject","public key","private key");

            var webPushClient = new WebPushClient();
            webPushClient.SetVapidDetails(vapidDetails);

            //TODO; store pushsubscription for later use

            // send notification
            var payload = new 
            {
                Msg = "Thank you for subscribing",
                Icon = "[URL to an image to display in the notification]"
            };

            try
            {
                await webPushClient.SendNotificationAsync(pushSubscription, JsonConvert.SerializeObject(payload), vapidDetails);
            }
            catch (WebPushException exception)
            {
                var statusCode = exception.StatusCode;
                return new StatusCodeResult((int)statusCode);
            }

            return new OkResult();
        }
    }
}