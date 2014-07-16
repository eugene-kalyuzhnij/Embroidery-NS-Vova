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
using System.ServiceModel;
using NSEmbroidery.UI.Embroidery;
using System.Configuration;
using System.Diagnostics;

namespace NSEmbroidery.UI
{
    public partial class MainForm : Form
    {
        Bitmap CurrentImage { get; set; }
        List<TextBox> textBoxes;
        Color SymbolColor;
        Dictionary<string, int> resolutions;
        bool isChangedCells;

        NSEmbroidery.UI.Embroidery.EmbroideryCreatorServiceClient embroideryService;

        private delegate System.Drawing.Bitmap Embroidery(System.Drawing.Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, NSEmbroidery.UI.Embroidery.GridType type);

        public MainForm()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            this.FillPanelPalette();
            this.pictureBoxCurrentImage.Select();
            
            /*
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.Save(ConfigurationSaveMode.Modified, true);

            ConfigurationManager.RefreshSection("appSettings");
            */
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
            var allColors = GetColorsFromPanel();
            if (allColors != null)
                foreach (var item in allColors)
                    if (item == color) return;

                int x;
                int countControls = panelColors.Controls.Count;

                if (countControls > 0)
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

                if (panelSymbols.Controls.Count > 0)
                {
                    int xTextBox = textBoxes.Last().Location.X + 35;
                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(30, 10);
                    textBox.Location = new Point(xTextBox, 5);
                    textBox.Text = (panelSymbols.Controls.Count + 1).ToString();

                    textBoxes.Add(textBox);
                    panelSymbols.Controls.Add(textBox);
                }
        }

        private void boxColor_Click(object sender, EventArgs e)
        {
            PictureBox boxColor = sender as PictureBox;
            if (boxColor != null)
            {
                panelColors.Controls.Remove(boxColor);
                RefreshPanelColor();

                if (panelSymbols.Controls.Count > 0)
                {
                    panelSymbols.Controls.RemoveAt(panelSymbols.Controls.Count - 1);
                    textBoxes.Remove(textBoxes.Last());
                    if (panelSymbols.Controls.Count == 0)
                        buttonRemoveSymbols.Visible = false;
                }
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
            if (panelColors.Controls.Count > 0)
            {
                Color[] result = new Color[panelColors.Controls.Count];
                int i = 0;
                foreach (PictureBox item in panelColors.Controls)
                {
                    result[i++] = item.BackColor;
                }

                return result;
            }

            return null;
        }



        private void buttonCreateEmbroidery_Click(object sender, EventArgs e)
        {
            #region Exceptions
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

            if (palette == null)
            {
                MessageBox.Show("Choose palette");
                return;
            }

            resultLabel.Text = "Loading...";
            resultLabel.Refresh();


            int ratio;
            resolutions.TryGetValue((string)comboBoxResolution.SelectedItem, out ratio);

            char[] masSymbols = null;
            masSymbols = new char[symbols.Count];
            int i = 0;
            foreach (var item in symbols)
                masSymbols[i++] = item;

            if (palette.Length > masSymbols.Length)
                masSymbols = null;


            NSEmbroidery.UI.Embroidery.GridType type = NSEmbroidery.UI.Embroidery.GridType.None;
            if (checkBoxGrid.CheckState == CheckState.Checked)
            {
                if (radioButtonPoints.Checked)
                    type = NSEmbroidery.UI.Embroidery.GridType.Points;
                else if (radioButtonLine.Checked)
                    type = NSEmbroidery.UI.Embroidery.GridType.SolidLine;
            }
            #endregion

/*--------------------using service here-------------------------------------------------*/
            string uri = Properties.Settings.Default.AddressOfService;
            embroideryService = EmbroideryService.GetEmbroideryService(uri);
            Embroidery embroideryDelegate = new Embroidery(embroideryService.GetEmbroidery);
            try
            {
                Bitmap image = new Bitmap(CurrentImage);

                Stopwatch watch = new Stopwatch();
                watch.Start();
                IAsyncResult asuncResult = embroideryDelegate.BeginInvoke(image, ratio, cellsCount, new System.Drawing.Color[]{System.Drawing.Color.Black, System.Drawing.Color.White, System.Drawing.Color.Red}, masSymbols, SymbolColor, type, null, null);
                Bitmap resultImage = embroideryDelegate.EndInvoke(asuncResult);
                watch.Stop();

                labelResolutionImage.Text = "Time spent: ";
                labelResolutionImage.Text += watch.ElapsedMilliseconds.ToString();
                labelResolutionImage.Refresh();

                embroideryService.Close();
                ResultImage imageForm = new ResultImage();
                imageForm.Image = new Bitmap(resultImage);

                resultLabel.Text = "";

                imageForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resolution that was choosed is too large");
                resultLabel.Text = "";
                return;
            }
/*---------------------------------------------------------------------------------------*/

        }


        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxResolution_DropDown(object sender, EventArgs e)
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

                    if (isChangedCells)
                    {
                        if (CurrentImage != null)
                        {
                            try
                            {
                                labelWaitResolution.Text = "Wait...";
                                labelWaitResolution.Refresh();

                                try
                                {
                                    using (Bitmap image = new Bitmap(CurrentImage))
                                    {
                                        embroideryService = EmbroideryService.GetEmbroideryService(Properties.Settings.Default.AddressOfService);
                                        resolutions = embroideryService.PossibleResolutions(image, cells, 2, 10);//Count of resolutions here <-----------|
                                        embroideryService.Close();
                                    }
                                }
                                catch (OutOfMemoryException ex)
                                {
                                    MessageBox.Show("Input image is too large");
                                    labelWaitResolution.Text = "";
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Occurred some exception. Try set less or more count of cells");
                                    labelWaitResolution.Text = "";
                                    return;
                                }

                                foreach (var item in resolutions)
                                    comboBoxResolution.Items.Add(item.Key);

                                isChangedCells = false;

                                labelWaitResolution.Text = "";
                                labelWaitResolution.Refresh();
                            }
                            catch(Exception ex)
                            {
                                labelWaitResolution.Text = "";
                                MessageBox.Show("Incorrect data: " + ex.Message);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Open image first");
                            return;
                        }
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
            if (GetColorsFromPanel() != null)
            {
                int colorsLength = this.GetColorsFromPanel().Length;

                    int x = 5;
                    for (int i = 0; i < colorsLength; i++)
                    {

                        TextBox textBox = new TextBox();
                        textBox.Size = new Size(30, 10);
                        textBox.Location = new Point(x, 5);

                        textBoxes.Add(textBox);
                        panelSymbols.Controls.Add(textBox);
                        textBox.Text = (i + 1).ToString();

                        x = x + 35;
                    }
                    buttonRemoveSymbols.Visible = true;
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

                            CurrentImage = new Bitmap(myStream);
                            pictureBoxCurrentImage.Image = CurrentImage;

                            ShowInfoInTextBoxCells();

                            labelresolution.Text = CurrentImage.Width.ToString() + "x" + CurrentImage.Height.ToString();

                            this.pictureBoxColorChoice.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ShowInfoInTextBoxCells()
        {
            textBoxCells.ForeColor = Color.Gray;
            textBoxCells.Text = "2..." + CurrentImage.Width.ToString();
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

        private void textBoxCells_LostFocus(object sender, EventArgs e)
        {
            if (CurrentImage != null && textBoxCells.Text == "")
                ShowInfoInTextBoxCells();
        }

        private void textBoxCells_GotFocus(object sender, EventArgs e)
        {
            if (textBoxCells.Text.Contains("2..."))
            {
                textBoxCells.ForeColor = Color.Black;
                textBoxCells.Text = "";
            }
        }

        private void textBoxR_TextChanged(object sender, EventArgs e)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            //---------Red-------------------
            try
            {
                r = Convert.ToInt32(textBoxR.Text);
                if (r > 255)
                {
                    textBoxR.Text = "255";
                    r = 255;
                }
            }
            catch
            {
                r = 0;
            }
            //----------Green--------------
            try
            {
                g = Convert.ToInt32(textBoxG.Text);
                if (g > 255)
                {
                    textBoxG.Text = "255";
                    g = 255;
                }
            }
            catch
            {
                g = 0;
            }
            //------------Blue-------------
            try
            {
                b = Convert.ToInt32(textBoxB.Text);
                if (b > 255)
                {
                    textBoxB.Text = "255";
                    b = 255;
                }
            }
            catch
            {
                b = 0;
            }


            pictureBoxColorCreated.BackColor = Color.FromArgb(r, g, b);
            pictureBoxColorCreated.Refresh();

        }

        private void textBoxR_Click(object sender, EventArgs e)
        {

        }

        private void buttonCreateColor_Click(object sender, EventArgs e)
        {
            textBoxR.Text = "0";
            textBoxG.Text = "0";
            textBoxB.Text = "0";
            panelCreateColor.Visible = true;
            
        }

        private void buttonCencel_Click(object sender, EventArgs e)
        {
            panelCreateColor.Visible = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.AddColorToPanelColor(pictureBoxColorCreated.BackColor);
            panelCreateColor.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxCurrentImage_Click(object sender, EventArgs e)
        {

        }

        private void buttonRemoveSymbols_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < panelSymbols.Controls.Count; )
            {
                panelSymbols.Controls.RemoveAt(0);
                textBoxes.RemoveAt(0);
            }

            buttonRemoveSymbols.Visible = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void servicesAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressOfService newWindow = new AddressOfService();
            newWindow.ShowDialog();
        }

    }
}
