using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using System.Data.Entity;

namespace NSEmbroidery.Data.EF
{
    public class UsersRepository : IRepository<User>
    {
        public User GetById(int Id)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Users.Find(Id);
            }
        }


        public User Add(User user)
        {
            using (ModelContext context = new ModelContext())
            {
                var u = context.Users.Add(user);
                context.SaveChanges();

                return u;
            }
        }

        public void Remove(User user)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public User GetLast()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Users.Last();
            }
        }



        public IList<User> GetAll()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Users.ToList();
            }
        }



        public void SaveChanges(User item)
        {
            throw new NotImplementedException();
        }
    }
}
