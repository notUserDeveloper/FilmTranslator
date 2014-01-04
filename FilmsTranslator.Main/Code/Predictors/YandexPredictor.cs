using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using FilmsTranslator.Main.Code.Predictors.Abstract;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code.Predictors
{
    public class YandexPredictor : PredictorAbstract
    {
        private const string PatternGrabYandexSearch = @"<h2.*?><span>(.*?)</span>.*?</h2>";
        private readonly HttpRequester _httpRequester;
        private readonly Regex _regexYandexSearch = new Regex(PatternGrabYandexSearch, RegexOptions.IgnoreCase);


        public YandexPredictor(HttpRequester httpRequester)
        {
            _httpRequester = httpRequester;
        }

        public override NewTitle GetPredictor(NewTitle newTitle)
        {
            Thread.Sleep(new Random().Next(2100, 6500));
            string content =
                _httpRequester.DoRequest(
                    "http://xmlsearch.yandex.ru/xmlsearch?user=alexksey-stadov&key=03.220628124:22c3eb602725b4e76839a0de25fed92e&query=" +
                    HttpUtility.HtmlEncode(newTitle.TransliteTitle + " кинопоиск"));
            newTitle.Predictor = ParsePredictor(content);
            return newTitle;
        }

        protected override string ParsePredictor(string context)
        {
            Match match = _regexYandexSearch.Match(context);
            return Regex.Replace(match.Groups[1].Value, "<.*?>", String.Empty);
        }
    }
}