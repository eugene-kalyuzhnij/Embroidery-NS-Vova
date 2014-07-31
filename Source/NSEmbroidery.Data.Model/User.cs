using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace NSEmbroidery.Data.Models
{
    [Table("Users")]
    [Serializable]
    [DataContract]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [DataMember]
        public string FirstName { get; set; }

        [Required]
        [DataMember]
        public string LastName { get; set; }

        //public DateTime Year { get; set; }
        public virtual ICollection<Comment> Comments {get; private set;}
        public virtual ICollection<Like> Likes { get; private set; }
        protected virtual ICollection<Embroidery> Embroideries { get; private set; }

    }
}
