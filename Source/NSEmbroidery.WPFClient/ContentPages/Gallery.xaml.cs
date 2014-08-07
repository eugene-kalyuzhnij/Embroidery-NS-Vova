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

        private int _otherUserId;
        private bool _isOtherUser = false;

        public Gallery(Frame content)
        {
            InitializeComponent();
            Content = content;
        }

        public Gallery(int otherUserId)
        {
            InitializeComponent();

            _isOtherUser = true;
            _otherUserId = otherUserId;
            this.Style = null;

            Content = Menu.Content;
        }


        private void Gallery_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isOtherUser)
            {
                NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
                SmallImages = client.GetSmallEmbroideries(client.CurrentUserId);
            }
            else
            {
                NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
                SmallImages = client.GetSmallEmbroideries(_otherUserId);
            }

            foreach (var item in SmallImages)
            {
                var image = new Image()
                {
                    Source = item.GetSmallImage().GetBitmapSource(),
                    Width = 150,
                    Height = 150,
                    DataContext = item,
                    Stretch = Stretch.None
                };

                var border = new Border()
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(255, 10, 10, 10)),
                    Margin = new Thickness(5, 5, 5, 5),
                    BorderThickness = new Thickness(2)
                };

                border.Child = image;

                image.MouseUp += image_MouseUp;
                images.Children.Add(border);
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
