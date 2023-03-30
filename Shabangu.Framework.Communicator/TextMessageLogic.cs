
using Shabangu.Communicator.Properties;
using Shabangu.Types.Communicator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;

using System.Net;
using System.Net.Http.Headers;
using System.Configuration;
using System.Text;

namespace Shabangu.Communicator
{
    public class TextMessageLogic
    {
        public bool SendTextMessage(TextMessage message)
        {
            int ems = 0;
            if(message.Message.Length > 160) { ems = 1; }
            using (HttpClient client = new HttpClient())
            {                
                client.BaseAddress = new Uri(message.BaseUrl);
                client.Timeout = new TimeSpan(0, 2, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "send?number=" + message.Cellnumber + "&message=" + message.Message + "&ems="+ ems);
                request.Headers.Add("Authorization", "Basic dTY0NDQ1OkRQR3lMZHJX");
                HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
                try
                {
                    response.EnsureSuccessStatusCode();
                    return response.IsSuccessStatusCode;                  
                }
                catch (Exception ex)
                {
                    throw new Exception($"Something is wrong: {ex.Message}");
                }
            }         
        }
    }
}
