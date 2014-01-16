using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class TranslationManager
    {
        private readonly PredictorManager _predictorManager;
        private readonly Transliteration _transliteration;

        public TranslationManager(Transliteration transliteration, PredictorManager predictorManager)
        {
            _transliteration = transliteration;
            _predictorManager = predictorManager;
        }

        public void DoTranslate(NewTitle newTitle)
        {
            newTitle.TransliteTitle =  _transliteration.DoTransliteration(newTitle.ClearTitle);
            newTitle.Predictor = _predictorManager.DoPredict(newTitle.TransliteTitle);
        }
    }
}