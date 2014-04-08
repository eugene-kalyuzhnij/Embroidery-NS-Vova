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
        PatternCreator creator;
        List<TextBox> textBoxes;
        Color SymbolColor;

        public Form1()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
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


/*--------------------using dll here--------------------------------------------------------------------------------------------------*/
                            creator = new PatternCreator(CurrentImage);
                            
                            List<int> allSquare = creator.GetPossibleSquareCounts();
                            List<Resolution> resolutions = creator.GetPossibleResolutions(6);
/*------------------------------------------------------------------------------------------------------------------------------------*/

                            foreach (var item in allSquare)
                                comboBoxSquareCount.Items.Add(item);

                            foreach (var item in resolutions)
                                comboBoxResolution.Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        private void DeleteAllItems(ComboBox comboBox)
        {
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
            
            int squareCount = 0;
            try
            {
                squareCount = Convert.ToInt32(comboBoxSquareCount.SelectedItem);
            }
            catch
            {
                MessageBox.Show("Sure that Count of squares is correct");
                return;
            }

            if (squareCount == 0)
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


            char[] masSymbols = new char[symbols.Count];
            int i = 0;
            foreach (var item in symbols)
                masSymbols[i++] = item;


 /*--------------------using dll here------------------------------------------------*/
            creator = new PatternCreator(CurrentImage);
            if (palette.Length <= masSymbols.Length)
            {
                creator.SymbolsFlag = true;
                creator.Symbols = masSymbols;
            }
            if (checkBoxGrid.CheckState == CheckState.Checked) 
                creator.GridFlag = true;

            creator.SymbolColor = SymbolColor;
            creator.SquareCount = squareCount;
            creator.Resolution = (Resolution)comboBoxResolution.SelectedItem;
            creator.Palette = palette;

            Bitmap result = creator.GetImage();
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



    }
}
