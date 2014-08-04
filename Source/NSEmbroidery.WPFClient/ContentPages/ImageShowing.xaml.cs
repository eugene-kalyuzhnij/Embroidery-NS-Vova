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
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.WPFClient
{
    /// <summary>
    /// Interaction logic for Image.xaml
    /// </summary>
    public partial class ImageShowing : Page
    {
        public Frame Content { get; set; }
        public int ImageId { get; private set; }
        List<Embroidery> smallImages;
        int? nextId;
        int? prevId;

        public ImageShowing(int id, List<Embroidery> smallImages, Frame content)
        {
            InitializeComponent();

            Content = content;
            ImageId = id;
            this.smallImages = smallImages;
        }

        private void image_Loaded(object sender, RoutedEventArgs e)
        {
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            Embroidery embroidery = client.GetEmbroidery(ImageId);

            nextId = smallImages.GetNextId(ImageId);
            prevId = smallImages.GetPrevId(ImageId);

            if (prevId == null)
                buttonPrev.Visibility = Visibility.Hidden;
            if (nextId == null)
                buttonNext.Visibility = Visibility.Hidden;

            var image = new Image()
            {
                Source = embroidery.Image.GetBitmapSource()
            };

            imageContent.Children.Add(image);
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            if(prevId != null)
                Content.NavigationService.Navigate(new ImageShowing((int)prevId, smallImages, Content));
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if(nextId != null)
                Content.NavigationService.Navigate(new ImageShowing((int)nextId, smallImages, Content));
        }
    }
}
