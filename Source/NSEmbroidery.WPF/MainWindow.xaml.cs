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


namespace NSEmbroidery.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string imageName = null;

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
        }

        private void color_MouseUp(object sender, MouseEventArgs e)
        {
            choosedColors.Children.Remove((UIElement)e.Source);
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
            
            Dictionary<string, int> resolutions = wcf_service.PossibleResolutions(image, cellsCount, 2, 10);
            
            comboBoxResolutions.ItemsSource = resolutions.Keys;
        }

        private void cellsCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            comboBoxResolutions.ItemsSource = null;
        }

        private void createEmbroidery_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Color[] palette = GetPalette(choosedColors.Children);
            
            Embroidery.EmbroideryCreatorServiceClient wcf_service = new Embroidery.EmbroideryCreatorServiceClient();

        }

        //Don't work
        private System.Drawing.Color[] GetPalette(UIElementCollection elements)
        {
            System.Drawing.Color[] colors = new System.Drawing.Color[elements.Count];

            for (int i = 0; i < elements.Count; i++)
            {
                Brush cololr = ((Rectangle)elements[i]).Fill;
                //colors[i] = System.Drawing.Colo
            }

            return colors;

        }

        

    }
}
