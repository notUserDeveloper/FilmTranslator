using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class JobProvider
    {
        private EntityContext db;
        public JobProvider()
        {
            db = DB.Init;
        }

        public void AddJob(ref Job job)
        {
            Job jobLoc = job;
            var jobDb = db.Jobs.FirstOrDefault(j => j.Path == jobLoc.Path);
            if (jobDb == null)
            {
                db.Jobs.Add(job);
            }
            else
            {
                job = jobDb;
            }
        }
    }
}
