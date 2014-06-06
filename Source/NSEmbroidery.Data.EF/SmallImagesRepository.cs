using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.Data.EF
{
    /*
    public class SmallImagesRepository : IRepository<SmallImage>
    {
        public SmallImage GetById(int id)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.SmallImages.Find(id);
            }
        }

        public IList<SmallImage> GetAll()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.SmallImages.ToList();
            }
        }

        public SmallImage Add(SmallImage item)
        {
            using (ModelContext context = new ModelContext())
            {
                var small = context.SmallImages.Add(item);
                context.SaveChanges();

                return small;
            }
        }

        public void Remove(SmallImage item)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public SmallImage GetLast()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.SmallImages.Last();
            }
        }
    }
     * 
     * */
}
