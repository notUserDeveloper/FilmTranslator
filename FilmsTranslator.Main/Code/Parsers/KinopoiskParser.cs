using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class KinopoiskParser
    {
        private readonly HttpRequester _httpRequester;

        private const string PatternGrabKinopoisk = @"\s*<h1 class=""moviename-big"" itemprop=""name"">(?<RusName>[^<]*)\s*.*alternativeHeadline"">(?<EngName>.*?)</span>.*?year.*?>(?<Year>.*?)</a>.*?country.*?>(?<Country>.*?)</a>.*?director"">(?:<a.*?>)?(?<Producer>[^<]*)(?:</a>)?.*?genre""><a.*?>(?<Genre>.*?)</a>";
        private readonly Regex _regexKinopoisk = new Regex(PatternGrabKinopoisk, RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public KinopoiskParser(HttpRequester httpRequester)
        {
            _httpRequester = httpRequester;
        }

        public Kinopoisk GetKinopoiskInfo(string text)
        {
            var content = _httpRequester.DoRequest("http://www.kinopoisk.ru/index.php?first=yes&what=&kp_query=" + text);
            if (content == null || content.Contains("К сожалению, по вашему запросу ничего не найдено"))
            {
                return null;
            }
            return ParseContentKinopoisk(HttpUtility.HtmlDecode(content));
        }

        private Kinopoisk ParseContentKinopoisk(string content)
        {
            Match parse = _regexKinopoisk.Match(content);
            Func<string, double> getDouble = s =>
                {
                    double rez;
                    if (Double.TryParse(s, out rez))
                    {
                        return rez;
                    }
                    return 0;
                };
            return new Kinopoisk
                {
                    //Budget = getDouble(parse.Groups["Budget"].Value),
                    Country = parse.Groups["Country"].Value,
                    EngName = parse.Groups["EngName"].Value,
                    Genre = parse.Groups["Genre"].Value,
                    Producer = parse.Groups["Producer"].Value,
                    //Rating = getDouble(parse.Groups["Rating"].Value),
                    RusName = parse.Groups["RusName"].Value,
                    Year = parse.Groups["Year"].Value
                };
        }
    }
}