using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Z5
{
    public class ShowApi
    {
        public static List<Episode> start_get(int id)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format($"http://api.tvmaze.com/seasons/{id}/episodes"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            List<Episode> episodes = JsonConvert.DeserializeObject<List<Episode>>(jsonString);

            return episodes;
        }
    }
}
