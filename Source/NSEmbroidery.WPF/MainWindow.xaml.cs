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
using DColor = System.Drawing.Color;


namespace NSEmbroidery.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string imageName = null;
        Dictionary<string, int> resolutions = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Color choosedColor = e.NewValue;
            
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
                    MessageBox.Show("Could not open image");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Open image first");
                return;
            }
            
            try
            {
                cellsCount = Convert.ToInt32(cellsCountTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Count of cells is incorrect");
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

        //Dont work
        private void createEmbroidery_Click(object sender, RoutedEventArgs e)
        {
            //TODO: end method implementation and process all exceptions

            DColor[] palette;
            char[] symbols;
            Embroidery.GridType gridType = Embroidery.GridType.None;
            int coefficient;

            palette = GetPalette(choosedColors.Children);

            if (palette == null){
                informationText.Text = "";
                informationText.Text = "Create palette before embroidery creating";
                return;
            }

            if ((bool)checkBoxSymbols.IsChecked) symbols = GetSymbols();

            resolutions.TryGetValue((string)comboBoxResolutions.SelectedItem, out coefficient);

            if ((bool)checkBoxGrid.IsChecked)
            {
                if ((bool)radioButtonLine.IsChecked) gridType = Embroidery.GridType.SolidLine;
                else gridType = Embroidery.GridType.Points;
            }




            Embroidery.EmbroideryCreatorServiceClient wcf_service = new Embroidery.EmbroideryCreatorServiceClient();

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
            int count = GetPalette(choosedColors.Children).Count();

            for (int i = 0; i < count; i++)
            {
                string currentSymbol = (i < 10) ? i.ToString() : "";
                AddSymbolTextBox(currentSymbol);
            }

        }

        private void AddSymbolTextBox(string symbol)
        {
                dockPanelSymbols.Children.Add(new TextBox() { Width = 30, Margin = new Thickness(2, 2, 2, 2),
                                                              Text = ((symbol != null)? symbol : "") });
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
            char[] symbols = new char[dockPanelSymbols.Children.Count];
            int i = 0;
            foreach (var item in dockPanelSymbols.Children)
            {
                symbols[i++] = ((TextBox)item).Text.Single();
            }

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
