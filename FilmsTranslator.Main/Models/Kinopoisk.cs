using System.ComponentModel.DataAnnotations;

namespace FilmsTranslator.Main.Models
{
    public class Kinopoisk
    {
        [Key]
        public int Id { get; set; }
        public string RusName { get; set; }
        public string EngName { get; set; }
        public string Year { get; set; }
        public string Country { get; set; }
        public string Producer { get; set; }
        public string Genre { get; set; }
        public double? Budget { get; set; }
        public double Rating { get; set; }
    }
}
