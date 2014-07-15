using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using DColor = System.Drawing.Color;
using DBitmap = System.Drawing.Bitmap;


namespace NSEmbroidery.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string imageName = null;
        Dictionary<string, int> resolutions = null;
        private delegate System.Drawing.Bitmap EmbroideryAsync(System.Drawing.Bitmap image, int resolutionCoefficient, int cellsCount, DColor[] palette, char[] symbols, DColor symbolColor, Embroidery.GridType type);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            
            Color choosedColor = colorPicker.SelectedColor;
            
            Rectangle colorItem = new Rectangle();
            colorItem.Width = 20;
            colorItem.Height = 20;
            colorItem.Fill = new SolidColorBrush(choosedColor);
            colorItem.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            colorItem.Margin = new Thickness(5, 5, 0, 5);
            colorItem.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            colorItem.MouseUp += color_MouseUp;

            choosedColors.Children.Add(colorItem);

            if ((bool)checkBoxSymbols.IsChecked)
                AddSymbolTextBox("");
        }

        private void color_MouseUp(object sender, MouseEventArgs e)
        {
            choosedColors.Children.Remove((UIElement)e.Source);
            if((bool)checkBoxSymbols.IsChecked)
                dockPanelSymbols.Children.RemoveAt(dockPanelSymbols.Children.Count - 1);
        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.Filter = "All files (*.*)|*.*|jpg files (*.jpg)|*.jpg|bmp files (*bmp)|*.bmp";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                openedImage.Source = null;

                string filename = dlg.FileName;
                imageName = filename;
                BitmapImage image = new BitmapImage(new Uri(filename));

                resolutionText.Text = image.Width + "x" + image.Height;
                openedImage.Source = image;

            }
        }

        private void ComboBoxResolutions_DropDownOpened(object sender, EventArgs e)
        {
            System.Drawing.Bitmap image;
            int cellsCount;

            if(imageName != null)
            {
                try
                {
                    image = new System.Drawing.Bitmap(imageName);
                }
                catch
                {
                    informationText.Text = "Could not open image";
                    return;
                }
            }
            else
            {
                informationText.Text = "Open image first";
                return;
            }
            
            try
            {
                cellsCount = Convert.ToInt32(cellsCountTextBox.Text);
            }
            catch (Exception ex)
            {
                informationText.Text = "Count of cells is incorrect";
                return;
            }

            Embroidery.EmbroideryCreatorServiceClient wcf_service = new Embroidery.EmbroideryCreatorServiceClient();
            
            resolutions = wcf_service.PossibleResolutions(image, cellsCount, 2, 10);
            
            comboBoxResolutions.ItemsSource = resolutions.Keys;

        }

        private void cellsCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            comboBoxResolutions.ItemsSource = null;
        }

        private async void createEmbroidery_Click(object sender, RoutedEventArgs e)
        {

            DColor[] palette;
            char[] symbols = null;
            Embroidery.GridType gridType = Embroidery.GridType.None;
            int coefficient;
            int _cellsCount;

            palette = GetPalette(choosedColors.Children);

            if (palette == null){
                informationText.Text = "";
                informationText.Text = "Create palette before embroidery creating";
                return;
            }

            if ((bool)checkBoxSymbols.IsChecked) symbols = GetSymbols();

            try
            {
                resolutions.TryGetValue((string)comboBoxResolutions.SelectedItem, out coefficient);
            }
            catch
            {
                informationText.Text = "Choose resolution first";
                return;
            }

            if ((bool)checkBoxGrid.IsChecked)
            {
                if ((bool)radioButtonLine.IsChecked) gridType = Embroidery.GridType.SolidLine;
                else gridType = Embroidery.GridType.Points;
            }


            if ((bool)checkBoxSymbols.IsChecked)
            {
                symbols = GetSymbols();
                if (symbols == null)
                {
                    informationText.Text = "Symbols initialization is incorrect";
                    return;
                }
            }


            DBitmap _imageFromFile = new DBitmap(imageName);
            DBitmap inputImage = new DBitmap(_imageFromFile.Width, _imageFromFile.Height);

            for(int y = 0; y < inputImage.Height; y++)
                for(int x = 0; x < inputImage.Width; x++)
                    inputImage.SetPixel(x, y, _imageFromFile.GetPixel(x, y));

            _cellsCount = Convert.ToInt32(cellsCountTextBox.Text);

            Embroidery.EmbroideryCreatorServiceClient wcf_service = new Embroidery.EmbroideryCreatorServiceClient();

            EmbroideryAsync asyncCall = new EmbroideryAsync(wcf_service.GetEmbroidery);

            IAsyncResult resultAsyncCall = asyncCall.BeginInvoke(inputImage, coefficient, _cellsCount, palette, symbols, DColor.Black, gridType, null, null);

            DBitmap resultImage = asyncCall.EndInvoke(resultAsyncCall);

            
            BitmapSource bitmapSource = await GetBitmapSource(resultImage);
            
            Preview preview = new Preview();
            preview.canvasPreview.Children.Add(new Image() { Source = bitmapSource });
            preview.ShowDialog();
        }

        private async Task<BitmapSource> GetBitmapSource(DBitmap image)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(),
                                                      IntPtr.Zero,
                                                      System.Windows.Int32Rect.Empty,
                                                      BitmapSizeOptions.FromWidthAndHeight(image.Width, image.Height));


            return bitmapSource;
        }

        private DColor[] GetPalette(UIElementCollection elements)
        {
            DColor[] colors;
            int count = elements.Count;

            if (count == 0)
                return null;

            colors = new DColor[elements.Count];

            for (int i = 0; i < elements.Count; i++)
            {
                Color colol = (((SolidColorBrush)((Rectangle)elements[i]).Fill)).Color;
                colors[i] = DColor.FromArgb(colol.A, colol.R, colol.G, colol.B);
            }

            return colors;

        }

        private void symbolsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int count;

            try
            {
                count = GetPalette(choosedColors.Children).Count();
            }
            catch
            {
                informationText.Text = "Create palette first";
                return;
            }

            for (int i = 0; i < count; i++)
            {
                string currentSymbol = (i < 10) ? i.ToString() : "";
                AddSymbolTextBox(currentSymbol);
            }

        }

        private void AddSymbolTextBox(string symbol)
        {
            TextBox textBox = new TextBox(){ Width = 30, Margin = new Thickness(2, 2, 2, 2),         
                                             Text = ((symbol != null)? symbol : "") };
            textBox.TextChanged += textBox_TextChanged;

            dockPanelSymbols.Children.Add(textBox);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox current = (TextBox)sender;
            current.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void symbolsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            dockPanelSymbols.Children.RemoveRange(0, dockPanelSymbols.Children.Count);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private char[] GetSymbols()
        {
            bool wasException = false;
            char[] symbols = new char[dockPanelSymbols.Children.Count];
            int i = 0;
            foreach (var item in dockPanelSymbols.Children)
            {
                char symbol = ' ';
                try
                {
                    symbol = ((TextBox)item).Text.Single();
                }
                catch
                {
                    ((TextBox)item).Background = new SolidColorBrush(Color.FromArgb(100, 100, 10, 10));
                    wasException = true;
                }
                if(!wasException)
                    symbols[i++] = symbol;
            }

            if (wasException) return null;

            return symbols;
        }

        private void gridCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonLine.Visibility = System.Windows.Visibility.Visible;
            radioButtonPoints.Visibility = System.Windows.Visibility.Visible;

            if ((bool)!radioButtonPoints.IsChecked && (bool)!radioButtonLine.IsChecked)
                radioButtonLine.IsChecked = true;
        }

        private void checkBoxGrid_Unchecked(object sender, RoutedEventArgs e)
        {
            radioButtonLine.Visibility = System.Windows.Visibility.Hidden;
            radioButtonPoints.Visibility = System.Windows.Visibility.Hidden;

        }

        private void textBlockInformation(object sender, MouseButtonEventArgs e)
        {
            ((TextBlock)sender).Text = "";
        }

    }
}
