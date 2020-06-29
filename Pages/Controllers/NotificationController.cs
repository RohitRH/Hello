using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPush;

namespace WebPushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public static List<PushSubscription> Subscriptions { get; set; } = new List<PushSubscription>();
        VapidDetails vapidDetails = new VapidDetails
        {
            PrivateKey = "6GJW3jlOQonru2IsakRLpqj2d6qURK2C9GCZSlYwKq8",
            PublicKey = "BMBuVtMBpcgwRtUNttNj2yXP3PGCSrf_fT94pCb1Bdl1JDnH8_CSK0GXqa8hOAkLq1EYnTH__zaXhy5jLoJ4s2A",
            Subject = "https://localhost:44301"
        };

        [HttpGet("Test")]
        public string Get()
        {
            return "Test successful!!!";
        }

        [HttpPost("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Subscribe([FromBody] PushSubscription sub)
        {
            Subscriptions.Add(sub);
        }

        [HttpPost("unsubscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Unsubscribe([FromBody] PushSubscription sub)
        {
            var item = Subscriptions.FirstOrDefault(s => s.Endpoint == sub.Endpoint);
            if (item != null)
            {
                Subscriptions.Remove(item);
            }
        }

        [HttpPost("broadcast")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Broadcast()
        {
            var client = new WebPushClient();
            client.SetVapidDetails(vapidDetails);
            var serializedMessage = JsonConvert.SerializeObject(new { Title = "New Notification",Message = "Hello Welcome to our site",Url = "google.com"});
            foreach (var pushSubscription in Subscriptions)
            {
                client.SendNotification(pushSubscription, serializedMessage, vapidDetails);
            }

        }
    }
}