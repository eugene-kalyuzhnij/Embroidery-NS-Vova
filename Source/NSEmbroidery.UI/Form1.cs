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
        Dictionary<Resolution, int> resolutions;
        bool isChangedCells;

        private delegate Bitmap Embroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, bool grid, GridType type);

        public Form1()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            this.FillPanelPalette();
        }

        
        private void FillPanelPalette()
        {

            List<PictureBox> pictureBoxes = new List<PictureBox>();
            int x = 0;
            int y = 0;
            for (int i = 0; i < 12; i++)
            {
                if (i == 6)
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
            pictureBoxes[10].BackColor = Color.Aqua;
            pictureBoxes[11].BackColor = Color.Bisque;
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
        
        private void AddColorToPanelColor(Color color)
        {
            int x;
            int countControls = panelColors.Controls.Count;

            if(countControls > 0)
                x = panelColors.Controls[countControls - 1].Location.X + 25;
            else x = 10;

            PictureBox boxColor = new PictureBox();
            boxColor.Size = new Size(20, 20);
            boxColor.Location = new Point(x, 5);
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
            int x = 10;

            foreach (PictureBox item in panelColors.Controls)
            {
                item.Location = new Point(x, 5);

                x += 25;
            }
        }

        private Color[] GetColorsFromPanel()
        {
            Color[] result = new Color[panelColors.Controls.Count];
            int i = 0;
            foreach (PictureBox item in panelColors.Controls)
            {
                result[i++] = item.BackColor;
            }

            return result;
        }



        private void buttonCreateEmbroidery_Click(object sender, EventArgs e)
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
                cellsCount = Convert.ToInt32(textBoxCells.Text);
            }
            catch
            {
                MessageBox.Show("Count of cells is wrong");
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

            resultLabel.Text = "Loading...";


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
            GridType type = GridType.SolidLine;
            if (checkBoxGrid.CheckState == CheckState.Checked)
            {
                grid = true;
                if (radioButtonPoints.Checked)
                    type = GridType.Points;
            }

            Embroidery callMethod = new Embroidery(PatternCreator.CreateEmbroidery);

/*--------------------using dll here-------------------------------------------------*/
            IAsyncResult result = callMethod.BeginInvoke(CurrentImage, ratio, cellsCount, palette, masSymbols, SymbolColor, grid, type, null, null);
            Bitmap embordieryImage = callMethod.EndInvoke(result);
/*-----------------------------------------------------------------------------------*/

            ResultImage imageForm = new ResultImage();
            imageForm.Image = embordieryImage;

            resultLabel.Text = "";

            imageForm.ShowDialog();

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxResolution_DropDown(object sender, EventArgs e)//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
                ComboBox comboBox = (ComboBox)sender;

                int cells = 0;
                try
                {
                    cells = Convert.ToInt32(textBoxCells.Text);
                }
                catch
                {
                    MessageBox.Show("Input count of cells first");
                    return;
                }

                Color[] palette = this.GetColorsFromPanel();
                if (palette.Length > 0)
                {
                    if (isChangedCells)
                    {
                        if (CurrentImage != null)
                        {
                            resolutions = Calculate.PossibleResolutions(CurrentImage, cells, palette, 10);//Count of resolution here <-----------|

                            foreach (var item in resolutions)
                                comboBoxResolution.Items.Add(item.Key);

                            isChangedCells = false;
                        }
                        else
                        {
                            MessageBox.Show("Open image first");
                            return;
                        }
                    }
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

        private void buttonAddSymbols_Click(object sender, EventArgs e)
        {
            int colorsLength = this.GetColorsFromPanel().Length;
            if (colorsLength > 0)
            {
                int x = 5;
                for (int i = 0; i < colorsLength; i++)
                {

                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(30, 10);
                    textBox.Location = new Point(x, 5);

                    textBoxes.Add(textBox);
                    panelSymbols.Controls.Add(textBox);

                    x = x + 35;
                }
            }
            else MessageBox.Show("Create palette first");
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
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
                            DeleteAllItems(comboBoxResolution);
                            comboBoxResolution.SelectedItem = null;

                            textBoxCells.Text = "";

                            CurrentImage = new Bitmap(myStream);
                            pictureBoxCurrentImage.Image = CurrentImage;

                            labelresolution.Text = CurrentImage.Width.ToString() + "x" + CurrentImage.Height.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void pictureBoxSymbolColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = Color.FromArgb(255, 255, 128, 128);
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                SymbolColor = MyDialog.Color;
                pictureBoxSymbolColor.BackColor = MyDialog.Color;
            }

        }

        private void checkBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxGrid.Checked)
            {
                if(!(radioButtonPoints.Checked || radioButtonLine.Checked))
                    radioButtonLine.Checked = true;
            }
            else
            {
                radioButtonLine.Checked = false;
                radioButtonPoints.Checked = false;
            }

        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
           RadioButton radioButton = (RadioButton)sender;
           if (radioButton.Checked)
                checkBoxGrid.Checked = true;
        }

        private void textBoxCells_TextChanged(object sender, EventArgs e)
        {
            this.comboBoxResolution.SelectedItem = null;
            this.DeleteAllItems(comboBoxResolution);

            isChangedCells = true;
        }


    }
}
