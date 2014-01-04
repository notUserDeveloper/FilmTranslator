namespace FilmsTranslator.Main.Code.Spellers.Abstract
{
    public abstract class SpellerAbstract
    {
        protected readonly HttpRequester HttpRequester;
        protected SpellerAbstract(HttpRequester httpRequester)
        {
            HttpRequester = httpRequester;
        }

        public abstract string DoSpell(string text);
    }
}
