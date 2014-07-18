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
using System.Windows.Shapes;
using System.IO;
using DBitmap = System.Drawing.Bitmap;
using DColor = System.Drawing.Color;

namespace NSEmbroidery.WPF
{

    /// <summary>
    /// Interaction logic for Preview.xaml
    /// </summary>
    public partial class Preview : Window
    {


        public Preview()
        {
            double width = System.Windows.SystemParameters.PrimaryScreenWidth;
            double height = System.Windows.SystemParameters.PrimaryScreenHeight;

            InitializeComponent();


            this.Width = width / 3;
            this.Height = height / 2;
            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapSource image = previewImage.Source as BitmapSource;
            using (var fileStream = new FileStream("D:\\new.jpeg", FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
