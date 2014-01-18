using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsTranslator.Main.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Job")]
        public int? JobId { get; set; }
        public string StartTitle { get; set; }
        public string Extension { get; set; }
        public int? Year { get; set; }
        public double Size { get; set; }
        public DateTime AddDate { get; set; }
        public bool Checked { get; set; }


        public virtual Job Job { get; set; }
        public virtual ICollection<NewTitle> NewTitles { get; set; }
    }
}
