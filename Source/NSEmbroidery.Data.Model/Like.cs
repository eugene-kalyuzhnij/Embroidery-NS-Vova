using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSEmbroidery.Data.Models
{
    public class Like
    {
        /*
        [Key]
        public int Id { get; set; }
        */
        public virtual User User { get; set; }
        [Key]
        [Column(Order=0)]
        public int UserId { get; set; }

        public virtual Embroidery Embroidery { get; set; }
        [Key]
        [Column(Order=1)]
        public int EmbroideryId { get; set; }
    }
}