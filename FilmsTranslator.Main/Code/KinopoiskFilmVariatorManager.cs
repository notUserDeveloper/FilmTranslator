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

        public KinopoiskFilmVariator GetNearFilmVariation(string source, int? sourceYear, List<KinopoiskFilmVariator> filmVariations)
        {
            var min = int.MaxValue;
            var index = 0;
            for (int i = 0; i < filmVariations.Count; i++)
            {
                int distance;
                if (sourceYear.HasValue)
                {
                    distance = _damerauLevenstein.GetDistance(source + " " + sourceYear,
                        filmVariations[i].Title + " " + filmVariations[i].Year);
                }
                else
                {
                    distance = _damerauLevenstein.GetDistance(source,
                        filmVariations[i].Title);
                }

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
