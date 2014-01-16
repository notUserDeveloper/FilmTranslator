using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace FilmsTranslator.Main.Models
{
    public class KinopoiskFilmVariator
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("NewTitle")]
        public int NewTitleId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Url { get; set; }


        public virtual NewTitle NewTitle { get; set; }
    }
}
