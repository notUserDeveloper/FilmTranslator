using System.ComponentModel.DataAnnotations;

namespace FilmsTranslator.Main.Models
{
    public class Parasite
    {
        [Key]
        public int Id { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
    }
}
