using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NSEmbroidery.Core;

namespace NSEmbroidery.UI
{
    public partial class Form1 : Form
    {
        Bitmap CurrentImage { get; set; }


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*|jpg files (*.jpg)|*.jpg|bmp files (*bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            CurrentImage = new Bitmap(myStream);
                            pictureBoxCurrentImage.Image = CurrentImage;
                            pictureBoxCurrentImage.Refresh();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void buttonChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = Color.FromArgb(255, 255, 128, 128);
            if (MyDialog.ShowDialog() == DialogResult.OK)
                AddColorToPanelColor(MyDialog.Color);
        }


        private void AddColorToPanelColor(Color color)
        {
            int y;
            int countControls = panelColors.Controls.Count;

            if(countControls > 0)
                y = panelColors.Controls[countControls - 1].Location.Y + 25;
            else y = 10;

            PictureBox boxColor = new PictureBox();
            boxColor.Size = new Size(20, 20);
            boxColor.Location = new Point(5, y);
            boxColor.BackColor = color;
            boxColor.Click += boxColor_Click;

            panelColors.Controls.Add(boxColor);
            panelColors.Refresh();
        }


        private void boxColor_Click(object sender, EventArgs e)
        {
            PictureBox boxColor = sender as PictureBox;
            if (boxColor != null)
            {
                panelColors.Controls.Remove(boxColor);
                RefreshPanelColor();
            }
        }


        private void RefreshPanelColor()
        {
            int y = 10;

            foreach (PictureBox item in panelColors.Controls)
            {
                item.Location = new Point(5, y);

                y += 25;
            }
        }


        public Color[] GetColorsFromPanel()
        {
            Color[] result = new Color[panelColors.Controls.Count];
            int i = 0;
            foreach (PictureBox item in panelColors.Controls)
            {
                result[i++] = item.BackColor;
            }

            return result;
        }

        private void buttonCreateScheme_Click(object sender, EventArgs e)
        {
            //use dll here
            if(CurrentImage == null)
            {
                MessageBox.Show("Open any image first!");
                return;
            }
            
            int countOfCrissCrosses;
            try
            {
                countOfCrissCrosses = Convert.ToInt32(texBoxCountOfCrissCrosses.Text);
            }
            catch
            {
                MessageBox.Show("Sure that Count of CrissCrosses is correct");
            }

            //Settings settings = new Settings(countOfCrissCrosses, 
            //BitmapConverter converter = new BitmapConverter(CurrentImage, 


        }

    }
}
