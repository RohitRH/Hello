using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using RestSharp;
using System.Net.Http.Headers;
using Org.BouncyCastle.Crypto.Tls;

namespace WebPushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendSMSController : ControllerBase
    {
        [HttpGet("send")]

        public async  Task<IActionResult> SendSMS()
        {

            var number = new Random().Next(123456,987654).ToString();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var values = new Dictionary<string, string>();
            client.DefaultRequestHeaders.Add("authorization",  "VPCZbhQevtl089aiSuwE4xrgmBpDOoKJ51nf6XRsFkANdM3zTUMXKyizNcIVphYHUxo3Sagf5FsZ7nBe");
            values.Add("sender_id","FSTSMS");
            values.Add("message","  your otp is "+number);
            values.Add("language","english");
            values.Add("route","p");
            values.Add("numbers","7829020568");
            var content = new FormUrlEncodedContent(values);
            //await response.Content.ReadAsStringAsync();
            var response = await client.PostAsync("https://www.fast2sms.com/dev/bulk", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("sent");
            }
            return BadRequest("invalid request");
        }

        
    }
}