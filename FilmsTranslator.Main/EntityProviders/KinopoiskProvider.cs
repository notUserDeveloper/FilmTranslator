using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class KinopoiskProvider
    {
        private EntityContext db;
        public KinopoiskProvider()
        {
            db = DB.Init;
        }

        public void AddKinopoiskItem(Kinopoisk kinopoiskInfo)
        {
            var kinopoiskInfoDb = db.Kinopoisks.FirstOrDefault(k => k.RusName == kinopoiskInfo.RusName);
            if (kinopoiskInfoDb == null)
            {
                db.Kinopoisks.Add(kinopoiskInfo);
            }
            else
            {
                kinopoiskInfo.Id = kinopoiskInfoDb.Id;
            }
            db.SaveChanges();
        }
    }
}
