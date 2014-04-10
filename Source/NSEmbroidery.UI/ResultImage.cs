using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSEmbroidery.UI
{
    public partial class ResultImage : Form
    {
        Bitmap image;
        public Bitmap Image
        {
            get { return image; }
            set 
            { 
                pictureBoxResultImage.Image = value;

                int width, height = 0;
                if (Screen.PrimaryScreen.Bounds.Width / 2 < value.Width)
                    width = Screen.PrimaryScreen.Bounds.Width / 2;
                else width = value.Width;

                if (Screen.PrimaryScreen.Bounds.Height / 2 < value.Height + 35)
                    height = Screen.PrimaryScreen.Bounds.Height / 2;
                else height = value.Height;

                this.ClientSize = new Size(width, height + 30);
                this.panel1.Size = new Size(width, height);

                this.buttonCencel.Location = new Point(width - 90, this.ClientSize.Height - 28);
                this.buttonSave.Location = new Point(width - 170, this.ClientSize.Height - 28);
            }
        }

        public ResultImage()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBoxResultImage.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBoxResultImage.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBoxResultImage.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void buttonCencel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void ResultImage_SizeChanged(object sender, EventArgs e)
        {
            this.panel1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 35);

            this.buttonCencel.Location = new Point(this.ClientSize.Width - 90, this.ClientSize.Height - 28);
            this.buttonSave.Location = new Point(this.ClientSize.Width - 170, this.ClientSize.Height - 28);
        }
    }
}
