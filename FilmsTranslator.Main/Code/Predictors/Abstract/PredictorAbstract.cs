using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code.Predictors.Abstract
{
    public abstract class PredictorAbstract
    {
        public abstract NewTitle GetPredictor(NewTitle newTitle);
        protected abstract string ParsePredictor(string context);
    }
}