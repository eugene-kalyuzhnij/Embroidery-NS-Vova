using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
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
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using NSEmbroidery.Data.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;
using DColor = System.Drawing.Color;
using DBitmap = System.Drawing.Bitmap;


namespace NSEmbroidery.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        HttpClient client;
        string imageName = null;
        Dictionary<string, int> resolutions = null;
        bool wasWrongCellsCount = false;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            colorPicker.SelectedColor = Color.FromArgb(0, 255, 255, 255);
        }




        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (informationText.Text != "") informationText.Text = "";

            Color choosedColor = colorPicker.SelectedColor;

            if (!HasThisColor(choosedColor) && choosedColor != Color.FromArgb(0, 255, 255, 255))
            {
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
        }

        private bool HasThisColor(Color color)
        {
            foreach (var item in choosedColors.Children)
            {
                Rectangle rectangle = item as Rectangle;
                if (rectangle != null)
                {
                    SolidColorBrush colorBrush = rectangle.Fill as SolidColorBrush;
                    if (colorBrush != null)
                        if (colorBrush.Color == color) return true;
                }
            }


            return false;
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

                cellsCountTextBox.Text = "";
                // Open document
                openedImage.Source = null;

                string filename = dlg.FileName;
                imageName = filename;

                BitmapImage image;
                try
                {
                    image = new BitmapImage(new Uri(filename));
                }
                catch
                {
                    informationText.Text = "Wrong image format";
                    return;
                }

                int width = image.PixelWidth;
                int height = image.PixelHeight;

                cellsCountTextBox.Text = "1.." + width;
                
                resolutionText.Text = width + "x" + height;
                openedImage.Source = image;

            }
        }

        private async void ComboBoxResolutions_DropDownOpened(object sender, EventArgs e)
        {
            System.Drawing.Bitmap image;
            int cellsCount;

            if(imageName != null)
            {
                try
                {
                    image = new DBitmap(imageName);
                }
                catch (OutOfMemoryException ex)
                {
                    MessageBox.Show("Sorry, but image is too large :(");
                    return;
                }
                catch
                {
                    informationText.Text = "Could not open image"; ;
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
                cellsCountTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 200, 10, 10));
                wasWrongCellsCount = true;
                return;
            }

            if (cellsCount <= 1) 
            { 
                informationText.Text = "count of cells has to be more than 1";
                return; 
            }
            else if (cellsCount > image.Width) { informationText.Text = "count of cells has to be less than " + (image.Width + 1).ToString(); return; }


            Embroidery.EmbroideryCreatorServiceClient wcf_service = new Embroidery.EmbroideryCreatorServiceClient();
            List<string> waitEnumerable = new List<string>();
            waitEnumerable.Add("Wait");

            comboBoxResolutions.ItemsSource = waitEnumerable;
            try
            {
                resolutions = await wcf_service.PossibleResolutionsAsync(image, cellsCount, 2, 10);
            }
            catch(Exception ex)
            {
                comboBoxResolutions.ItemsSource = null;
                informationText.Text = ex.Message;
                return;
            }
            if (resolutions == null)
            {
                informationText.Text = "Some error was occured while getting resolutions. See log for details.";
                comboBoxResolutions.ItemsSource = null;
                return;
            }

            comboBoxResolutions.ItemsSource = resolutions.Keys;

        }

        private void cellsCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            comboBoxResolutions.ItemsSource = null;
        }

        private void createEmbroidery_Click(object sender, RoutedEventArgs e)
        {
            //loadingCanvas.Visibility = System.Windows.Visibility.Visible;
            //ShowLoading(loadingCanvas);
            informationText.Text = "";

            DColor[] palette;
            char[] symbols = null;
            Embroidery.GridType gridType = Embroidery.GridType.None;
            int coefficient;
            int _cellsCount;

            palette = GetPalette(choosedColors.Children);

            if (palette == null){
                informationText.Text = "";
                informationText.Text = "Create palette before embroidery creating";
                //HideLoading();
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
                //HideLoading();
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
                    //HideLoading();
                    return;
                }
            }

            try
            {
                _cellsCount = Convert.ToInt32(cellsCountTextBox.Text);
            }
            catch
            {
                informationText.Text = "cout of cells is wrong.";
                //HideLoading();
                return;
            }



            Thread openPreview = new Thread(() => {
                Preview preview = new Preview();
                preview.Show();

                Thread creatingEmbroidery = new Thread(() =>
                {
                    CreateEmbroidery(preview, imageName, coefficient, _cellsCount, palette, symbols, DColor.Black, gridType);
                });
                creatingEmbroidery.Start();

                System.Windows.Threading.Dispatcher.Run();
            });
            openPreview.SetApartmentState(ApartmentState.STA);
            openPreview.IsBackground = true;
            openPreview.Start();

           

        }

        public void ShowLoading(DependencyObject obj)
        {
            obj.Dispatcher.Invoke(delegate { loadingCanvas.Visibility = System.Windows.Visibility.Visible; }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }

        private void HideLoading()
        {
            loadingCanvas.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void CreateEmbroidery(Preview preview, string imageName, int coefficient, int cellsCount, DColor[] palette, char[] symbols, DColor symbolColor, Embroidery.GridType gridType)
        {

            BitmapSource bitmapSource = null;

            try
            {
                DBitmap _imageFromFile = new DBitmap(imageName);

                Func<DBitmap, DBitmap> getCopyBitmap = new Func<DBitmap, DBitmap>(GetCopyOfBitmap);
                IAsyncResult getCopyBitmapResult = getCopyBitmap.BeginInvoke(_imageFromFile, null, null);
                DBitmap inputImage = getCopyBitmap.EndInvoke(getCopyBitmapResult);

                Embroidery.EmbroideryCreatorServiceClient wcf_service = new Embroidery.EmbroideryCreatorServiceClient();

                Func<DBitmap, int, int, DColor[], char[], DColor, Embroidery.GridType, DBitmap> getEmbroidery = new Func<DBitmap, int, int, DColor[], char[], DColor, Embroidery.GridType, DBitmap>(wcf_service.GetEmbroidery);
                IAsyncResult getEmbroideryResult = getEmbroidery.BeginInvoke(inputImage, coefficient, cellsCount, palette, symbols, DColor.Black, gridType, null, null);
                DBitmap resultImage = getEmbroidery.EndInvoke(getEmbroideryResult);


                if (resultImage == null)
                {
                    MessageBox.Show("Some error occured on the server");
                    //HideLoading();
                    return;
                }

                bitmapSource = GetBitmapSource(resultImage);
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show("Sorry, but image is too large :(");
                //HideLoading();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some exception was occured. Message: " + ex.Message);
                //HideLoading();
                return;
            }

            bitmapSource.Freeze();

            preview.Dispatcher.BeginInvoke(new Action(() =>
                {
                    preview.loadingPanel.Visibility = System.Windows.Visibility.Collapsed;
                    preview.previewImage.Source = bitmapSource;
                }),
                System.Windows.Threading.DispatcherPriority.Normal);
           

        }


        private BitmapSource GetBitmapSource(DBitmap image)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(),
                                                      IntPtr.Zero,
                                                      System.Windows.Int32Rect.Empty,
                                                      BitmapSizeOptions.FromWidthAndHeight(image.Width, image.Height));


            return bitmapSource;
        }


        public DBitmap GetCopyOfBitmap(DBitmap image)
        {
            DBitmap inputImage = new DBitmap(image.Width, image.Height);

            for (int y = 0; y < inputImage.Height; y++)
                for (int x = 0; x < inputImage.Width; x++)
                    inputImage.SetPixel(x, y, image.GetPixel(x, y));


            return inputImage;
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
            textBox.GotFocus += textBox_GotFocus;

            dockPanelSymbols.Children.Add(textBox);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox current = (TextBox)sender;
            current.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            if (informationText.Text != "") informationText.Text = "";
        }

        private void symbolsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            dockPanelSymbols.Children.RemoveRange(0, dockPanelSymbols.Children.Count);
            if (informationText.Text != "") informationText.Text = "";
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

        private void cellsCountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (informationText.Text != "") informationText.Text = "";
            if (cellsCountTextBox.Text.Contains("1..")) cellsCountTextBox.Text = "";
            SolidColorBrush solidColor = new SolidColorBrush(Color.FromArgb(255, 200, 10, 10));
            if (wasWrongCellsCount)
            {
                wasWrongCellsCount = false;
                cellsCountTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        private void removeColorsButton_Click(object sender, RoutedEventArgs e)
        {
            choosedColors.Children.RemoveRange(0, choosedColors.Children.Count);
        }

        private void RemoveSettings()
        {
            checkBoxSymbols.IsChecked = false;
            checkBoxGrid.IsChecked = false;
            choosedColors.Children.RemoveRange(0, choosedColors.Children.Count);
            cellsCountTextBox.Text = "";
        }

        private void removeSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveSettings();
        }

        private void comboBoxResolutions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
