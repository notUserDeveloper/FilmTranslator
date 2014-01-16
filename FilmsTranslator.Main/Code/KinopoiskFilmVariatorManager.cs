using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class KinopoiskFilmVariatorManager
    {
        private readonly DamerauLevensteinMetric _damerauLevenstein;

        public KinopoiskFilmVariatorManager(DamerauLevensteinMetric damerauLevenstein)
        {
            _damerauLevenstein = damerauLevenstein;
        }

        public KinopoiskFilmVariator GetNearFilmVariation(string source, List<KinopoiskFilmVariator> filmVariations)
        {
            var min = int.MaxValue;
            var index = 0;
            for (int i = 0; i < filmVariations.Count; i++)
            {
                var distance = _damerauLevenstein.GetDistance(source, filmVariations[i].Title + " " + filmVariations[i].Year);
                if (distance < min)
                {
                    min = distance;
                    index = i;
                }
            }
            return filmVariations[index];
        }
    }
}
