using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelContext context = new ModelContext();

            context.Model.Add(new Model() { Name = "name" });
        }

    }

    class ModelContext : DbContext
    {
        public DbSet<Model> Model { get; set; }
    }


    class Model
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }


    class Model2
    {
        public int Id { get; set; }

        public string Name2 { get; set; }
    }
}
