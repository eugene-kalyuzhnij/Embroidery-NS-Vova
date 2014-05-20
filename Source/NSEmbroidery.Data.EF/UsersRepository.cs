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


        public void Add(User user)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Remove(User user)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Users.Remove(user);
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
    }
}
