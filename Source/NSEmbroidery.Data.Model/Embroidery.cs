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
using System.Runtime.Serialization;

namespace NSEmbroidery.Data.Models
{
    [Serializable]
    [DataContract]
    public class Embroidery
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool PublicEmbroidery { get; set; }

        [Required]
        [DataMember]
        public byte[] Data { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public byte[] SmallImageData { get; set; }

        [DataMember]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }

        protected virtual ICollection<Comment> Comments { get; set; }
        protected virtual ICollection<Like> Likes { get; set; }


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

        public Embroidery(Bitmap image, Size newSize)
            : this(image)
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

        public bool CreateSmallImage(Size newSize)
        {
            Bitmap image = GetImage();
            if (image != null)
            {
                try
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

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        public Bitmap GetImage()
        {

                Bitmap result = null;
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    using (MemoryStream stream = new MemoryStream(Data))
                    {
                        result = (Bitmap)formatter.Deserialize(stream);
                    }

                    return result;
                }
                catch
                {
                    return null;
                }

        }

        public Bitmap GetSmallImage()
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
