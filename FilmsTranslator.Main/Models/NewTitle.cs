using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsTranslator.Main.Models
{
    public class NewTitle
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public string ClearTitle { get; set; }
        public string TransliteTitle { get; set; }
        public string Predictor { get; set; }
        [ForeignKey("Kinopoisk")]
        public int? KinopoiskId { get; set; }
        public DateTime TryDate { get; set; }

        public virtual Film Film { get; set; }
        public virtual Kinopoisk Kinopoisk { get; set; }
    }
}
