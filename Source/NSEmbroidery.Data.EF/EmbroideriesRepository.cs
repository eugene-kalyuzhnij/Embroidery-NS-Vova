﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.Data.EF
{
    public class EmbroideriesRepository : IRepository<Embroidery>
    {
        public Models.Embroidery GetById(int id)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Embroideries.Find(id);
            }
        }

        public void Add(Embroidery item)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Embroideries.Add(item);
                context.SaveChanges();
            }
        }

        public void Remove(Embroidery item)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Embroideries.Remove(item);
            }
        }

        public Models.Embroidery GetLast()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Embroideries.Last();
            }
        }


        public IList<Embroidery> GetAll()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Embroideries.ToList();
            }
        }
    }
}
