using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.Data.EF
{
    public class LikesRepository : IRepository<Like>
    {
        public Like GetById(int id)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Likes.Find(id);
            }
        }

        public IList<Like> GetAll()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Likes.ToList();
            }
        }

        public Like Add(Like item)
        {
            using (ModelContext context = new ModelContext())
            {
                var like = context.Likes.Add(item);
                context.SaveChanges();

                return like;
            }
        }

        public void Remove(Like item)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Like GetLast()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Likes.Last();
            }
        }
    }
}
