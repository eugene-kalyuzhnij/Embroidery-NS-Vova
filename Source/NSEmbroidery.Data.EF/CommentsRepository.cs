using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.Data.EF
{
    public class CommentsRepository : IRepository<Comment>
    {
        public Comment GetById(int id)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Comments.Find(id);
            }
        }

        public IList<Comment> GetAll()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Comments.ToList();
            }
        }

        public Comment Add(Comment item)
        {
            using (ModelContext context = new ModelContext())
            {
                var comment = context.Comments.Add(item);
                context.SaveChanges();

                return comment;
            }
        }

        public void Remove(Comment item)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Comments.Add(item);
                context.SaveChanges();
            }
        }

        public Comment GetLast()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Comments.Last();
            }
        }

    }
}
