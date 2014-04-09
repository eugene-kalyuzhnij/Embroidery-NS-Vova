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
using System.Drawing.Imaging;

namespace NSEmbroidery.UI
{
    public partial class Form1 : Form
    {
        Bitmap CurrentImage { get; set; }
        List<TextBox> textBoxes;
        Color SymbolColor;
        List<int> possibleCells;
        Dictionary<Resolution, int> resolutions;
        bool cellsChanged = false;

        public Form1()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            pictureBoxes = new List<PictureBox>();
            this.FillPanelPalette();
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

                            DeleteAllItems(comboBoxSquareCount);
                            DeleteAllItems(comboBoxResolution);
                            pictureBoxResult.Image = null;

                            CurrentImage = new Bitmap(myStream);
                            pictureBoxCurrentImage.Image = CurrentImage;

                            labelresolution.Text = CurrentImage.Width.ToString() + "x" + CurrentImage.Height.ToString();

                            possibleCells = Calculate.PossibleCellsCount(CurrentImage);


                            foreach (var item in possibleCells)
                                comboBoxSquareCount.Items.Add(item);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        List<PictureBox> pictureBoxes;
        private void FillPanelPalette()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                { 
                    y += 25;
                    x = 0;
                }

                PictureBox color = new PictureBox();
                color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                color.BackColor = Color.Green;
                color.Location = new System.Drawing.Point(5 + x, 5 + y);
                color.Name = "Color";
                color.Size = new System.Drawing.Size(20, 20);
                color.TabStop = false;
                color.Click += new EventHandler(ChoosedColor);

                pictureBoxes.Add(color);
                panelColorChoice.Controls.Add(color);

                x += 25;

            }

            pictureBoxes[0].BackColor = Color.Red;
            pictureBoxes[1].BackColor = Color.Green;
            pictureBoxes[2].BackColor = Color.Gray;
            pictureBoxes[3].BackColor = Color.Yellow;
            pictureBoxes[4].BackColor = Color.White;
            pictureBoxes[5].BackColor = Color.Black;
            pictureBoxes[6].BackColor = Color.Blue;
            pictureBoxes[7].BackColor = Color.DarkKhaki;
            pictureBoxes[8].BackColor = Color.Brown;
            pictureBoxes[9].BackColor = Color.Orange;
        }

        private void ChoosedColor(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            AddColorToPanelColor(picture.BackColor);
            panelColorChoice.Visible = false;


        }


        private void DeleteAllItems(ComboBox comboBox)
        {
            if(comboBox.Items.Count > 0)
            for (int i = 0; i < comboBox.Items.Count; )
                comboBox.Items.RemoveAt(i);
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
            boxColor.BorderStyle = BorderStyle.FixedSingle;

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
            


            int cellsCount = 0;

                try
                {
                    cellsCount = Convert.ToInt32(comboBoxSquareCount.SelectedItem);
                }
                catch
                {
                    MessageBox.Show("Sure that Count of cells is correct");
                    return;
                }

            if (cellsCount == 0)
            {
                MessageBox.Show("Choose Count of squares");
                return;
            }

            List<Char> symbols = new List<char>();
            foreach (var item in textBoxes)
            {
                try
                {
                    symbols.Add(Convert.ToChar(item.Text));
                }
                catch
                {
                    MessageBox.Show("Sumbols data is incorrext");
                    return;
                }
            }

            if (comboBoxResolution.SelectedItem == null)
            {
                MessageBox.Show("Choose resolution");
                    return;
            }

            Color[] palette = this.GetColorsFromPanel();

            if (palette.Length == 0)
            {
                MessageBox.Show("Choose palette");
                return;
            }



            int ratio;
            resolutions.TryGetValue((Resolution)comboBoxResolution.SelectedItem, out ratio);

            char[] masSymbols = null;
            masSymbols = new char[symbols.Count];
            int i = 0;
            foreach (var item in symbols)
                masSymbols[i++] = item;

            if (palette.Length > masSymbols.Length)
                masSymbols = null;


            bool grid = false;
            if (checkBoxGrid.CheckState == CheckState.Checked)
                grid = true;

/*--------------------using dll here------------------------------------------------*/
            Bitmap result = PatternCreator.CreateEmbroidery(CurrentImage, ratio, cellsCount, palette, masSymbols, SymbolColor, grid, GridType.SolidLine);
/*-----------------------------------------------------------------------------------*/

            pictureBoxResult.Image = result;

        }








        private void addTextBox_Click(object sender, EventArgs e)
        {
            TextBox box = new TextBox();
            if (textBoxes.Count == 0)
            {
                box.Location = new System.Drawing.Point(25, 5);
                box.TabIndex = 0;
            }
            else
            {
              //  box.TabIndex = textBoxes.Count - 1;
                box.Location = new System.Drawing.Point(25, textBoxes.Last().Location.Y + 25);
            }
            box.Name = "Symbol";
            box.Size = new System.Drawing.Size(30, 20);
            

            addTextBox.Location = new Point(addTextBox.Location.X, addTextBox.Location.Y + 25);
            buttonMinus.Location = new Point(buttonMinus.Location.X, buttonMinus.Location.Y + 25);
            textBoxes.Add(box);
            panelSymbols.Controls.Add(box);


        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
 
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBoxResult.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBoxResult.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBoxResult.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBoxes.Count > 0)
            {
                panelSymbols.Controls.Remove(textBoxes.Last());
                buttonMinus.Location = new Point(buttonMinus.Location.X, buttonMinus.Location.Y - 25);
                addTextBox.Location = new Point(addTextBox.Location.X, addTextBox.Location.Y - 25);

                textBoxes.Remove(textBoxes.Last());
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = Color.FromArgb(255, 255, 128, 128);
            if (MyDialog.ShowDialog() == DialogResult.OK)
                SymbolColor = MyDialog.Color;
                
        }

        private void comboBoxResolution_DropDown(object sender, EventArgs e)
        {
            if (cellsChanged)
            {

                ComboBox comboBox = (ComboBox)sender;
                this.DeleteAllItems(comboBox);

                int cells = 0;
                cells = Convert.ToInt32(comboBoxSquareCount.SelectedItem);

                Color[] palette = this.GetColorsFromPanel();
                if (palette.Length > 0)
                {
                    resolutions = Calculate.PossibleResolutions(CurrentImage, cells, palette, 20);

                    foreach (var item in resolutions)
                        comboBoxResolution.Items.Add(item.Key);
                }
                else
                {
                    MessageBox.Show("Create palette");
                    return;
                }

                cellsChanged = false;
            }
        }

        private void comboBoxSquareCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DeleteAllItems(comboBoxResolution);
            comboBoxResolution.Items.Clear();
            cellsChanged = true;
        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            this.DeleteAllItems(this.comboBoxResolution);

            int cells = 0;
            cells = Convert.ToInt32(comboBoxSquareCount.SelectedItem);

            Color[] palette = this.GetColorsFromPanel();
            if (palette.Length > 0)
            {
                resolutions = Calculate.PossibleResolutions(CurrentImage, cells, palette, 20);

                foreach (var item in resolutions)
                    comboBoxResolution.Items.Add(item.Key);
            }
            else
            {
                MessageBox.Show("Create palette");
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!panelColorChoice.Visible)
                panelColorChoice.Visible = true;
            else panelColorChoice.Visible = false;
        }

    }
}
