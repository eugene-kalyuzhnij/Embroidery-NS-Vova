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
        public User GetById(int id)
        {
            using (ModelContext context = new ModelContext())
            {        
                return context.Users.Find(id);
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

                var comments = context.Comments.Where(c => c.UserId == user.Id);
                foreach (var comment in comments)
                        context.Entry(comment).State = EntityState.Deleted;

                var likes = context.Likes.Where(l => l.UserId == user.Id);
                foreach(var like in likes)
                    context.Entry(like).State = EntityState.Deleted;

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
            using (ModelContext context = new ModelContext())
            {
                var user = context.Users.Find(item.Id);
                user.Email = item.Email;
                user.FirstName = item.FirstName;
                user.LastName = item.LastName;

                context.SaveChanges();
            }
        }
    }
}
