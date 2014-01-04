using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace FilmsTranslator.Proxy
{
    public class ProxyMain
    {
        private static int _page = 3;
        private static string _patternSait;
        private static string _patternIp;
        private static MatchCollection _matchCol;
        private static Regex _regex;
        private static WebClient _client;
        private static CountdownEvent _countdown;


        public static void MainGrabber()
        {
            //  id="serp_res_(\d+)">\s*<div class="res-wrap">\s*<h3 class="res-head">\s*<a\s*[\S, ]*\s*target="[\w]*"\s*href="http://(?:www.)?([-\w.]+/[\S]*)"
            _patternSait =
                @"id=""serp_res_(\d+)"">\s*<div class=""res-wrap"">\s*<h3 class=""res-head"">\s*<a\s*[\S, ]*\s*target=""[\w]*""\s*href=""http://(?:www.)?([-\w.]+/[\S]*)""";
            //  (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{2,5})
            _patternIp = @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{2,5})";

            var th1 = new Thread(SiteGrabber);
            //th1.Start();
            var th2 = new Thread(GrabberIp);
            //th2.Start();

            var th4 = new Thread(ReGrabber);
            //th4.Start();

            var db = new DataProviderProxy();
            while (true)
            {
                List<string> listIp = db.GetUncheckedIp(100);
                if (listIp != null)
                {
                    _countdown = new CountdownEvent(listIp.Count);
                    foreach (string item in listIp)
                    {
                        new Thread(Checker).Start(item);
                    }
                    _countdown.Wait();
                }
                Thread.Sleep(50);
            }
        }


        public static void SiteGrabber()
        {
            while (true)
            {
                using (_client = new WebClient())
                {
                    var db = new DataProviderProxy();
                    _client.Encoding = Encoding.UTF8;
                    string resault =
                        _client.DownloadString(
                            @"http://go.mail.ru/search?q=+"":8080"" +"":3128"" +"":80""&num=10&rch=e&sf=" + _page*10);
                    _regex = new Regex(_patternSait);
                    _matchCol = _regex.Matches(resault);
                    foreach (Match match in _matchCol)
                    {
                        Console.WriteLine("найден: " + match.Groups[2].Value);
                        db.InsertSite(match.Groups[2].Value);
                    }
                    _page++;
                }
                Thread.Sleep(1000*60*3);
            }
        }

        public static void GrabberIp()
        {
            while (true)
            {
                Thread.Sleep(1000*20);
                using (_client = new WebClient())
                {
                    var db = new DataProviderProxy();
                    _client.Encoding = Encoding.UTF8;
                    Dictionary<int, string> dbResault = db.GetSiteWithId();
                    if (dbResault == null) continue;
                    try
                    {
                        string resault = _client.DownloadString(@"http://" + dbResault.Values.ElementAt(0));
                        _regex = new Regex(_patternIp);
                        _matchCol = _regex.Matches(resault);
                        foreach (Match match in _matchCol)
                        {
                            Console.WriteLine("найден ip: " + match.Groups[1].Value);
                            db.InsertIp(dbResault.Keys.ElementAt(0), match.Groups[1].Value);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("errorConnect 404");
                    }
                    finally
                    {
                        db.UpdateDate(dbResault.Keys.ElementAt(0), DateTime.Now);
                    }
                }
            }
        }

        public static void Checker(object stat)
        {
            WebRequest webReq = WebRequest.Create(@"http://bitfry.narod.ru/02.htm");
            var db = new DataProviderProxy();
            string ip = stat.ToString();
            try
            {
                webReq.Proxy = new WebProxy(ip);
                webReq.Timeout = 10000;
                WebResponse webRes = webReq.GetResponse();
                Console.WriteLine("отличная прокся: " + ip);
                db.UpdateStatus(ip, StatusIp.Good, "ip");
            }
            catch
            {
                db.DeleteIp(ip);
                Console.WriteLine("плохая прокся del: " + ip);
            }
            finally
            {
                _countdown.Signal();
            }
        }

        public static void ReGrabber()
        {
            while (true)
            {
                Thread.Sleep(1000*60*60);
                var db = new DataProviderProxy();
                Dictionary<int, string> resault = db.GetOldSites();
                if (resault == null) continue;
                foreach (var item in resault)
                {
                    db.UpdateDate(item.Key, null);
                    db.UpdateStatus(item.Key.ToString(), StatusIp.Unchecked, "idSite");
                }
            }
        }
    }
}