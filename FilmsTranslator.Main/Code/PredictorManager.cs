using FilmsTranslator.Main.Code.Spellers.Abstract;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class PredictorManager
    {
        private readonly SpellerAbstract _speller;

        public PredictorManager(SpellerAbstract speller)
        {
            _speller = speller;
        }

        public void DoPredict(NewTitle transliterationNewTitle)
        {
            transliterationNewTitle.Predictor = _speller.DoSpell(transliterationNewTitle.TransliteTitle);
        }
    }
}
