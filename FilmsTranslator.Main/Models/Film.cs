using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmsTranslator.Main.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string StartTitle { get; set; }
        public string Extension { get; set; }
        public double Size { get; set; }
        public DateTime AddDate { get; set; }
        public bool Checked { get; set; }


        public virtual ICollection<NewTitle> NewTitles { get; set; }
    }
}
