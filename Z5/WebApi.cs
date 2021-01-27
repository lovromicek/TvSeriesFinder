using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Z5
{
    public class WebApi
    {
        public static List<TvSeriesTitles> GetShowListFromApi(string name)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format($"http://api.tvmaze.com/search/shows?q={name}"));

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

            var titles = JsonConvert.DeserializeObject<List<TvSeriesTitles>>(jsonString);

            return titles;
        }
    }
}