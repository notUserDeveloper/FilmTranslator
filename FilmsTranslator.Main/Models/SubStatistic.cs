using System.ComponentModel.DataAnnotations;

namespace FilmsTranslator.Main.Models
{
    public class SubStatistic
    {
        [Key]
        public int Id { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
    }
}
