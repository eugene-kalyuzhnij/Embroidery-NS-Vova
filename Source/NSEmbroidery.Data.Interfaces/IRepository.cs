﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace NSEmbroidery.Data.Interfaces
{

    public interface IRepository<T>
    {
        T GetById(int id);
        IList<T> GetAll();
        T Add(T item);
        void Remove(T item);
        T GetLast();
        void SaveChanges(T item);
    }
}
