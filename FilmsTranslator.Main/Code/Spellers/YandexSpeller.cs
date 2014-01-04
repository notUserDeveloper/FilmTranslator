using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Code.Spellers.Abstract;
using FilmsTranslator.Main.Code.Spellers.Transport;
using Newtonsoft.Json;

namespace FilmsTranslator.Main.Code.Spellers
{
    public class YandexSpeller : SpellerAbstract
    {
        private const string ServiceUrl = "http://speller.yandex.net/services/spellservice.json/checkText?options=2&text=";

        public YandexSpeller(HttpRequester httpRequester) : base(httpRequester) { }

        public override string DoSpell(string text)
        {
            var jsonResponce = JsonConvert.DeserializeObject<YandexSpellerResponce[]>(HttpRequester.DoRequest(ServiceUrl + text));
            return FixList(text, jsonResponce);
        }

        private string FixList(string text, IEnumerable<YandexSpellerResponce> jsonResponce)
        {
            var offset = 0;
            foreach (var item in jsonResponce)
            {
                if (item.s.Length == 0){ continue; }
                text = text.Replace(text.Substring(item.pos+offset, item.len), item.s.First());
                offset += GetOffset(item.len,item.s.First().Length);
            }
            return text;
        }

        private int GetOffset(int oldLength, int newLength)
        {
            if (oldLength != newLength)
            {
                return newLength - oldLength;
            }
            return 0;
        }
    }
}
