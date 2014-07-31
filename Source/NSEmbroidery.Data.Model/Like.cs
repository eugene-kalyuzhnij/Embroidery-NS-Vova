using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace NSEmbroidery.Data.Models
{
    [Serializable]
    [DataContract]
    public class Like
    {
        protected virtual User User { get; set; }

        [Key]
        [Column(Order=0)]
        [DataMember]
        public int UserId { get; set; }
        
        protected virtual Embroidery Embroidery { get; set; }

        [Key]
        [Column(Order=1)]
        [DataMember]
        public int EmbroideryId { get; set; }
    }
}