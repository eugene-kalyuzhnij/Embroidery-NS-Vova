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
using NSEmbroidery.WPFClient;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.WPFClient
{
    /// <summary>
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : Page
    {
        public Frame Content { get; set; }
        public List<Embroidery> SmallImages { get; set; }

        public Gallery(Frame content)
        {
            InitializeComponent();
            Content = content;
        }

        private void Gallery_Loaded(object sender, RoutedEventArgs e)
        {
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            SmallImages = client.GetSmallEmbroideries(client.CurrentUserId);

            foreach (var item in SmallImages)
            {
                var image = new Image()
                {
                    Source = item.SmallImage.GetBitmapSource(),
                    Width = 150,
                    Height = 150,
                    Margin = new Thickness(5, 5, 5, 5),
                    DataContext = item
                };

                image.MouseUp += image_MouseUp;
                images.Children.Add(image);
            }
             
        }

        void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image image = e.Source as Image;
            if (image != null)
            {
                Embroidery emb = image.DataContext as Embroidery;
                if(emb != null)
                    Content.NavigationService.Navigate(new ImageShowing(emb.Id, SmallImages, Content));
            }
        }


        
    }
}
