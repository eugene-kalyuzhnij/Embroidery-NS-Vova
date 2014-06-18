using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace NSEmbroidery.Data.Models
{
    [Table("Users")]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //public DateTime Year { get; set; }

        public virtual ICollection<Comment> Comments {get; set;}
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Embroidery> Embroideries { get; set; }

    }
}
