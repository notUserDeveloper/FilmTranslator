using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityManagers
{
    public class KinopoiskManager
    {
        private EntityContext db;
        public KinopoiskManager()
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
