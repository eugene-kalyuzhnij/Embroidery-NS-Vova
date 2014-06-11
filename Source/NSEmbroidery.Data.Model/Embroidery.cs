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
using System.Drawing.Drawing2D;

namespace NSEmbroidery.Data.Models
{
    public class Embroidery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool PublicEmbroidery { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public DateTime DateCreated { get; set; }

        public byte[] SmallImageData { get; set; }

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

        public Embroidery(Bitmap image, Size newSize) : this(image)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)newSize.Width / (float)sourceWidth);
            nPercentH = ((float)newSize.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, 0, 0, destWidth, destHeight);
            g.Dispose();

            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, b);
                SmallImageData = stream.ToArray();
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

        public Bitmap SmallImage
        {
            get
            {
                Bitmap result = null;
                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream stream = new MemoryStream(SmallImageData))
                {
                    result = (Bitmap)formatter.Deserialize(stream);
                }

                return result;
            }
        }

    }
}
