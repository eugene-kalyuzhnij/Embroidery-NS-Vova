using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Data.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IList<T> GetAll();
        T Add(T item);
        void Remove(T item);
        T GetLast();
    }
}
