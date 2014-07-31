using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NSEmbroidery.Data.Models
{
    [Serializable]
    [DataContract]
    public class Comment
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [DataMember]
        public string Comment_msg { get; set; }

        protected virtual User User { get; set; }
        [DataMember]
        public int UserId { get; set; }

        protected virtual Embroidery Embroidery { get; set; }
        [DataMember]
        public int EmbroideryId { get; set; }

        [Required]
        [DataMember]
        public DateTime DateCreated { get; set; }
    }
}
