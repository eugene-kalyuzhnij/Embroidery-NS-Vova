using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSEmbroidery.Data.Models
{
    public class Embroidery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image")]
        public byte[] Data { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }


        public Embroidery() { }

        public Embroidery(Bitmap image)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, image);
                Data = stream.ToArray();
            }
        }

        public Bitmap Image
        {
            get
            {
                Bitmap result = null;
                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream stream = new MemoryStream(Data))
                {
                    result = (Bitmap)formatter.Deserialize(stream);
                }

                return result;
            }
        }
    }
}
