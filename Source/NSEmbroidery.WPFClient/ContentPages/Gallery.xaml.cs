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

namespace NSEmbroidery.WPFClient
{
    /// <summary>
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : Page
    {
        public Gallery()
        {
            InitializeComponent();
        }

        private void Gallery_Loaded(object sender, RoutedEventArgs e)
        {
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            List<BitmapSource> embroideries = client.GetSmallEmbroideries(client.CurrentUserId);

            foreach (var item in embroideries)
                stackPanelImages.Children.Add(new Image()
                {
                    Source = item,
                    Width = 150,
                    Height = 150,
                    Margin = new Thickness(5, 5, 5, 5)
                });

        }

        
    }
}
