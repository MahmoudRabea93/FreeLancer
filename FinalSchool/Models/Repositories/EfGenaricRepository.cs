using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalSchool.Models.Repositories
{
    public class EfGenaricRepository<T> : IGenaricRepository<T> where T :class
    {
        DbContext Context;
        IDbSet<T> Entity;

        public EfGenaricRepository(DbContext db)
        {
            this.Context = db;
            Entity = Context.Set<T>();
        }

        public void Add(T EntityToAdd)
        {
            Entity.Add(EntityToAdd);
        }

        public void Delete(int id,T EntityToDelete)
        {
            Entity.Attach(EntityToDelete);
            Context.Entry<T>(EntityToDelete).State = EntityState.Modified;
        }

        public T Display(int? id)
        {
           return Entity.Find(id);
        }

        public T Find(int? id)
        {
            return Entity.Find(id);
        }

        public IEnumerable<T> List()
        {
            return Entity.ToList();
        }

        public void Update(int id, T EntityToUpdate)
        {
            Entity.Attach(EntityToUpdate);
            Context.Entry<T>(EntityToUpdate).State = EntityState.Modified;
        }
    }
}