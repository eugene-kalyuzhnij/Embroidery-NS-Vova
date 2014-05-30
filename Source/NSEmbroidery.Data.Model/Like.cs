using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NSEmbroidery.Data.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Embroidery Embroidery { get; set; }
        public int EmbroideryId { get; set; }
    }
}