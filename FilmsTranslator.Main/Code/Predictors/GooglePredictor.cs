using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using FilmsTranslator.Main.Code.Predictors.Abstract;
using FilmsTranslator.Main.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilmsTranslator.Main.Code.Predictors
{
    public class GooglePredictor : PredictorAbstract
    {

        private readonly HttpRequester _httpRequester;

        public GooglePredictor(HttpRequester httpRequester)
        {
            _httpRequester = httpRequester;
        }


        public override NewTitle GetPredictor(NewTitle newTitle)
        {
            Thread.Sleep(new Random().Next(1000, 5000));
            var content = _httpRequester.DoRequest("https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=" + HttpUtility.HtmlEncode(newTitle.TransliteTitle + " кинопоиск"));
            newTitle.Predictor = ParsePredictor(content);
            return newTitle;
        }

        protected override string ParsePredictor(string context)
        {
            string predictor = context;
            if (predictor == null)
            {
                return null;
            }
            var jObj = (JObject) JsonConvert.DeserializeObject(context);
            try
            {
                if ((int) jObj["responseStatus"] == 200)
                {
                    predictor = jObj["responseData"]["results"].Select(x => (string) x["title"]).FirstOrDefault();
                    return Regex.Replace(predictor, "<[^>]*>", String.Empty);
                }
            }
            catch (Exception)
            {

                return null;
            }
            return null;
        }
    }
}