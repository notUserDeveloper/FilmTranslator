using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace FilmsTranslator.Main.Code
{
    public class HttpRequester
    {
        public string DoRequest(string url)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.GetEncoding("windows-1251");
                    ServicePointManager.Expect100Continue = false;
                    wc.Headers.Add(new NameValueCollection
                        {
                            {"User-Agent", "Mozilla/4.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/14.0"},
                            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"},
                            {"Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3"}
                        });
                    return wc.DownloadString(url);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}