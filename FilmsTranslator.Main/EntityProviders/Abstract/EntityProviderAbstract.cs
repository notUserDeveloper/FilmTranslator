using System;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders.Abstract
{
    public abstract class EntityProviderAbstract<TEntity> where TEntity:class 
    {
        private EntityContext db;

        protected EntityProviderAbstract()
        {
            db = DB.Init;
        }

        public TEntity MakeObserved(TEntity entity, Func<TEntity,bool> func)
        {
            try
            {
                //db.Set<TEntity>()
            }
            catch
            {
                throw new Exception("Объект или несколько объектов не найдены в бд, убедитесь что они добавлены");
            }
            return null;
        }
    }
}
