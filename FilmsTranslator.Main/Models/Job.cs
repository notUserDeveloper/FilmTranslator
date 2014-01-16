using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FilmsTranslator.Main.Code.Enums;
namespace FilmsTranslator.Main.Models
{
    public class Job // Назвал так, task taskstatus - .net отжал (
    {
        public Job()
        {
            Status = JobStatus.Await;
        }

        [Key]
        public int Id { get; set; }
        public JobStatus Status { get; set; }
        public string Path { get; set; }


        public virtual ICollection<Film> Films { get; set; }
    }
}
