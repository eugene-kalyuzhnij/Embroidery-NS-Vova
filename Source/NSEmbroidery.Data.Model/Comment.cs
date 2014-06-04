﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NSEmbroidery.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Comment_msg { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Embroidery Embroidery { get; set; }
        public int EmbroideryId { get; set; }
    }
}